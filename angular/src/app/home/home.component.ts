import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, ReactiveFormsModule } from "@angular/forms";
import data from '../../assets/data.json';
import { v4 as uuid } from 'uuid';
import { Router } from "@angular/router";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  protected readonly data = data;

  message = new FormControl('');

  constructor(private router: Router) {}

  async sendMessage() {
    sessionStorage.setItem('message', JSON.stringify(this.message!.value));
    const id = uuid();
    await fetch('http://localhost:5001/Chat', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({chatId: id, role: 'user', content: this.message!.value})
    }).then(() => {
      this.router.navigate(['/chat', id]);
    })
  }
}
