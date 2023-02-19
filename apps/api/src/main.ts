import { faker, Sex } from '@faker-js/faker';
import { Genders, IUser } from '@prommerce/shared-types';
import cors from 'cors';
import express, { Request } from 'express';
import path from 'path';

const app = express();

app.use(cors<Request>());
app.use(express.json());
app.use('/assets', express.static(path.join(__dirname, 'assets')));

const convertAppGendersToFaker = (appGender: Genders): Sex => {
  switch (appGender) {
    case Genders.Male:
      return Sex.Male;

    case Genders.Female:
      return Sex.Female;

    case Genders.Binary:
    case Genders.Other:
    case Genders.NotSpecified:
    default: {
      const randSexIndex = Math.floor(Math.random() * Object.keys(Sex).length);
      return Sex[Object.keys(Sex)[randSexIndex]];
    }
  }
};

const seed = () => {
  const users: IUser[] = [];

  const testUser: IUser = {
    id: faker.datatype.uuid(),
    identifier: '1',
    firstName: 'Test',
    lastName: 'User',
    gender: Genders.NotSpecified,
    birthDate: new Date('1-1-1'),
    profilePicture:
      'https://img.freepik.com/free-vector/businessman-character-avatar-isolated_24877-60111.jpg?w=740&t=st=1665314823~exp=1665315423~hmac=b116a0a675157dbc826abe58c0bb722f344672e86bb63fcbdd3f014be2ecab69',
    email: 'test.user@prommerce.com',
    phoneNumber: '1',
    createdDate: new Date(),
    createdBy: 'de0297d9-33d9-4abb-b9d2-9eb36dccb01f',
    modifiedDate: new Date(),
    modifiedBy: 'de0297d9-33d9-4abb-b9d2-9eb36dccb01f',
    deleted: false,
  };
  users.push(testUser);

  for (let i = 0; i < 100; i++) {
    const randSexIndex = Math.floor(
      Math.random() * Object.keys(Genders).length
    );
    const gender: Genders = Genders[Object.keys(Genders)[randSexIndex]];
    const firstName = faker.name.firstName(convertAppGendersToFaker(gender));
    const lastName = faker.name.lastName();
    const email = faker.internet.email(firstName, lastName);
    const user = {
      id: faker.datatype.uuid(),
      identifier: faker.random.numeric(13, {
        allowLeadingZeros: false,
      }),
      firstName,
      lastName,
      gender: gender,
      birthDate: faker.date.birthdate(),
      profilePicture: faker.image.avatar(),
      email,
      phoneNumber: faker.phone.number(),
      createdDate: faker.date.past(2),
      createdBy: 'de0297d9-33d9-4abb-b9d2-9eb36dccb01f',
      modifiedDate: faker.date.past(2),
      modifiedBy: 'de0297d9-33d9-4abb-b9d2-9eb36dccb01f',
      deleted: false,
    };
    users.push(user);
  }

  return users;
};

const _users = seed();

app.get('/users', (_, res) => {
  console.log('here');

  res.json(_users);
});

const port = process.env.PORT || 3333;
const server = app.listen(port, () => {
  console.log(`Listening at http://localhost:${port}`);
});
server.on('error', console.error);
