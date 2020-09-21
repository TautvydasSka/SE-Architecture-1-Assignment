import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../models/book';

declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  private http: HttpClient;
  private baseUrl: string;
  public books: Book[];

  @ViewChild('modal', null) modal: ElementRef;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.getBooks();
  }

  private getBooks(): void {
    this.http.get<Book[]>(this.baseUrl + 'api/books').subscribe(result => {
      this.books = result;
      console.log(this.books);
    }, error => console.error(error));
  }
  public showModal(isEdit: boolean): void {
    console.log("clicked");
    $('#myModal').modal('show');
  }
}