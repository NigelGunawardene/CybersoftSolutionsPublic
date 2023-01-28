import { Component, OnDestroy } from '@angular/core';
import { AccountService } from 'src/app/services/account/account.service';
import { RouterOutlet } from '@angular/router';
import { fader, slider, stepper, transformer } from './route-animations';
import { Store } from '@ngrx/store';
import * as AuthSelector from 'src/app/state/auth/auth.selectors';
import { skipWhile, tap, takeUntil } from 'rxjs/operators';
import { select } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { StatusEnums } from './state/common/enums/StatusEnums';
import * as ProductActions from 'src/app/state/products/products.actions';
import * as AuthActions from 'src/app/state/auth/auth.actions';
import { StateService } from './services/state/state.service';

@Component({
  selector: 'app-root',
  animations: [
    // <-- add your animations here
    fader,
    slider,
    //transformer,
    //stepper
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnDestroy {
  title = 'cybersoft';
  userLoggedInStatus$;
  hasUserLoggedInSubscription$!: Subscription;

  prepareRoute(outlet: RouterOutlet) {
    return (
      outlet &&
      outlet.activatedRouteData &&
      outlet.activatedRouteData['animation']
    );
  }

  constructor(
    private stateService: StateService,
    private store: Store,
    private accountService: AccountService
  ) {
    this._loadRelevantData();
  }

  ngOnDestroy(): void {
    this.hasUserLoggedInSubscription$.unsubscribe();
  }

  private _loadRelevantData() {
    this._loadProducts();
    if (this.accountService.isUserLoggedIn()) {
      this._tryLoadUser();
    }
  }

  private _loadProducts() {
    this.store.dispatch(ProductActions.GetProducts());
  }

  private _tryLoadUser() {
    this._autoLogIn();
    this.hasUserLoggedInSubscription$ = this.store
      .pipe(
        select(AuthSelector.authStatusSelector)
        // takeUntil(this.destroy$),
      )
      .subscribe((data) => {
        this.userLoggedInStatus$ = data;
        if (this.userLoggedInStatus$ == StatusEnums.SUCCESS) {
          this.stateService.updateCartAndOrders();
          // this.store.dispatch(CartActions.GetCart());
          // this.store.dispatch(OrdersActions.GetOrders());
          //populate profile, cart and orders
        }
      });
  }

  private _autoLogIn() {
    this.store.dispatch(AuthActions.LoginUser());
  }
}
