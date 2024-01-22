import { Component, Input, OnInit } from '@angular/core';
import data from '../../assets/data.json';

@Component({
  selector: 'message',
  standalone: true,
  styleUrl: './message.component.scss',
  template: `
    @for (message of messages; track message.id) {
      <div class="message-container {{ message.role }}">
        <p>{{ message.content }}</p>
      </div>
    }
  `,
})
export class MessageComponent {
  @Input({ required: true }) messages: Messages[] | undefined;
}

type Messages = {
  id: string;
  role: "user" | "assistant";
  content: string;
};
