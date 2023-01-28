import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Store } from '@ngrx/store';
import { Product } from '../../../models/product/product';
import { MessengerService } from '../../../services/messenger/messenger.service';
import { ProductService } from '../../../services/product/product.service';
import * as ProductActions from 'src/app/state/products/products.actions';
import * as ProductsSelector from 'src/app/state/products/products.selectors';
import { map, tap } from 'rxjs/operators';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss'],
})
export class SearchBarComponent implements OnInit, OnDestroy {
  defaultProductList: Product[] = [];
  filteredProductList: Product[] = [];
  searchText: string = 'Search';
  

  constructor(
    private messageService: MessengerService,
    private productService: ProductService,
    private store: Store
  ) {
    // this.store.dispatch(ProductActions.GetProducts());
  }

  ngOnInit(): void {
    // this.store.select(ProductsSelector.productsSelector).pipe(
    //     map((products) => (this.defaultProductList = products)));


    // this.productListSubscription$ = this.store
    //   .select(ProductsSelector.productsSelector)
    //   .subscribe({
    //     next: (res) => {
    //       this.defaultProductList = res;
    //       this.sendFilteredProductListOrDefault();
    //     },
    //   });
    // this.productListSubscription$ = this.store.select(ProductsSelector.productsSelector)
    // this.sendFilteredProductListOrDefault();
  }

  ngOnDestroy(): void {
    // this.productListSubscription$.unsubscribe();
  }

  // sendFilteredProductListOrDefault() {
  //   if (this.searchText === '' || this.searchText === 'Search') {
  //     this.messageService.sendSearchBarMessage(this.productListSubscription$);
  //   } else {
  //     this.messageService.sendSearchBarMessage(this.productListSubscription$.pipe(map(products => products.filter(items => items.name.toLocaleLowerCase().indexOf(this.searchText) !== -1))));
  //     // this.messageService.sendSearchBarMessage(this.filteredProductList);
  //   }
  // }

  // filterProducts(arg) {
  //   this.searchText = this.searchText.toLocaleLowerCase();
  //   this.filteredProductList = this.defaultProductList.filter(
  //     (product: Product) =>
  //       product.name.toLocaleLowerCase().indexOf(this.searchText) !== -1
  //   );
  //   this.sendFilteredProductListOrDefault();
  // }

  filterProducts(arg) {
    this.messageService.sendSearchBarMessage(this.searchText);
  }

  clearSearchBar() {
    if (this.searchText === 'Search') {
      this.searchText = '';
    }
  }
}
