import {Component, HostListener, OnInit} from '@angular/core';
import { Store } from '@ngrx/store';
import {fadeOut} from "../../../route-animations";
import * as AuthSelector from 'src/app/state/auth/auth.selectors';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/state/common/interfaces/IUser';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  animations: [fadeOut]
})
export class HeaderComponent implements OnInit {

  loggedInUserModel$!: Observable<IUser | null>;

  constructor(private store: Store) { }

  scrolled = 0;

  @HostListener('window:scroll', ['$event'])
  onWindowScroll($event) {
    const numb = window.scrollY;
    if (numb >= 100 && window.screen.width < 769){
      this.scrolled = 1;
    }
    else{
      this.scrolled = 0;
    }
  }


  ngOnInit(): void {
    this.loggedInUserModel$ = this.store.select(AuthSelector.authSelector);
  }

}
