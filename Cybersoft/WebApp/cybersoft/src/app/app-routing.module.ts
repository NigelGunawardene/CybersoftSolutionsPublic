import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { AuthGuard } from 'src/app/helpers/auth.guard';
import { CheckoutComponent } from "./components/checkout/checkout.component";
import { OrdersComponent } from "./components/orders/orders.component";
import { FaqComponent } from "./components/shared/faq/faq.component";
import { PrivacyPolicyComponent } from "./components/shared/privacy-policy/privacy-policy.component";
import {ViewProductsComponent} from "./components/shared/view-products/view-products.component";

//import { HomeComponent } from './home';
//import { AuthGuard } from './_helpers';
//import { Role } from './models';

const accountModule = () => import('src/app/components/account/account.module').then(x => x.AccountModule);
const adminModule = () => import('src/app/components/admin/admin.module').then(x => x.AdminModule);
const profileModule = () => import('src/app/components/profile/profile.module').then(x => x.ProfileModule);

const routes: Routes = [
  //{ path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/shop', pathMatch: 'full' },
  { path: 'viewproducts', component: ViewProductsComponent , data: { animation: 'isRight' }  },
  { path: 'faq', component: FaqComponent , data: { animation: 'isRight' }  },
  { path: 'privacy', component: PrivacyPolicyComponent },
  { path: 'shop', component: ShoppingCartComponent, canActivate: [AuthGuard], data: { animation: 'isLeft' } },
  { path: 'checkout', component: CheckoutComponent, canActivate: [AuthGuard], data: { animation: 'isRight' }  },
  { path: 'orders', component: OrdersComponent, canActivate: [AuthGuard] , data: { animation: 'isRight' }  },
  { path: 'account', loadChildren: accountModule },
  { path: 'admin', loadChildren: adminModule },
  { path: '**', redirectTo: '/account/login', pathMatch: 'full' },


  //{ path: 'profile', loadChildren: profileModule, canActivate: [AuthGuard] },
  //{ path: 'admin', loadChildren: adminModule, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],//, {onSameUrlNavigation: 'reload'}
  exports: [RouterModule]
})
export class AppRoutingModule { }

