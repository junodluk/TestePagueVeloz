import { PeoplePhone } from './../../models/peoplephone.interface';
import { DaterangePickerComponent } from 'ng2-daterangepicker';
import { Component, OnInit, ViewChild, ElementRef, ValueProvider } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { State } from './../../models/state.interface';
import { PeopleService } from './../../services/people.service';
import { People } from './../../models/people.interface';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/Observable/forkJoin';
import { ToastyService } from 'ng2-toasty';

@Component({
  selector: 'app-people-form',
  templateUrl: './people-form.component.html',
  styleUrls: ['./people-form.component.css']
})
export class PeopleFormComponent implements OnInit {
  // @ViewChild("birthDate") birthDateRef: DaterangePickerComponent;
  public cpfMask = [/[0-9]/, /[0-9]/, /[0-9]/, '.', /[0-9]/, /[0-9]/, /[0-9]/, '.', /[0-9]/, /[0-9]/, /[0-9]/, '-', /[0-9]/, /[0-9]/];
  public phoneMask = ['(', /[0-9]/, /[0-9]/, /[0-9]/, ')', ' ', /[0-9]/, /[0-9]/, /[0-9]/, /[0-9]/, /[0-9]/, '-', /[0-9]/, /[0-9]/, /[0-9]/, /[0-9]/];
  public datePickerOptions: any = {
    singleDatePicker: true,
    showDropdowns: true,
    opens: "left",
    startDate: Date.now()
  };

  public phoneIndex: number;
  public phoneEditForm: boolean;
  public phoneDummy: PeoplePhone = {} as PeoplePhone;
  public states: State;
  public people = {} as People;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private title: Title,
    private toastyService: ToastyService,
    private peopleService: PeopleService)
    {
      route.params.subscribe(p => {
        var id = +p['id'];
        if (id)
          this.people.id = id;
      });

      title.setTitle("Cadastro de Pessoas");
    }
    
  ngOnInit() {
    var sources : any = [
      this.peopleService.getStates()
    ];

    if (this.people.id)
      sources.push(this.peopleService.getPeople(this.people.id));

    Observable.forkJoin(sources).subscribe(data => {
      this.states = data[0] as State;
      if (this.people.id) {
        this.people = data[1] as People;
        this.datePickerOptions.startDate = new Date((data[1] as People).birthDate);
        this.people.stateId = (data[1] as People).state.id;
        console.log(this.people);
      }
    }, err => {
      if (err.status == 404)
        this.navigateBackToList();
    });
  }
  
  public selectedDate(value: any) {
    this.people.birthDate = value.start;
  }

  cpfIsValid() {
    if (!this.people.cpf)
      return false;
    
    if (this.people.cpf.length != 14)
      return false;
    
    return true;
  }

  phoneIsValid() {
    if (!this.phoneDummy.phone) 
      return false;

    if (this.phoneDummy.phone.length < this.phoneMask.length) 
      return false;

    return true;
    }

  onStateChange() {
    // this.people.stateId = this.people.state.id;
  }

  showPhoneEditForm() {
    this.phoneEditForm = true;
  }

  editPhone(index: number, phone: PeoplePhone) {
    this.showPhoneEditForm();
    this.phoneIndex = index;
    this.phoneDummy.phone = phone.phone;

    if (phone.id)
      this.phoneDummy.id = phone.id;

    if (phone.peopleId)
      this.phoneDummy.peopleId = phone.peopleId;
  }

  resetPhone() {
    this.phoneIndex = -1;
    this.phoneDummy = {} as PeoplePhone;
    this.phoneEditForm = false;
  }

  savePhone() {
    if (!this.phoneIsValid()) 
      return;

    if (this.phoneIndex >= 0)
      this.people.phones[this.phoneIndex] = this.phoneDummy;
    else 
      this.people.phones.push(this.phoneDummy);

    this.resetPhone();
    console.log(this.people.phones);
  }

  deletePhone(index: number) {
    this.people.phones.splice(index,1);
  }

  delete() {
    if (this.people.id) {
      if (confirm("Você tem certeza que deseja remover este registro?")) {
        this.peopleService.delete(this.people.id)
        .subscribe(id => {
          this.navigateBackToList();
          this.toastyService.success({
            title: 'Success',
            msg: `Pessoa removida com sucesso!`,
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          });
        });
      }
    }
  }

  submit() {
    if (this.people.id) {
      this.peopleService.update(this.people)
        .subscribe(p => {
          this.navigateBackToList();
          this.toastyService.success({
            title: 'Success',
            msg: `A pessoa ${p.name} foi modificada com sucesso!`,
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          });
        })
    } else {
      this.peopleService.create(this.people)
        .subscribe(p => {
          this.navigateBackToList();
          this.toastyService.success({
            title: 'Success',
            msg: `A pessoa ${p.name} foi inserida com sucesso!`,
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          });
        });
    }

  }

  navigateBackToList() {
    this.router.navigate(['/peoples']);
  }

}
