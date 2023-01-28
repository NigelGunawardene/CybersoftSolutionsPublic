import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import {
  catchError,
  map,
  tap,
  Observable,
  of,
  ignoreElements,
  Subscription,
} from 'rxjs';
import { PagedCollection } from 'src/app/models/paged-collection/paged-collection';
import { Product } from 'src/app/models/product/product';
import { UpsertProduct } from 'src/app/models/product/upsertProduct';
import { ProductService } from 'src/app/services/product/product.service';
import { StateService } from 'src/app/services/state/state.service';
import * as ProductsSelector from 'src/app/state/products/products.selectors';
@Component({
  selector: 'app-manage-products',
  templateUrl: './manage-products.component.html',
  styleUrls: ['./manage-products.component.scss'],
})
export class ManageProductsComponent implements OnInit {
  allProductsSubscription$!: Subscription;
  allProductsList: Product[] = [];
  public currentPage!: number;
  public pageSize: number = 10;
  public paginatedProducts: Product[] = [];
  public totalPages!: number;
  public totalItems!: number;

  loading = false;
  submitted = false;

  selectedProduct!: Product;
  editProductForm!: FormGroup;

  addProductForm!: FormGroup;

  constructor(
    private store: Store,
    private stateService: StateService,
    private productService: ProductService,
    private activeRoute: ActivatedRoute,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.activeRoute.queryParams.subscribe((params) => {
      this.currentPage = Number(params['page']) || 1;
    });

    this.editProductForm = this.formBuilder.group({
      productName: [''],
      productDescription: [''],
      productPrice: [''],
      productImageUrl: [''],
    });

    this.addProductForm = this.formBuilder.group({
      productName: [''],
      productDescription: [''],
      productPrice: [''],
    });

    this.getPaginatedProducts();
  }

  handlePageChange(event) {
    this.currentPage = event;
    this.getPaginatedProducts();
  }

  getPaginatedProducts() {
    this.allProductsSubscription$ = this.productService
      .getAllProductsPaginated(this.pageSize, this.currentPage)
      .subscribe({
        next: (pagedItems) => {
          (this.allProductsList = pagedItems.items),
            (this.currentPage = pagedItems.currentPage);
          this.totalPages = pagedItems.totalPages;
          this.totalItems = pagedItems.totalCount;
        },
      });
  }

  handleError(error: any) {
    window.alert('error');
  }

  assignCurrentProduct(product) {
    this.selectedProduct = product;
    // this.editProductFormProperties['productName'].setValue("asdasd")
    this.editProductForm.setValue({
      productName: this.selectedProduct?.name,
      productDescription: this.selectedProduct?.description,
      productPrice: this.selectedProduct?.price,
      productImageUrl: this.selectedProduct?.imageUrl,
    });
  }

  get editProductFormProperties() {
    return this.editProductForm.controls;
  }

  get addProductFormProperties() {
    return this.addProductForm.controls;
  }

  onEditSubmit() {
    let upsertProductItem: UpsertProduct = {
      id: this.selectedProduct.id,
      name: this.editProductFormProperties['productName'].value,
      description: this.editProductFormProperties['productDescription'].value,
      price: this.editProductFormProperties['productPrice'].value,
    };

    this.productService.upsertProduct(upsertProductItem).subscribe({
      next: (res) => {
        this.stateService.updateProducts(),
          this.getPaginatedProducts();
      },
    });
  }

  onAddSubmit() {
    let newProduct: Product = {
      id: 0,
      name: this.addProductFormProperties['productName'].value,
      description: this.addProductFormProperties['productDescription'].value,
      price: this.addProductFormProperties['productPrice'].value,
      quantity: 1,
      imageUrl: "",
      dateAdded: new Date(),
      isDeleted: false
    }

    this.productService.upsertProduct(newProduct).subscribe({
      next: (res) => {
        this.stateService.updateProducts(),
          this.getPaginatedProducts();
      },
    });
  }

  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe({
      next: res => {
        this.stateService.updateProducts(),
          this.getPaginatedProducts();
      }
    })
  }

  // closeModal() {
  //   this.formModal.hide();
  // }
}
