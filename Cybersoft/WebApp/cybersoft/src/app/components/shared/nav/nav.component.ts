import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/services/account/account.service';
import { MessengerService } from 'src/app/services/messenger/messenger.service';
import * as CartSelector from 'src/app/state/cart/cart.selectors';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit, OnDestroy {

  currentUserRole: string = "";
  isUserLoggedIn: boolean = false;
  //loggedInUserName: string = "";
  numberOfItemsInCart: number = 0;

  numberOfCartItemsSubscription$! : Subscription;

  constructor(
    public accountService: AccountService,
    private store: Store,
    //private messageService: MessengerService
  ) {
    accountService.getLoggedInUserRole.subscribe(role => this.changeRole(role));
  }

  ngOnInit(): void {
     let userRole = this.accountService.getDetailOfLoggedInUser("role");
     if(userRole !== null){
       //this.currentUserRole = userRole;
       //this.loggedInUserName = this.accountService.getDetailOfLoggedInUser("name");
       this.changeRole(userRole)
     }
     this.handleSubscription();
  }

  changeRole(role: string): void {
    this.currentUserRole = role;
  }

  handleSubscription(){
    this.numberOfCartItemsSubscription$ = this.store.select(CartSelector.selectAllCartItems).subscribe(cartItems =>{
     if(cartItems.length > 0) {
        this.numberOfItemsInCart = cartItems.length
      }
      else{
        this.numberOfItemsInCart = 0
      }
    })
    // this.messageService.getNumberOfCartItemsMessage().subscribe( num => {
    //   if(num > 0) {
    //     this.numberOfItemsInCart = num
    //   }
    //   else{
    //     this.numberOfItemsInCart = 0
    //   }
    // })
  }

  ngOnDestroy(): void {
      this.numberOfCartItemsSubscription$.unsubscribe();
  }
}
