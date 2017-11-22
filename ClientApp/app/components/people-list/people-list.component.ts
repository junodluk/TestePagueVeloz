import { Component, OnInit } from '@angular/core';
import { State } from '../../models/state.interface';
import { People } from '../../models/people.interface';
import { PeopleService } from '../../services/people.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-people-list',
  templateUrl: './people-list.component.html',
  styleUrls: ['./people-list.component.css']
})
export class PeopleListComponent implements OnInit {
  public peoples: People[];

  constructor(private router: Router, private peopleService: PeopleService) { }

  ngOnInit() {
    this.peopleService.getPeopleList().subscribe(p => this.peoples = p);
  }

}
