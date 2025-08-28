import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterCreds, User } from '../../types/user';
import { tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);

  private baseUrl = environment.apiUrl
  login(email: string, password: string) {
    return this.http.post<User>(this.baseUrl + 'account/login', { email, password }).pipe(
      tap(user => {
        if (user) {
          this.setCurrentUser(user);
        }

      })
    );
  }
  register(creds: RegisterCreds) {
    return this.http.post<User>(this.baseUrl + 'account/register', creds).pipe(
      tap(user => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }
  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }


}
