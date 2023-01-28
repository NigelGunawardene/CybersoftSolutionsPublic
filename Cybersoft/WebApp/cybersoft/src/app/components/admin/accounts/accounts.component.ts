import {Component, OnDestroy, OnInit, ViewEncapsulation} from '@angular/core';
import {Account} from "../../../models/account";
import {AccountService} from 'src/app/services/account/account.service';
// import { PaginationComponent } from "../../shared/pagination/pagination.component";
import {ActivatedRoute} from "@angular/router";
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.scss'],
  encapsulation: ViewEncapsulation.None
})


export class AccountsComponent implements OnInit, OnDestroy {

  // allAccountsList: Account[] = [];
  public currentPage!: number;
  public pageSize: number = 10;
  public paginatedAccounts! : Account[];
  public totalPages! : number;
  public totalItems! : number;

  private accountsSubscription$! : Subscription;
  private activeRouteSubscription$! : Subscription;

  constructor(
    private accountService : AccountService,
    private activeRoute : ActivatedRoute
  ) {}

  ngOnInit(): void {
    // this.populateAccountsList();
    this.activeRoute.queryParams.subscribe(params => {
      this.currentPage = Number(params['page']) || 1
      this.getPaginatedAccounts();
    })
  }

  handlePageChange(event){
    this.currentPage = event;
    this.getPaginatedAccounts()
  }

  getPaginatedAccounts() {
    this.accountService.getAllAccountsPaginated('customer', this.pageSize, this.currentPage).subscribe((pagedItems) =>{
      this.paginatedAccounts = pagedItems.items;
      this.currentPage = pagedItems.currentPage;
      this.totalPages = pagedItems.totalPages
      this.totalItems = pagedItems.totalCount
    })
  }

  // populateAccountsList(){
  //   this.accountService.getAllAccounts("Customer")
  //     .subscribe((accounts:Account[]) => {
  //       this.allAccountsList = accounts;
  //     });
  // }

  ngOnDestroy(): void {
    if(this.accountsSubscription$){
      this.accountsSubscription$.unsubscribe();
    }

    if(this.activeRouteSubscription$){
      this.activeRouteSubscription$.unsubscribe();
    }
  }
}
