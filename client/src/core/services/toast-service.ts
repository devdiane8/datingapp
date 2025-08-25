import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor() {
    this.createToastContainer();
  }

  private createToastContainer() {
    if (!document.getElementById('.toast-container')) {
      const container = document.createElement('div');
      container.id = 'toast-container';
      container.className = 'toast toast-bottom toast-end';
      document.body.appendChild(container);
    }

  }

  private createToastElement(message: string, alertClass: string, duration = 1000) {

    const toastContainner = document.getElementById('toast-container');
    if (!toastContainner) {
      console.error('Toast container not found');
      return;
    }
    const toast = document.createElement('div');
    toast.classList.add('alert', alertClass, 'shadow-lg');
    toast.innerHTML = `
      <span>${message}</span>
      <button class="ml-4 btn btn-sm btn-ghost">X</button>
    `;
    toast.querySelector('button')?.addEventListener('click', () => {
      toastContainner?.removeChild(toast);
    });

    toastContainner.appendChild(toast);
    setTimeout(() => {
      toastContainner?.removeChild(toast);
    }, duration);
  }
  success(message: string, duration = 500) {
    this.createToastElement(message, 'alert-success', duration);
  }
  error(message: string, duration = 500) {
    this.createToastElement(message, 'alert-error', duration);
  }
  warning(message: string, duration = 500) {
    this.createToastElement(message, 'alert-warning', duration);
  }
  info(message: string, duration = 500) {
    this.createToastElement(message, 'alert-info', duration);
  }

}
