import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppErrorHandler } from './app.error-handler';

import { ToastyModule } from 'ng2-toasty';
import { Daterangepicker } from 'ng2-daterangepicker';
import { TextMaskModule } from 'angular2-text-mask';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PeopleFormComponent } from './components/people-form/people-form.component';
import { PeopleService } from './services/people.service';
import { PeopleListComponent } from './components/people-list/people-list.component';
import { PaginationComponent } from './components/shared/pagination.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        PeopleFormComponent,
        PeopleListComponent,
        PaginationComponent
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
            { path: '**', redirectTo: 'peoples' }
        ])
    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler },
        DatePipe,
        PeopleService
    ]
})
export class AppModuleShared {
}
