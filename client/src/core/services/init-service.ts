import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InitService {

  private accountService = inject(AccountService);
  init() {
    const accountString = localStorage.getItem('user');
    if (!accountString) {
      return of(null) ;
    }
    const user = JSON.parse(accountString);
    this.accountService.currentUser.set(user);
    return of(null);
  }

}
