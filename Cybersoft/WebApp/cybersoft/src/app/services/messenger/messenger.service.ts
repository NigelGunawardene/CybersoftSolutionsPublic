import { Injectable } from '@angular/core';
import {BehaviorSubject, Subject} from 'rxjs';
import {Product} from "../../models/product/product";


@Injectable({
  providedIn: 'root'
})
export class MessengerService {

  subject = new Subject();
  searchBarSubject= new Subject<string>();
  productSubject= new Subject<Product>();
  statusSubject= new Subject<boolean>();
  cartItemIdSubject= new Subject<number>();
  numberOfCartItemsSubject= new Subject<number>();
  //newAccountSubject= new Subject<boolean>();
  //public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  //isUserLoggedin = new BehaviorSubject<boolean>(false);
  constructor() { }

  sendMessage(message) {
    this.subject.next(message) //triggering an event
  }

  getMessage() {
    return this.subject.asObservable()
  }

  sendSearchBarMessage(message) {
    this.searchBarSubject.next(message) //triggering an event
  }

  getSearchBarMessage() {
    return this.searchBarSubject.asObservable()
  }

  // sendProductMessage(message) {
  //   this.productSubject.next(message) //triggering an event
  // }

  // getProductMessage() {
  //   return this.productSubject.asObservable()
  // }

  sendStatusMessage(message) {
    this.statusSubject.next(message) //triggering an event
  }

  getStatusMessage() {
    return this.statusSubject.asObservable()
  }

  // sendCartItemIdMessage(message) {
  //   this.cartItemIdSubject.next(message) //triggering an event
  // }

  // getCartItemIdMessage() {
  //   return this.cartItemIdSubject.asObservable()
  // }

  // sendNumberOfCartItemsMessage(message) {
  //   this.numberOfCartItemsSubject.next(message) //triggering an event
  // }

  // getNumberOfCartItemsMessage() {
  //   return this.numberOfCartItemsSubject.asObservable()
  // }

  // sendNewAccountMessage(message) {
  //   this.newAccountSubject.next(message) //triggering an event
  // }
  //
  // getNewAccountMessage() {
  //   return this.newAccountSubject.asObservable()
  // }
}
