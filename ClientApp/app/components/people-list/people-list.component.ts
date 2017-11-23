import { DaterangePickerComponent } from 'ng2-daterangepicker';
import { People } from './../../models/people.interface';
import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { State } from '../../models/state.interface';
import { PeopleService } from '../../services/people.service';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-people-list',
  templateUrl: './people-list.component.html',
  styleUrls: ['./people-list.component.css']
})
export class PeopleListComponent implements OnInit {
  private readonly PAGE_SIZE = 10;
  public datePickerOptions: any = {
    locale: { format: 'DD/MM/YYYY' },
    singleDatePicker: false,
    showDropdowns: true,
    opens: "left",
    start: Date.now(),
    end: Date.now()
  };

  public registrationDateStart: Date;
  public registrationDateEnd: Date;
  public birthDateStart: Date;
  public birthDateEnd: Date;
  
  public query: any = { stateId: '', page: 0, pageSize: 0 };
  public columns : [{title: string, key: string, isSortable: boolean}] = [
    { title: '#', key: '', isSortable: true },
    { title: 'Nome', key: 'name', isSortable: true },
    { title: 'CPF', key: 'cpf', isSortable: true },
    { title: 'Nascimento', key: 'birthDate', isSortable: true },
    { title: 'Estado', key: 'state.id', isSortable: true },
    { title: 'Registro', key: 'registrationDate', isSortable: true },
    { title: 'Modificado', key: 'lastUpdate', isSortable: true }
  ];
  
  public queryResult: { totalItems: number, items: People[]} = {
    totalItems: 0,
    items: []
  };
  // public peoples: People[];
  public states: State;

  constructor(
    private title: Title,
    private router: Router,
    private datePipe: DatePipe,
    private peopleService: PeopleService) 
  {
    title.setTitle("Lista de Pessoas");
  }

  ngOnInit() {
    this.peopleService.getStates().subscribe(s => this.states = s);

    this.resetFilter();
  }

  private populatePeoples() {
    this.peopleService.getPeopleList(this.query)
      .subscribe(result => {this.queryResult = result; console.log(result)});
  }
  
  public selectBirthDate(value: any) {
    this.birthDateStart = value.start;
    this.birthDateEnd = value.end;
    this.query.birthDateStartString = this.datePipe.transform(new Date(value.start), 'yyyy-MM-dd HH:mm:ss');
    this.query.birthDateEndString = this.datePipe.transform(new Date(value.end), 'yyyy-MM-dd HH:mm:ss');
    this.onFilterChange();
  }
  
  public selectRegistrationDate(value: any) {
    this.registrationDateStart = value.start;
    this.registrationDateEnd = value.end;
    this.query.registrationDateStartString = this.datePipe.transform(new Date(value.start), 'yyyy-MM-dd HH:mm:ss');
    this.query.registrationDateEndString = this.datePipe.transform(new Date(value.end), 'yyyy-MM-dd HH:mm:ss');
    this.onFilterChange();
  }

  onFilterChange() {
    this.query.page = 1;
    this.populatePeoples();
  }

  resetFilter() {
    this.query = {
      stateId: 'TODOS',
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.populatePeoples();
  }

  sortBy(columnName: string) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending; 
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populatePeoples();
  }

  onPageChange(page: number) {
    this.query.page = page; 
    this.populatePeoples();
  }

  viewPeople(people: People) {
    this.router.navigate(['/peoples/', people.id]);
  }

}
