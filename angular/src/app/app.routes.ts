import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ChatComponent } from './chat/chat.component';
import { TestComponent } from './test/test.component';
import {ChatsComponent} from "./chats/chats.component";

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'chat',
    component: ChatsComponent,
  },
  {
    path: 'chat/:id',
    component: ChatComponent,
  },
  {
    path: 'test',
    component: TestComponent,
  },
];
