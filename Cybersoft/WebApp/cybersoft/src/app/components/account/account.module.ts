import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from 'src/app/components/account/account-routing.module';
import { LayoutComponent } from 'src/app/components/account/layout/layout.component';
import { LoginComponent } from 'src/app/components/account/login/login.component';
import { RegisterComponent } from 'src/app/components/account/register/register.component';
import { VerifyEmailComponent } from 'src/app/components/account/verify-email/verify-email.component';
import { ForgotPasswordComponent } from 'src/app/components/account/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from 'src/app/components/account/reset-password/reset-password.component';
import { AccountComponent } from './account.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCheckboxModule } from '@angular/material/checkbox'
import { StoreModule } from '@ngrx/store';
import { AuthReducer } from 'src/app/state/auth/auth.reducer';
import { EffectsModule } from '@ngrx/effects';
import { AuthEffects } from 'src/app/state/auth/auth.effects';
// import { ProductsReducer } from 'src/app/state/products/products.reducer';
// import { ProductsEffects } from 'src/app/state/products/products.effects';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        AccountRoutingModule,
        MatProgressSpinnerModule,
        MatCheckboxModule,
        // StoreModule.forFeature('auth', AuthReducer),
        // EffectsModule.forFeature([AuthEffects]),
        // StoreModule.forFeature('products', ProductsReducer),
        // EffectsModule.forFeature([ProductsEffects]),
    ],
    declarations: [
        LayoutComponent,
        LoginComponent,
        RegisterComponent,
        VerifyEmailComponent,
        ForgotPasswordComponent,
        ResetPasswordComponent,
        AccountComponent,
    ]
})
export class AccountModule { }
