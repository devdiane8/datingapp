import { Component, inject, signal } from '@angular/core';
import { AccountService } from '../../core/services/account-service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {

  protected accountService = inject(AccountService);

  protected creds: any = {};

  login() {
    console.log("test", this.creds);
    this.accountService.login(this.creds.email, this.creds.password).subscribe(
      {
        next: result => {
          console.log(result);
          this.creds = {};
        },
        error: error => alert(error),
      }
    );
  }
  logout() {
    this.accountService.logout();
  }

}
