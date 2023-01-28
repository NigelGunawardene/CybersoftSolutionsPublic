import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from 'src/app/components/account/layout/layout.component';
import { LoginComponent } from 'src/app/components/account/login/login.component';
import { RegisterComponent } from 'src/app/components/account/register/register.component';
import { VerifyEmailComponent } from 'src/app/components/account/verify-email/verify-email.component';
import { ForgotPasswordComponent } from 'src/app/components/account/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from 'src/app/components/account/reset-password/reset-password.component';
import {AccountComponent} from "./account.component";
import {AuthGuard} from "../../helpers/auth.guard";

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'profile', component: AccountComponent, canActivate: [AuthGuard] },
            { path: 'verify-email', component: VerifyEmailComponent },
            { path: 'forgot-password', component: ForgotPasswordComponent },
            { path: 'reset-password', component: ResetPasswordComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AccountRoutingModule { }
