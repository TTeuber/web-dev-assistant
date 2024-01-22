import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-nav',
  standalone: true,
  template: `
    <nav>
      <a routerLink="/">Home</a>
      <a routerLink="/chat">Chat</a>
    </nav>
  `,
  imports: [RouterLink],
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent {}
