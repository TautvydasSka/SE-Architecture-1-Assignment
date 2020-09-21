import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../models/book';
import { FormControl, FormGroup } from '@angular/forms';

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

  public isEdit: boolean;

  private bookForm: FormGroup;

  @ViewChild('modal', null) modal: ElementRef;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.getBooks();

    this.bookForm = new FormGroup({
      isbn: new FormControl(''),
      author: new FormControl(''),
      title: new FormControl(''),
      pageCount: new FormControl('')
    });
  }

  private getBooks(): void {
    this.http.get<Book[]>(this.baseUrl + 'api/books').subscribe(result => {
      this.books = result;
    }, error => console.error(error));
  }

  public showModal(isEdit: boolean, editedBook: Book = null): void {
    this.isEdit = isEdit;
    $('#exampleModal').modal();

    if (editedBook) {
      this.bookForm.setValue(editedBook);
    } else {
      this.bookForm.reset();
    }
  }

  public submit(): void {
    const book: Book = {
      isbn: this.bookForm.get('isbn').value,
      author: this.bookForm.get('author').value,
      title: this.bookForm.get('title').value,
      pageCount: parseInt(this.bookForm.get('pageCount').value),
    };

    if (this.isEdit) {
      this.http.put<object>(this.baseUrl + 'api/books', book).subscribe(result => {
        console.log(result);
        this.getBooks();
      }, error => console.error(error));
    } else {
      this.http.post<object>(this.baseUrl + 'api/books', book).subscribe(result => {
        console.log(result);
        this.getBooks();
      }, error => console.error(error));
    }

    $('#exampleModal').modal('hide')
  }

  public delete(isbn: string): void {
    this.http.delete<object>(this.baseUrl + `api/books/${isbn}`).subscribe(result => {
      console.log(result);
      this.getBooks();
    }, error => console.error(error));
  }

  private handleError(): void {

  }
}