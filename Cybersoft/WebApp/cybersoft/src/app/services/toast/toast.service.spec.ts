import { TestBed } from '@angular/core/testing';
import { ToastEvent } from 'src/app/models/toast/toast-event';
import { ToastEventTypes } from 'src/app/models/toast/toast-event-types';
import { ToastService } from './toast.service';

describe('ToastService', () => {
  let service: ToastService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ToastService);
  });

  it('should fire success toast event', (done) => {
    // given
    const title = 'some title for success toast';
    const message = 'some message for success toast';

    service.toastEvents.subscribe((event) => {
      // then
      expect(event).toEqual({ type: ToastEventTypes.Success, title, message });
      done();
    });

    // when
    service.showSuccessToast(title, message);
  });

  it('should fire info toast event', (done) => {
    // given
    const title = 'some title for info toast';
    const message = 'some message for info toast';

    service.toastEvents.subscribe((event) => {
      // then
      expect(event).toEqual({ type: ToastEventTypes.Info, title, message });
      done();
    });

    // when
    service.showInfoToast(title, message);
  });

  it('should fire warning toast event', (done) => {
    // given
    const title = 'some title for warning toast';
    const message = 'some message for warning toast';

    service.toastEvents.subscribe((event) => {
      // then
      expect(event).toEqual({ type: ToastEventTypes.Warning, title, message });
      done();
    });

    // when
    service.showWarningToast(title, message);
  });

  it('should fire error toast event', (done) => {
    // given
    const title = 'some title for error toast';
    const message = 'some message for error toast';

    service.toastEvents.subscribe((event) => {
      // then
      expect(event).toEqual({ type: ToastEventTypes.Error, title, message });
      done();
    });

    // when
    service.showErrorToast(title, message);
  });
});