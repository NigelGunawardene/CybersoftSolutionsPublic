import { Component, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from '../../services/account';
import { Account } from '../../models/account';
import { fadeInAnimation } from '../../route-animations';
import { Store } from '@ngrx/store';
import * as AuthSelector from 'src/app/state/auth/auth.selectors';
import {map } from 'rxjs/operators'
import { IUser } from 'src/app/state/common/interfaces/IUser';
import { Observable, Subscription } from 'rxjs';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss'],
  animations: [fadeInAnimation],
})
export class AccountComponent implements OnInit, OnDestroy {
  //authStoreSubscription!: Subscription;
  //loggedInUserModel1: IUser | null = null;
  loggedInUserModel : Observable<IUser | null>;

  constructor(private accountService: AccountService, private store: Store) {
    // this.store.select(AuthSelector.authSelector).pipe(map((res : IUser) => this.loggedInUserModel = res));
    this.loggedInUserModel = this.store.select(AuthSelector.authSelector);
    
    // this code works but using the above approach instead
    // this.authStoreSubscription = this.store.select(AuthSelector.authSelector).subscribe({
    //   next: (res) => this.loggedInUserModel = res
    // })
  }

  ngOnInit(): void {
    // let username = this.accountService.getLoggedInUserDetails('name');
    // this.accountService.getByUserName(username).subscribe((user) => {
    //   this.loggedInUserModel = user;
    // });
  }

  ngOnDestroy(): void {
    //this.authStoreSubscription.unsubscribe();
  }
}
