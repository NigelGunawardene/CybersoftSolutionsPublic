import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { ToastEvent } from 'src/app/models/toast/toast-event';
import { ToastEventTypes } from 'src/app/models/toast/toast-event-types';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  toastEvents: Observable<ToastEvent>;
  private _toastEvents = new Subject<ToastEvent>();

  constructor() {
    this.toastEvents = this._toastEvents.asObservable();
  }

  /**
   * Show success toast notification.
   * @param title Toast title
   * @param message Toast message
   */
   showSuccessToast(title: string, message: string) {
    this._toastEvents.next({
      message,
      title,
      type: ToastEventTypes.Success,
    });
  }

  /**
   * Show info toast notification.
   * @param title Toast title
   * @param message Toast message
   */
  showInfoToast(title: string, message: string) {
    this._toastEvents.next({
      message,
      title,
      type: ToastEventTypes.Info,
    });
  }

  /**
   * Show warning toast notification.
   * @param title Toast title
   * @param message Toast message
   */
  showWarningToast(title: string, message: string) {
    this._toastEvents.next({
      message,
      title,
      type: ToastEventTypes.Warning,
    });
  }

  /**
   * Show error toast notification.
   * @param title Toast title
   * @param message Toast message
   */
  showErrorToast(title: string, message: string) {
    this._toastEvents.next({
      message,
      title,
      type: ToastEventTypes.Error,
    });
  }
}
