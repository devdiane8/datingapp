import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Nav } from "../layout/nav/nav";
import { AccountService } from '../core/services/account-service';
import { Home } from "../features/home/home";

@Component({
  selector: 'app-root',
  imports: [Nav, Home],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  protected readonly title = "Dating App";
  protected members = signal<any[]>([]);

  async ngOnInit() {
    this.members.set(await this.getMembers());
  }

  setCurrentUser() {
    const accountString = localStorage.getItem('user');
    if (!accountString) {
      return;
    }
    const account = JSON.parse(accountString);
    this.accountService.currentUser.set(account);
  }



  async getMembers() {
    try {
      return lastValueFrom(this.http.get<any[]>('https://localhost:5001/api/members'));
    } catch (err) {
      console.log(err);
      return [];
    }
  }

}
