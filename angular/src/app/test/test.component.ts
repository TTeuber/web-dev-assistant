import { Component, OnInit, signal } from "@angular/core";
import { CommonModule } from '@angular/common';
import { EditModalComponent } from "../components/edit.component";

@Component({
  selector: 'app-test',
  standalone: true,
  imports: [CommonModule, EditModalComponent],
  styles: [`
    @mixin test-mixin {
      color: red;
    }
    p {
      @include test-mixin;
    }
  `],
  template: `
    <p>test</p>
  `,
})
export class TestComponent {
  showModal = false;

  closeModal() {
    this.showModal = false;
  }
}
