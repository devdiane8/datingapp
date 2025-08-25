import { Component, inject, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterCreds } from '../../../types/user';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  protected creds = {} as RegisterCreds;
  protected accountService = inject(AccountService);
  cancelRegister = output<boolean>();

  register() {
    this.accountService.regster(this.creds).subscribe({
      next: response => { console.log(response), this.cancelRegister.emit(false); },
      error: error => alert(error)
    });

  }
  cancel() {
    console.log('Registration cancelled');
  }

}
