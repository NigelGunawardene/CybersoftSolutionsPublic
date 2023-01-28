import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import {catchError, filter, take, switchMap, finalize, retry} from 'rxjs/operators';
import {AccountService} from "../account";
import {Router} from "@angular/router";
import { ToastService } from '../toast/toast.service';

@Injectable()
export class TokenInterceptorService implements HttpInterceptor {

  private isRefreshing = false;
  private authTokensSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(private accountService: AccountService, private router: Router, private toastService: ToastService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
    // It's an auth request, don't get a token
    if (request.url.toLowerCase().includes('/authenticate')) {
      return next.handle(request.clone());
    }

    if (this.accountService.getJwtToken()) {
      request = this.addToken(request, this.accountService.getJwtToken()!);
    }
    return next.handle(request).pipe(catchError(error => {

      if (error instanceof HttpErrorResponse && error.status === 401) {
        return this.handle401Error(request, next);
      } 
      if (error instanceof HttpErrorResponse && error.status === 403) {
        return this.handle403Error(request, next);
      } 
      if (error instanceof HttpErrorResponse && error.status >= 500) {
        return this.handle500Error(request, next);
      } 
      else {
        this.accountService.logout();
        // location.reload();
        return throwError(() => error);
      }
    }));
  }

  
  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        'Authorization': `Bearer ${token}`
      }
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.authTokensSubject.next(null);

      return this.accountService.refreshToken().pipe(
        switchMap((refreshedTokens: any) => {
          if (refreshedTokens != null) {
            this.authTokensSubject.next(refreshedTokens.jwtToken);
            this.isRefreshing = false;
            return next.handle(this.addToken(request, refreshedTokens.jwtToken));
          }
            else{
            this.accountService.logout();
            location.reload();
            return throwError(() => refreshedTokens);
            }
          }), finalize(() => {
            this.isRefreshing = false;
          }));

    } else {
      return this.authTokensSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(jwt => {
          return next.handle(this.addToken(request, jwt));
        }));
    }
  }

  private handle403Error(request: HttpRequest<any>, next: HttpHandler) {
    this.accountService.logout();
    this.toastService.showWarningToast("Unauthorized", "Unauthorized");
    return throwError(() => "Unauthorized");
  }

  private handle500Error(request: HttpRequest<any>, next: HttpHandler) {
    this.toastService.showWarningToast("Error", "Sorry, an error occured, please try again");
    return throwError(() => "Error");
  }
}
