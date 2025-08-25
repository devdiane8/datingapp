import { HttpClient } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-test-errors',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './test-errors.html',
  styleUrl: './test-errors.css'
})
export class TestErrors {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/';
  validationError = signal<string[]>([]);
  get404Error() {
    return this.http.get(`${this.baseUrl}buggy/not-found`).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => {
        console.log(error);
      }
    })

  }
  get500Error() {
    return this.http.get(`${this.baseUrl}buggy/server-error`).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => {
        console.log(error);
      }
    })
  }
  get400Error() {
    return this.http.get(`${this.baseUrl}buggy/bad-request`).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => {
        console.log(error);
      }
    })
  }
  get400ValidationError() {
    return this.http.post(`${this.baseUrl}account/register`, {
      name: 'John Doe',
      age: 20
    }).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => {
        console.log(error);
        this.validationError.set(error);
      }
    })
  }

}
