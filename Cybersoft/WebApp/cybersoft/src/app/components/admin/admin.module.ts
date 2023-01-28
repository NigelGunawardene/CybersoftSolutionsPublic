import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { SubNavComponent } from 'src/app/components/admin/subnav/subnav.component';
import { LayoutComponent } from 'src/app/components/admin/layout/layout.component';
import { OverviewComponent } from 'src/app/components/admin/overview/overview.component';
import {AccountsModule} from "./accounts/accounts.module";
import { AllOrdersComponent } from "./all-orders/all-orders.component";
import { IndividualAllOrderComponent } from "./all-orders/individual-all-order/individual-all-order.component";
import { AllOrderItemsComponent } from "./all-orders/individual-all-order/all-order-items/all-order-items.component";
// import { PaginationComponent } from "../shared/pagination/pagination.component";
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';
import { MatCheckboxModule } from "@angular/material/checkbox"; //HAVE TO USE ng add @angular/material TO MAKE SURE STYLES ARE APPLIED CORRECTLY FOR MATERIAL COMPONENTS
import { MatRippleModule } from '@angular/material/core';
import { MatPaginatorModule } from "@angular/material/paginator";
import { ManageProductsComponent } from './manage-products/manage-products.component';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AdminRoutingModule,
        MatProgressSpinnerModule,
        MatDividerModule,
        MatCheckboxModule,
        MatRippleModule,
        MatPaginatorModule,
        NgxPaginationModule,
    ],
    declarations: [
        SubNavComponent,
        LayoutComponent,
        OverviewComponent,
        // PaginationComponent,
        AllOrdersComponent,
        IndividualAllOrderComponent,
        AllOrderItemsComponent,
        ManageProductsComponent
    ]
})
export class AdminModule { }
