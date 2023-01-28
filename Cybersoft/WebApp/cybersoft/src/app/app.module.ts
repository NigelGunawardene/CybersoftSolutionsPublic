import { NgModule, APP_INITIALIZER, ModuleWithProviders } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { NavComponent } from './components/shared/nav/nav.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { FiltersComponent } from './components/shopping-cart/filters/filters.component';
import { ProductListComponent } from './components/shopping-cart/product-list/product-list.component';
import { CartComponent } from './components/shopping-cart/cart/cart.component';
import { CartItemComponent } from './components/shopping-cart/cart/cart-item/cart-item.component';
import { ProductItemComponent } from './components/shopping-cart/product-list/product-item/product-item.component';
import { TokenInterceptorService } from 'src/app/services/token-interceptor/token-interceptor.service';
import { AuthGuard } from 'src/app/helpers/auth.guard';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { CheckoutItemComponent } from './components/checkout/checkout-item/checkout-item.component';
import { OrdersComponent } from './components/orders/orders.component';
import { IndividualOrderComponent } from './components/orders/individual-order/individual-order.component';
import { OrderItemsComponent } from './components/orders/individual-order/order-items/order-items.component';
import { FaqComponent } from './components/shared/faq/faq.component';
import { CdkAccordionModule } from '@angular/cdk/accordion';
import { AccordionModule } from './components/shared/accordion/accordion.module';
import { PrivacyPolicyComponent } from './components/shared/privacy-policy/privacy-policy.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SearchBarComponent } from './components/shopping-cart/search-bar/search-bar.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ViewProductsComponent } from './components/shared/view-products/view-products.component';
import { ViewProductsItemsComponent } from './components/shared/view-products/view-products-items/view-products-items.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { AccountService } from './services/account';
import { StoreModule } from '@ngrx/store';
import { AuthReducer } from './state/auth/auth.reducer';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import { ProductsEffects } from './state/products/products.effects';
import { ProductsReducer } from './state/products/products.reducer';
import { AppEffects, AppReducers } from './state/app.state';
import { AuthEffects } from './state/auth/auth.effects';
import { ToasterComponent } from './components/shared/toast/toaster/toaster.component';
import { ToastComponent } from './components/shared/toast/toast/toast.component';
import { ScrollingModule } from '@angular/cdk/scrolling';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavComponent,
    ShoppingCartComponent,
    FiltersComponent,
    ProductListComponent,
    CartComponent,
    CartItemComponent,
    ProductItemComponent,
    CheckoutComponent,
    CheckoutItemComponent,
    OrdersComponent,
    IndividualOrderComponent,
    OrderItemsComponent,
    FaqComponent,
    PrivacyPolicyComponent,
    SearchBarComponent,
    ViewProductsComponent,
    ViewProductsItemsComponent,
    ToasterComponent,
    ToastComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    AccordionModule,
    CdkAccordionModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    ModalModule,
    NgxPaginationModule,
    ScrollingModule,


    StoreModule.forRoot(AppReducers), //StoreModule.forRoot({ auth: AuthReducer }),
    EffectsModule.forRoot(AppEffects),

    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: false,
      autoPause: true,
      features: {
        pause: false,
        lock: true,
        persist: true,
      },
    }),
  ],
  providers: [
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },

  ],
  bootstrap: [AppComponent],
})
export class AppModule {

}

