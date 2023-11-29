import { Component, Input, Output, EventEmitter, OnInit } from "@angular/core";
import { FormsModule, FormControl, ReactiveFormsModule } from "@angular/forms";
import { ChatData } from "../chats/chats.component";

@Component({
  selector: "edit-modal",
  standalone: true,
  styleUrls: ["./edit.component.scss"],
  template: `
    <div class="modal">
      <div class="modal-content">
        <button class="close-button" (click)="closeModal()">X</button>
          <h2>Edit Chat Title</h2>
        <div class="modal-body">
          <form (submit)="
            $event.preventDefault();
            editChat();
          ">
            <label for="title">Title</label>
            <input type="text" id="title" name="title" [formControl]="title" value="chat.title">
            <button type="submit" >Edit</button>
          </form>
        </div>
      </div>
    </div>
  `,
  imports: [
    FormsModule,
    ReactiveFormsModule
  ]
})
export class EditModalComponent implements OnInit {
  title = new FormControl('');
  @Input() chat: ChatData = {} as ChatData;
  @Output() closeModalEvent = new EventEmitter<boolean>();

  ngOnInit(): void {
    this.title.setValue(this.chat.title);
  }

  closeModal() {
    this.closeModalEvent.emit(false);
  }
  editChat() {
    fetch(`http://localhost:5001/Chat`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        chatId: this.chat.id,
        title: this.title.value
      })
    }).then(() => {
      if (this.title.value != null) {
        this.chat.title = this.title.value;
      }
      this.closeModal();
    })
  }
}
