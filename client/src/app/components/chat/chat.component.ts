import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, take } from 'rxjs';
import { SessionService } from 'src/app/service/sessionService';
import { sendMessage } from 'src/app/store/chat/chatAction';
import { ChatState } from 'src/app/store/chat/chatReducer';
import { User } from 'src/interfaces/user';
import { ChatBtnComponent } from '../chat-btn/chat-btn.component';

interface Message {
  content: string;
  isUser: boolean;
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
  }

  async getMentorAnswer(userMessage: string): Promise<string> {
    this.store.dispatch(sendMessage({ username: this.currentUser.username, msg: this.userInput }));

    return new Promise<string>((resolve) => {
      const subscription = this.response$.subscribe(response => {
        this.response = response;
        console.log("response", this.response.message);

        setTimeout(() => {
          resolve(this.response.message);
          subscription.unsubscribe();
        }, 2000);
      });
    });
  }

  async sendMessage() {
    let userText = this.userInput;
    this.userInput = '';
    const userMessage: Message = {
      content:userText,
      isUser: true
    };

    this.messages.push(userMessage);

    const mentorAnswerContent = await this.getMentorAnswer(this.userInput);

    if (mentorAnswerContent) {
      const mentorAnswer: Message = {
        content: mentorAnswerContent,
        isUser: false
      };

      this.messages.push(mentorAnswer);
    }
  }


  isChatVisible() {
    this.parent.showChat = !this.parent.showChat;
  }
}