import { State } from './../models/state.interface';
import { People } from './../models/people.interface';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class PeopleService {

  constructor(private http: Http) { }

  getPeopleList() {
    return this.http.get('/api/peoples')
      .map(res => res.json() as People[]);
  }

  getPeople(id: number) {
    return this.http.get(`/api/peoples/${id}`)
      .map(res => res.json() as People);
  }

  create(people: People) {
    return this.http.post('/api/peoples', people)
      .map(res => res.json() as People);
  }

  update(people: People) {
    return this.http.put(`/api/peoples/${people.id}`, people)
    .map(res => res.json() as People);
  }

  delete(id: number) {
    return this.http.delete(`/api/peoples/${id}`)
    .map(res => res.json());
  }

  // Não há razão para criar um serviço específico para os Estados pois não haverá manipulação dos dados
  getStates() {
    return this.http.get('/api/states')
      .map(res => res.json() as State);
  }

}
