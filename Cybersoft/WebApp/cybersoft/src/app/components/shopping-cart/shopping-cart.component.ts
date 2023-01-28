import { Component, OnInit } from '@angular/core';
import {RouterOutlet} from "@angular/router";

@Component({
  selector: 'app-shopping-cart',
  animations: [ // <-- add your animations here
    // fader,
    // slider,
    // transformer,
    //stepper
  ],
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})


export class ShoppingCartComponent implements OnInit {

  isMobileDevice: boolean = false

  constructor() { }

  ngOnInit(): void {
    if (window.screen.width < 769){
      this.isMobileDevice = true;
    }
  }

  // prepareRoute(outlet: RouterOutlet) {
  //   return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  // }

}
