import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { Message } from '../_models/message';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Message[];
  chatMessages: Message[];
  pagination: any = {};
  messageContainer = 'Unread';

  seeMessages: boolean;


  newMessage: any = {};

  currentUserID: number;

  idUSerToSendMessage: number;

  uniqueName: string;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.messages = null;
    this.seeMessages = false;
    this.uniqueName = this.authService.decodedToken.unique_name;

    // this.route.data.subscribe(data => {
    //   this.messages = data.messages.result;
    //   this.pagination = data.messages.pagination;
    // });
    this.currentUserID = this.authService.decodedToken.nameid;
    this.loadMessages();
  }

  loadMessages() {
    this.userService
      .getMessages(
        this.authService.decodedToken.nameid,
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.messageContainer
      )
      .subscribe(
        (res: PaginatedResult<Message[]>) => {
          this.messages = res.result;
          this.pagination = res.pagination;
        },
        error => {
          this.toastr.error(error);
        }
      );
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMessages();
  }

  loadChat(idRecipient: number, idSender: number)
  {
    const idUSerToSendMessage = this.currentUserID == idRecipient ? idSender : idRecipient;
    // this.idUSerToSendMessage = idUSerToSendMessage;
    console.log(idUSerToSendMessage);
    this.newMessage.recipientId = idUSerToSendMessage;

    this.userService.getMessageThread(this.currentUserID, idUSerToSendMessage)
    .subscribe(
      chatMessages => {
        this.chatMessages = chatMessages;
        console.log(chatMessages);
      },
      error => {
        this.toastr.error(error);
      }
    );

  }

  sendMessage() {
      this.userService
      .sendMessage(this.authService.decodedToken.nameid, this.newMessage)
      .subscribe(
        (message: Message) => {
          // this.chatMessages.unshift(message);
          this.chatMessages.push(message);
          this.newMessage.content = '';
        },
        error => {
          this.toastr.error(error);
        }
    );
  }

  toggleSeeMessage(){
    this.seeMessages  = !this.seeMessages;
  }
}
