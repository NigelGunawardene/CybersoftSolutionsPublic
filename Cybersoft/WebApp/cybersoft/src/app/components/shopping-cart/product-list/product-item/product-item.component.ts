import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Product } from 'src/app/models/product/product';
import { MessengerService } from 'src/app/services/messenger/messenger.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { CartItem } from '../../../../models/cart/cart-item';
import { addToCartAnimation, fadeOut } from 'src/app/route-animations';
import { Store } from '@ngrx/store';
import * as CartActions from 'src/app/state/cart/cart.actions';
import * as CartSelector from 'src/app/state/cart/cart.selectors';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss'],
  animations: [], //addToCartAnimation
})
export class ProductItemComponent implements OnInit, OnDestroy {
  @Input() productItem!: Product;

  addingToCartAnimation: boolean = true;
  defaultAddToCartButtonText: string = 'Add to cart';
  addToCartButtonText: string = this.defaultAddToCartButtonText;

  inputQuantity = 1;
  loading = false;
  format = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/;

  itemAddedInProgressSubscription$!: Subscription;

  constructor(
    private messageService: MessengerService,
    private cartService: CartService,
    private store: Store
  ) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    if (this.itemAddedInProgressSubscription$) {
      this.itemAddedInProgressSubscription$.unsubscribe();
    }
  }

  handleAddToCart() {
    this.addingToCartAnimation = !this.addingToCartAnimation; // this is for animation, ignore it for functionality

    if (this.format.test(this.inputQuantity.toString())) {
      this.inputQuantity = 1;
    } else if (!isNaN(this.inputQuantity)) {
      this.addToCartButtonClicked();
      // this.cartService.addProductToCart(this.convertProductToCartItem(this.productItem)).subscribe(() => {
      //   this.messageService.sendProductMessage(this.productItem);
      this.store.dispatch(
        CartActions.AddToCartCommand({
          cartItem: this.convertProductToCartItem(this.productItem),
        })
      );
      this.inputQuantity = 1;
      // this.changeButtonStatus();
      this.itemAddedInProgressSubscription$ = this.store
        .select(CartSelector.selectLoadingState)
        .subscribe((loadingState) => {
          let temporaryLoadingChecker = loadingState!;
          if (!temporaryLoadingChecker) {
            this.addToCartCompleted();
          }
        });
      //})
    } else {
      this.inputQuantity = 1;
    }
  }

  convertProductToCartItem(product: Product): CartItem {
    let addCartItem: any = {
      //id: 1,
      userId: 1,
      productId: product.id,
      productName: product.name,
      quantity: this.inputQuantity,
      price: product.price,
    };
    return addCartItem;
  }

  addToCartButtonClicked() {
    this.loading = true;
    this.addToCartButtonText = '';
  }

  addToCartCompleted() {
    this.loading = false;
    this.addToCartButtonText = this.defaultAddToCartButtonText;
  }

  // changeButtonStatus(){
  //   this.loading = !this.loading;
  // }
  // plus()
  // {
  //   this.inputQuantity = this.inputQuantity+1;
  // }
  // minus() {
  //   if (this.inputQuantity != 0) {
  //     this.inputQuantity = this.inputQuantity - 1;
  //   }
  // }
}
