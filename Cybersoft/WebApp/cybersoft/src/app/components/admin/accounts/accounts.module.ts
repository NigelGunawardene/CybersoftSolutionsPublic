import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AccountsRoutingModule } from './accounts-routing.module';
import { AddEditComponent } from 'src/app/components/admin/accounts/add-edit/add-edit.component';
import {AccountsComponent} from "./accounts.component";
import { IndividualAccountItemComponent } from './individual-account-item/individual-account-item.component';
import {AppModule} from "../../../app.module";
// import {PaginationComponent} from "../../shared/pagination/pagination.component";
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AccountsRoutingModule,
        NgxPaginationModule,
        // AppModule.forRoot()
    ],
    declarations: [
        AddEditComponent,
        AccountsComponent,
        IndividualAccountItemComponent,
        // PaginationComponent
    ]
})
export class AccountsModule { }
