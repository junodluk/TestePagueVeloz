import { State } from './../models/state.interface';
import { People } from './../models/people.interface';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class PeopleService {

  constructor(private http: Http) { }

  toQueryString(obj: any) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined) 
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
    console.log(parts.join('&'));
    return parts.join('&');
  }

  getPeopleList(filter: any) {
    return this.http.get(`/api/peoples?${this.toQueryString(filter)}`)
      .map(res => res.json() as { totalItems: number, items: People[]});
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
