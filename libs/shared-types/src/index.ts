import { ITrackedAndSoftDeleteEntity } from './lib/baseEntities';
import { Genders } from './lib/enums';

export * from './lib/baseEntities';
export * from './lib/enums';

export interface IUser extends ITrackedAndSoftDeleteEntity {
  identifier: string;
  firstName: string;
  lastName: string;
  gender: Genders;
  birthDate: Date;
  email: string;
  profilePicture: string;
  phoneNumber: string;
}
