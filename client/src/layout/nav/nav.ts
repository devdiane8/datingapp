import { Component, inject, OnInit, signal } from '@angular/core';
import { AccountService } from '../../core/services/account-service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastService } from '../../core/services/toast-service';
import { CommonModule } from '@angular/common';
import { themes } from '../theme';
import { BusyService } from '../../core/services/busy-service';
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav implements OnInit {

  protected accountService = inject(AccountService);
  private router = inject(Router);
  private toast = inject(ToastService);
  protected selectedTheme = signal<string>(localStorage.getItem('theme') || 'light');
  protected themes = themes;
  protected busyService = inject(BusyService);

  protected creds: any = {};

  ngOnInit(): void {
    document.documentElement.setAttribute('data-theme', this.selectedTheme());
  }

  handleSelectTheme(theme: string) {
    this.selectedTheme.set(theme);
    localStorage.setItem('theme', theme);
    document.documentElement.setAttribute('data-theme', theme);
    const element = document.activeElement as HTMLDivElement;
    if (element) {
      element.blur();
    }
  }



  login() {
    this.accountService.login(this.creds.email, this.creds.password).subscribe(
      {
        next: result => {
          this.router.navigateByUrl('/members');
          this.toast.success('Login successful');
          this.creds = {};
        },
        error: error => {
          this.toast.error(error.error);
        },
      }
    );
  }
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
