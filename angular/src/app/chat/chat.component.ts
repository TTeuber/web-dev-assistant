import { Component, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MessageComponent } from '../components/message.component';
import { v4 as uuid } from 'uuid';
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, MessageComponent, ReactiveFormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss',
})
export class ChatComponent implements OnInit {
  data: Data[] = [];
  text = new FormControl('');
  uuid = uuid;
  id: string;
  loading = false;

  constructor(private route: ActivatedRoute) {
    this.id = this.route.snapshot.paramMap.get("id")!;
  }
  ngOnInit() {
    // Check if this is the first message
    if (sessionStorage.getItem('message') !== null) {
      const message = JSON.parse(sessionStorage.getItem('message')!);
      const id = uuid();
      this.data.push({ id, content: message, role: 'user' } as Data);
      sessionStorage.removeItem('message');
      this.sendMessage(id);

    }
    else {
      fetch(`http://localhost:5001/Chat/${this.id}`)
        .then((response) => response.json())
        .then((data) => {
          this.data.push(...data);
        });
    }
  }

  sendMessage(id: string) {
    this.loading = true;
    fetch(`http://localhost:5001/Chat/${this.id}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({id, chatId: this.route.snapshot.paramMap.get("id"), content: this.data[this.data.length - 1].content})
    }).then(r => r.json()).then(d => {
      console.log("data: " + d);
      this.data.push(d as Data);
      this.loading = false;
    });
  }
}

type Data = {
  id: string;
  content: string;
  role: string;
};
