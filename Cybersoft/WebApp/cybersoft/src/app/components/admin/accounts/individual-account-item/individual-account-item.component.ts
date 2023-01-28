import {Component, Input, OnInit} from '@angular/core';
import {Order} from "../../../../models/order/order";
import {Account} from "../../../../models/account";

@Component({
  selector: 'app-individual-account-item',
  templateUrl: './individual-account-item.component.html',
  styleUrls: ['./individual-account-item.component.scss']
})
export class IndividualAccountItemComponent implements OnInit {


  @Input() individualAccountItem!: Account;
  constructor() { }

  ngOnInit(): void {
  }

}
