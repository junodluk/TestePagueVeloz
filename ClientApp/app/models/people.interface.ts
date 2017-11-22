import { State } from './state.interface';
import { PeoplePhone } from './peoplephone.interface';

export interface People {
    name: string;
    cpf: string;
    id: number;
    state: State;
    stateId: string;
    birthDate: Date;
    rg?: string;
    registrationDate?: Date;
    lastUpdate?: Date;
    phones?: PeoplePhone[];
}