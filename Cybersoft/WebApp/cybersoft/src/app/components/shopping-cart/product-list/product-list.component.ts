import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product/product.service';
import { Product } from 'src/app/models/product/product';
import { MessengerService } from '../../../services/messenger/messenger.service';
import { Store, select } from '@ngrx/store';
import * as ProductsSelector from 'src/app/state/products/products.selectors';
import { IAppState } from 'src/app/state/app.state';
import { Observable, map, Subscription } from 'rxjs';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
})
export class ProductListComponent implements OnInit {
  productsList$!: Observable<Product[]>;
  productsList!: Product[];
  backupProductsList!: Product[];
  searchText!: string;
  productListSubscription$! : Subscription;
  isMobileDevice: boolean = false

  constructor(
    private messageService: MessengerService,
    private store: Store<IAppState>,
  ) {}

  ngOnInit(): void {
    this.productsList$ = this.store.select(ProductsSelector.productsSelector);
    this.assignProducts();

    this.messageService.getSearchBarMessage().subscribe({
      next: (text) => {
        this.searchText = text;
        this.filterProductList();
      },
    });
    if (window.screen.width < 769){
      this.isMobileDevice = true;
    }
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
