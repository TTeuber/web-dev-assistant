import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditModalComponent } from "../components/edit.component";

@Component({
  selector: 'app-chats',
  standalone: true,
  imports: [EditModalComponent],
  templateUrl: './chats.component.html',
  styleUrl: './chats.component.scss'
})
export class ChatsComponent implements OnInit {
  chatData: ChatData[] = [];
  showEditModal = false;
  currentChat: ChatData = {} as ChatData;

  ngOnInit() {
    fetch('http://localhost:5001/Chat')
      .then((response) => response.json())
      .then((data) => {
        this.chatData = data as ChatData[];
      });
  }

  async deleteChat(id: string) {
    await fetch(`http://localhost:5001/chat/${id}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json'
        }
      }
    ).then(() => {
      this.chatData = this.chatData.filter((c) => c.id !== id);
    })


  }
  editChat(chat: ChatData) {
    this.showEditModal = true;
    this.currentChat = chat;
  }
}

export type ChatData = {
  id: string;
  title: string;
};
