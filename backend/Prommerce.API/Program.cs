using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Prommerce.API.Initializer;
using Prommerce.Application;
using Prommerce.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddData();
builder.Services.AddApplication();
builder.Services.AddScoped<IInitializer, DbInitializer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

// users endpoints
app.MapApplicationEndpoints();

// Initializers
using (var scope = app.Services.CreateScope())
{
    try
    {
        app.Logger.LogInformation("Executing initializers");
        var initializers = scope.ServiceProvider.GetServices<IInitializer>();
        foreach (var initializer in initializers.OrderByDescending(x => x.Priority))
        {
            app.Logger.LogInformation($"Executing initializer {initializer.GetType().Name}");
            await initializer.Initialise();
        }
    }
    catch (Exception e)
    {
        app.Logger.LogError(e, "Initializer failed");
        throw;
    }
}

app.Run();