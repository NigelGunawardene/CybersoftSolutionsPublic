import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { IAppState } from 'src/app/state/app.state';
import {Product} from "../../../models/product/product";
import {MessengerService} from "../../../services/messenger/messenger.service";
import * as ProductsSelector from 'src/app/state/products/products.selectors';


@Component({
  selector: 'app-view-products',
  templateUrl: './view-products.component.html',
  styleUrls: ['./view-products.component.scss']
})
export class ViewProductsComponent implements OnInit, OnDestroy {

  productsList$!: Observable<Product[]>;
  productsList!: Product[];
  backupProductsList!: Product[];
  searchText!: string;
  productListSubscription$! : Subscription;

  constructor(
    private messageService: MessengerService,
    private store: Store<IAppState>,
  ) { 
    this.productsList$ = this.store.select(ProductsSelector.productsSelector);
    this.assignProducts();

    this.messageService.getSearchBarMessage().subscribe({
      next: (text) => {
        this.searchText = text;
        this.filterProductList();
      },
    });
  }

  ngOnInit(): void {
    this.productsList$ = this.store.select(ProductsSelector.productsSelector);
    this.assignProducts();

    this.messageService.getSearchBarMessage().subscribe({
      next: (text) => {
        this.searchText = text;
        this.filterProductList();
      },
    });
  }

  assignProducts() {
    this.productListSubscription$ = this.productsList$.subscribe({
      next: (res) => {
        this.productsList = res;
        this.backupProductsList = res;
      },
    });
  }

  filterProductList() {
    if(this.searchText != '' && this.searchText != 'Search'){
      this.productsList = this.backupProductsList.filter(product => product.name.toLocaleLowerCase().indexOf(this.searchText) !== -1)
    }
    else{
      this.productsList = this.backupProductsList;
    }
  }

  ngOnDestroy(){
    if(this.productListSubscription$){
    this.productListSubscription$.unsubscribe();
    }
  }

}
