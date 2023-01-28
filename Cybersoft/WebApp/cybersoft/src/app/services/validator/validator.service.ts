import { Injectable } from '@angular/core';
import {AccountService} from "../account";
import {AbstractControl, ValidationErrors} from "@angular/forms";
import {debounceTime, distinctUntilChanged, Observable, of, switchMapTo, tap} from "rxjs";
import {map, take} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class ValidatorService {

  static checkUsername(accountService: AccountService) {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      if (!control.valueChanges || control.pristine) {
        return of(null);
      } else {
        return control.valueChanges.pipe(
          debounceTime(300),
          distinctUntilChanged(),
          take(1),
          switchMapTo(accountService.checkUsername(control.value)),
          tap(() => control.markAsTouched()),
          map((data) => (data ? null : { usernameInUse: true } ))
        );
      }
    };
  }

  static checkEmail(accountService: AccountService) {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      if (!control.valueChanges || control.pristine) {
        return of(null);
      } else {
        return control.valueChanges.pipe(
          debounceTime(800),
          distinctUntilChanged(),
          take(1),
          switchMapTo(accountService.checkEmail(control.value)),
          tap(() => control.markAsTouched()),
          map((data) => (data ? null : { emailInUse: true } ))
        );
      }
    };
  }

  static checkPhoneNumber(accountService: AccountService) {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      if (!control.valueChanges || control.pristine) {
        return of(null);
      } else {
        return control.valueChanges.pipe(
          debounceTime(800),
          distinctUntilChanged(),
          take(1),
          switchMapTo(accountService.checkPhoneNumber(control.value)),
          tap(() => control.markAsTouched()),
          map((data) => (data ? null : { phonenumberInUse: true } ))
        );
      }
    };
  }

  static cannotContainSpace(control: AbstractControl) : ValidationErrors | null {
    if((control.value as string).indexOf(' ') >= 0){
      return {cannotContainSpace: true}
    }
    return null;
  }

  static cannotContainPlus(control: AbstractControl) : ValidationErrors | null {
    if((control.value as string).indexOf('+') >= 0){
      return {cannotContainPlus: true}
    }
    return null;
  }

  static cannotContainSymbols(control: AbstractControl) : ValidationErrors | null {
    if(((control.value as string).indexOf('+') >= 0) || ((control.value as string).indexOf('-') >= 0) ){
      return {cannotContainSymbols: true}
    }
    return null;
  }

}
