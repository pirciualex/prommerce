export interface ITracked {
  createdDate: Date;
  createdBy: string;
  modifiedDate: Date;
  modifiedBy: string;
}

export interface ISoftDelete {
  deleted: boolean;
}

export interface ITrackedAndSoftDelete extends ITracked, ISoftDelete {}

export interface IBaseEntity {
  id: string;
}

export interface ITrackedEntity extends IBaseEntity, ITracked {}

export interface ITrackedAndSoftDeleteEntity
  extends ITrackedEntity,
    ISoftDelete {}
