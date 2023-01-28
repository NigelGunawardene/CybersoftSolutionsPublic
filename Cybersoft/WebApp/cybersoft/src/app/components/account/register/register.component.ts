import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
  ValidatorFn,
  ValidationErrors,
} from '@angular/forms';
import { AccountService } from 'src/app/services/account/account.service';
import { User } from 'src/app/models/user/user';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { ValidatorService } from '../../../services/validator/validator.service';
import { logInFadeIn } from '../../../route-animations';

/**
 *
 * @param form
 */

function passwordsMatchValidator(form) {
  const password = form.get('password');
  const confirmPassword = form.get('confirmPassword');

  if (password.value !== confirmPassword.value) {
    confirmPassword.setErrors({ passwordsMatch: true });
  } else {
    confirmPassword.setErrors(null);
  }
  return null;
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  animations: [logInFadeIn],
})
export class RegisterComponent implements OnInit, OnDestroy {
  registerForm!: FormGroup;
  loading = false;

  registerSubscription!: Subscription;

  constructor(
    private builder: FormBuilder,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.buildForm();
  }

  ngOnDestroy(): void {
    if (this.registerSubscription) {
      this.registerSubscription.unsubscribe();
    }
  }

  buildForm() {
    this.registerForm = this.builder.group(
      {
        username: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            ValidatorService.cannotContainSpace,
            ValidatorService.cannotContainPlus,
          ],
          ValidatorService.checkUsername(this.accountService),
        ],
        firstname: ['', [Validators.required, Validators.minLength(3)]],
        lastname: ['', [Validators.required, Validators.minLength(3)]],
        email: [
          '',
          [
            Validators.required,
            Validators.email,
            ValidatorService.cannotContainSpace,
          ],
          ValidatorService.checkEmail(this.accountService),
        ],
        phonenumber: [
          '',
          [
            Validators.required,
            ValidatorService.cannotContainSpace,
            ValidatorService.cannotContainSymbols,
          ],
          ValidatorService.checkPhoneNumber(this.accountService),
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            ValidatorService.cannotContainSpace,
          ],
        ], //passwordValidator
        confirmPassword: '',
      },
      {
        validators: passwordsMatchValidator,
      }
    );
  }

  register() {
    this.changeButtonStatus();
    let newUser: User = {
      username: this.registerForm.controls['username']!.value,
      firstName: this.registerForm.controls['firstname']!.value,
      lastName: this.registerForm.controls['lastname']!.value,
      email: this.registerForm.controls['email']!.value,
      phonenumber: this.registerForm.controls['phonenumber']!.value,
      password: this.registerForm.controls['password']!.value,
    };
    this.registerSubscription = this.accountService
      .register(newUser)
      .subscribe(() => {
        this.changeButtonStatus();
        this.router.navigate(['/account/login'], {
          queryParams: { registered: 'true' },
        });
      });
  }

  changeButtonStatus() {
    this.loading = !this.loading;
  }

  // ngAfterViewInit(): void {
  //   setTimeout(() => this.setControlValuesFromNativeValues(), 200);
  // }
  //
  // setControlValuesFromNativeValues(): void {
  //   this.registerForm.get('username').setValue(this.username.nativeElement.value);
  //   this.registerForm.get('password').setValue(this.password.nativeElement.value);
  // }
}
