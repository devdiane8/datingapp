import { Component, signal } from '@angular/core';
import { Register } from '../account/register/register';

@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

  protected registerMode = signal(false);


  handleRegister(registerMode: boolean) {
    this.registerMode.set(registerMode);
  }


}
