import { Injectable, Inject, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, tap } from 'rxjs';
import { map, finalize, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Account, RefreshTokenModel } from 'src/app/models/account';
import { User } from 'src/app/models/user/user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { MessengerService } from '../messenger/messenger.service';
import { CookieService } from 'ngx-cookie-service';
import { Product } from '../../models/product/product';
import { PagedCollection } from '../../models/paged-collection/paged-collection';
import { Store } from '@ngrx/store';
import * as AuthActions from 'src/app/state/auth/auth.actions';
import { IUser } from 'src/app/state/common/interfaces/IUser';
import { StateService } from '../state/state.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private accountSubject: BehaviorSubject<Account>;
  public account: Observable<Account>;
  // private authTokensSubject: BehaviorSubject<RefreshTokenModel>;
  // public authTokens: Observable<RefreshTokenModel>;
  private readonly apiPath: string = 'api/user';

  @Output() getLoggedInUserRole: EventEmitter<any> = new EventEmitter();
  @Output() logInFlag: EventEmitter<any> = new EventEmitter();

  constructor(
    private router: Router,
    private http: HttpClient,
    private messageService: MessengerService,
    private cookieService: CookieService,
    private store: Store,
    private stateService: StateService,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.accountSubject = new BehaviorSubject<Account>({} as any); // replaced null with {} as any
    this.account = this.accountSubject.asObservable();
    // this.authTokensSubject = new BehaviorSubject<RefreshTokenModel>({} as any); // replaced null with {} as any
    // this.authTokens = this.authTokensSubject.asObservable();
  }

  public get accountValue(): Account {
    return this.accountSubject.value;
  }

  login(username: string, password: string) {
    var response = this.http
      .post<any>(
        `${this.baseUrl}${this.apiPath}/authenticate`,
        { username, password },
        { withCredentials: true }
      )
      .subscribe({
        next: (res) => this._handleLoginSucceess(res),
        error: this._handleLoginFailure.bind(this),
      });
  }

  private _handleLoginSucceess(res) {
    this.storeTokens(res);
    this.getLoggedInUserRole.emit(this.getDetailOfLoggedInUser('role'));
    this.logInFlag.emit(false);
    this._addCurrentUserToState();

    this.router.navigate(['/shop']);
  }

  private _handleLoginFailure() {
    this.logInFlag.emit(true);
    this.logout();
  }

  private _addCurrentUserToState() {
    this.store.dispatch(AuthActions.LoginUser());
    this.stateService.updateCartAndOrders();
  }

  logout() {
    this.deleteTokens();
    this.getLoggedInUserRole.emit(null);
    this.store.dispatch(AuthActions.LogoutUser());
    this.router.navigate(['/account/login']);
  }

  refreshToken() {
    let model: RefreshTokenModel = {
      jwtToken: this.getJwtToken() as string,
      refreshToken: this.getRefreshToken() as string,
    };
    return this.http
      .post<any>(`${this.baseUrl}${this.apiPath}/refresh-token`, model)
      .pipe(
        tap((tokens: RefreshTokenModel) => {
          this.storeTokens(tokens);
        })
      );
  }

  storeTokens(res) {
    localStorage.setItem('token', res.jwtToken);
    localStorage.setItem('refreshToken', res.refreshToken);
  }

  deleteTokens() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
  }

  storeExpirationDate() {
    localStorage.setItem('expiration', this.getDetailOfLoggedInUser('exp'));
  }

  register(user: User) {
    return this.http.post(`${this.baseUrl}${this.apiPath}/register`, user);
  }

  getJwtToken() {
    return localStorage.getItem('token');
  }

  getRefreshToken() {
    return localStorage.getItem('refreshToken');
  }

  isUserLoggedIn() {
    try {
      if (
        localStorage.getItem('token') != null &&
        localStorage.getItem('refreshToken') != null
      ) {
        return true;
      } else {
        return false;
      }
    } catch (e) {
      console.log(e);
      return false;
    }
  }

  getDetailOfLoggedInUser(detail: string) {
    try {
      const helper = new JwtHelperService();
      const decodedToken = helper.decodeToken(localStorage.getItem('token')!);
      switch (detail) {
        case 'id': {
          return 'id'; // implement this later
          break;
        }
        case 'name': {
          return decodedToken.unique_name;
          break;
        }
        case 'role': {
          if (decodedToken) {
            return decodedToken?.role;
          } else {
            return null;
          }
          break;
        }
        case 'exp': {
          return decodedToken.exp;
          break;
        }
        default: {
          return 'not implemented';
          break;
        }
      }
    } catch (e) {
      console.log(e);
    }
  }

  getLoggedInUserState() {
    // let username = this.getLoggedInUserDetails('name');
    return this.getByMe().pipe(
      map((res: Account) => {
        const user = {
          userName: res.userName,
          firstName: res.firstName,
          lastName: res.lastName,
          email: res.email,
          phoneNumber: res.phoneNumber,
        } as IUser;
        return user;
      })
    );
  }

  getAllAccountsPaginated(
    accountType: string,
    pagesize: number,
    pagenumber: number
  ) {
    return this.http
      .get<PagedCollection<Account>>(
        `${this.baseUrl}${this.apiPath}/paginated/?accountType=${accountType}&pagesize=${pagesize}&pagenumber=${pagenumber}`
      )
      .pipe(catchError(this.handleError<PagedCollection<Account>>()));
  }

  // getAllAccounts(accountType: string) {
  //   return this.http.get<Account[]>(
  //     `${this.baseUrl}${this.apiPath}/all/${accountType}`
  //   );
  // }

  getById(id: string) {
    return this.http.get<Account>(`${this.baseUrl}${this.apiPath}/${id}`);
  }

  getByUserName(username: string) {
    return this.http.get<Account>(`${this.baseUrl}${this.apiPath}/${username}`);
  }

  getByMe() {
    return this.http.get<Account>(`${this.baseUrl}${this.apiPath}/me`);
  }


  checkUsername(username: string): Observable<Boolean> {
    return this.http.get<Boolean>(
      `${this.baseUrl}${this.apiPath}/checkusername/${username}`
    );
  }

  checkEmail(email: string): Observable<Boolean> {
    return this.http.get<Boolean>(
      `${this.baseUrl}${this.apiPath}/checkemail/${email}`
    );
  }

  checkPhoneNumber(phonenumber: string): Observable<Boolean> {
    return this.http.get<Boolean>(
      `${this.baseUrl}${this.apiPath}/checkphonenumber/${phonenumber}`
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}
