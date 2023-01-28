import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AddEditComponent } from 'src/app/components/admin/accounts/add-edit/add-edit.component';
import {AccountsComponent} from "./accounts.component";

const routes: Routes = [
    { path: '', component: AccountsComponent },
    // { path: 'add', component: AddEditComponent },
    // { path: 'edit/:id', component: AddEditComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AccountsRoutingModule { }
