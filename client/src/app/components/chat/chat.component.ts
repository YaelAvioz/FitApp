import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { SessionService } from 'src/app/service/sessionService';
import { sendMessage } from 'src/app/store/chat/chatAction';
import { ChatState } from 'src/app/store/chat/chatReducer';
import { User } from 'src/interfaces/user';
import { ChatBtnComponent } from '../chat-btn/chat-btn.component';

interface Message {
  content: string;
  isFromUser: boolean;
}

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})

export class ChatComponent {
  response$: Observable<string>;
  messages: Message[] = [];
  response: any;
  userInput: string = '';
  currentUser !: User;
  placeholderText: string = 'Enter text';


  constructor(private parent: ChatBtnComponent, private sessionService: SessionService, private store: Store<{ chatReducer: ChatState }>) {
    this.response$ = this.store.select((state) => {
      return state.chatReducer.response;
    })
    this.currentUser = this.sessionService.getUserFromSession();

    // Subscribe to response$ here and handle the response when it arrives
    this.response$.subscribe(response => {
      this.response = response;
      console.log("response", this.response.message);
      if (this.response.message) {
        const mentorAnswer: Message = {
          content: this.response.message,
          isFromUser: false
        };
        this.messages.push(mentorAnswer);
      }
    });
  }

  sendMessage() {
    const userMessage: Message = {
      content: this.userInput,
      isFromUser: true
    };

    this.messages.push(userMessage);

    this.store.dispatch(sendMessage({ username: this.currentUser.username, msg: this.userInput }));

    this.userInput = '';
  }

  isChatVisible() {
    this.parent.showChat = !this.parent.showChat;
  }
}
