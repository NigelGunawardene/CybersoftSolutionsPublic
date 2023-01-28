import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SubNavComponent } from 'src/app/components/admin/subnav/subnav.component';
import { LayoutComponent } from 'src/app/components/admin/layout/layout.component';
import { OverviewComponent } from 'src/app/components/admin/overview/overview.component';
import {FaqComponent} from "../shared/faq/faq.component";
import {AllOrdersComponent} from "./all-orders/all-orders.component";
import { ManageProductsComponent } from './manage-products/manage-products.component';

const accountsModule = () => import('./accounts/accounts.module').then(x => x.AccountsModule);

const routes: Routes = [
    { path: '', component: SubNavComponent, outlet: 'subnav' },
    {
        path: '', component: LayoutComponent,
        children: [
            { path: '', component: OverviewComponent },
            { path: 'orders', component: AllOrdersComponent },
            { path: 'accounts', loadChildren: accountsModule },
            { path: 'manage-products', component: ManageProductsComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
