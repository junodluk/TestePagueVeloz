import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppErrorHandler } from './app.error-handler';

import { ToastyModule } from 'ng2-toasty';
import { Daterangepicker } from 'ng2-daterangepicker';
import { TextMaskModule } from 'angular2-text-mask';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { PeopleFormComponent } from './components/people-form/people-form.component';
import { PeopleService } from './services/people.service';
import { PeopleListComponent } from './components/people-list/people-list.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        PeopleFormComponent,
        PeopleListComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ToastyModule.forRoot(),
        Daterangepicker,
        TextMaskModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'peoples', pathMatch: 'full' },
            { path: 'peoples/new', component: PeopleFormComponent },
            { path: 'peoples/:id', component: PeopleFormComponent },
            { path: 'peoples', component: PeopleListComponent },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler },
        PeopleService
    ]
})
export class AppModuleShared {
}
