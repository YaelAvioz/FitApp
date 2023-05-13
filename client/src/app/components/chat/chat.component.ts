import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { sendMessage } from 'src/app/store/chat/chatAction';
import { ChatState } from 'src/app/store/chat/chatReducer';

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
  msg$: Observable<string | null>;
  messages: Message[] = [];
  userInput: string = '';

  constructor(private store: Store<{ chatReducer: ChatState }>) {
    this.msg$ = this.store.select((state) => {
      return state.chatReducer.response;
    })
  }

  sendMessage() {
    const userMessage: Message = {
      content: this.userInput,
      isUser: true
    };
    const mentorAnswer: Message = {
      content: this.getMentorAnswer(this.userInput),
      isUser: false
    };

    this.messages.push(userMessage);
    this.messages.push(mentorAnswer);

    this.userInput = '';
  }

  getMentorAnswer(userMessage: string): string {
    this.store.dispatch(sendMessage({ username: "dana", msg: this.userInput }));
    //TODO: subscribe and return the value as mentor answere
    return 'Mentor: the message i got is: ' + this.userInput;
  }
}