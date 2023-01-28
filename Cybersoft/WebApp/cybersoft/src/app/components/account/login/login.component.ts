import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
// import { AccountService, AlertService } from 'src/app/services/account';
import { Subscription } from 'rxjs';
import { logInFadeIn } from 'src/app/route-animations';
import { AccountService } from 'src/app/services/account';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  templateUrl: 'login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [logInFadeIn],
  //encapsulation: ViewEncapsulation.None,
})
export class LoginComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  loading = false;
  submitted = false;
  showErrorMessage = false;
  showAccountCreatedMessage = false;
  routeSubscription: Subscription;
  loginFlagSubscription!: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    // private router: Router,
    private accountService: AccountService,
    private toastService: ToastService,
  ) {
    this.routeSubscription = this.route.queryParams.subscribe((params) => {
      if (params['registered'] === 'true') {
        this.showAccountCreatedMessage = true;
      }
    });
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      username: ['', [Validators.required]], //, Validators.email
      password: ['', Validators.required],
    });
    this.showErrorMessage = false;
  }

  ngOnDestroy(): void {
    this.routeSubscription.unsubscribe();
    if (this.loginFlagSubscription) {
      this.loginFlagSubscription.unsubscribe();
    }
  }

  // convenience getter for easy access to form fields
  get loginForm() {
    return this.form.controls;
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    this.login();
  }

  login() {
    this.accountService.login(
      this.loginForm['username'].value,
      this.loginForm['password'].value
    );
    this.loading = true;
    this.submitted = true;
    this.loginFlagSubscription = this.accountService.logInFlag.subscribe(
      (status) => {
        this.changeStatus(status);
        this.setButtonStatus();
      }
    );
  }
  setButtonStatus() {
    this.loading = !this.showErrorMessage;
    this.submitted = !this.showErrorMessage;
  }

  changeStatus(status: boolean): void {
    this.showErrorMessage = status;
  }
}
