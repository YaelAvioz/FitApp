import { Component } from '@angular/core';

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
  messages: Message[] = [];
  userInput: string = '';

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
    // Replace this logic with actual mentor's response generation logic
    return 'Mentor: the message i got is: ' + this.userInput;
  }
}
