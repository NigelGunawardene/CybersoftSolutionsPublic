import { ToastEventTypes } from './toast-event-types';

export interface ToastEvent {
  type: ToastEventTypes;
  title: string;
  message: string;
}