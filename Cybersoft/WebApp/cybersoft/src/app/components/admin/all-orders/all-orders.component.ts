import {Component, OnInit, ViewChild} from '@angular/core';
import { OrderService } from 'src/app/services/order/order.service';
import { Order } from 'src/app/models/order/order';
import {MessengerService} from "../../../services/messenger/messenger.service";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {PagedCollection} from "../../../models/paged-collection/paged-collection";
// import {Product} from "../../models/product/product";
// import {ProductService} from "../../services/product/product.service";

@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html',
  styleUrls: ['./all-orders.component.scss']
})
export class AllOrdersComponent implements OnInit {

  @ViewChild('paginator', { static: true}) paginator!: MatPaginator;

  length!: number;
  pageSize = 10;
  pageIndex = 0;
  pageSizeOptions = [5, 10, 25];
  showFirstLastButtons = true;
  ordersWrapper!: PagedCollection<Order> | null;

  constructor(
    private orderService: OrderService,
    private messageService: MessengerService
  ) { }

  ngOnInit(): void {
    this.handleSubscription();
    this.getPaginatedOrders();
  }

  // ngAfterViewInit(): void {
  //   this.paginator.pageIndex = 1;
  // }


  handlePageEvent(event: PageEvent) {
    //console.log(event)
    this.length = event.length;
    this.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
    this.getPaginatedOrders();
  }

  // NOTE TO SELF - THE +1 AND -1 NEAR PAGE INDEX BELOW IS REQUirED FOR ANGULAR MATERIAL PAGINATION BECAUSE THE INDEX STARTS FROM 0. NOT A GREAT SOLUTION BUT REQUIRED.
  // BECAUSE INDEx STARTS FROM 0 AND PAGINATION STARTS FROM 1, WE ADD 1 WHEN WE SEND THE REQUEST AND GET THE CORRECT DATA, AND THEN WHEN SETTING THE CURRENT INDEX, WE SUBTRACT 1 AND KEEP ANGULAR HAPPY

  getPaginatedOrders() {
    this.orderService.getOrdersPaginated('active', this.pageSize, this.pageIndex + 1).subscribe((pagedItems) =>{
      this.ordersWrapper = pagedItems;
      this.length = pagedItems.totalCount
      this.pageSize = pagedItems.pageSize
      this.pageIndex = pagedItems.currentPage -1
    })
  }


  handleSubscription() {
    this.messageService.getStatusMessage().subscribe((status: boolean) => {
      this.getPaginatedOrders()
    })
  }
}
