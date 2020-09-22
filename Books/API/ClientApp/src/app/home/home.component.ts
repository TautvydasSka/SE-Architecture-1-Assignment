import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Book } from '../models/book';
import { FormControl, FormGroup } from '@angular/forms';
import { ErrorResponse } from '../models/error-response';

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

  public isbnError: string;
  public authorError: string;
  public titleError: string;
  public pageCountError: string;

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
    this.clearModalErrors();

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
      pageCount: this.bookForm.get('pageCount').value ? parseInt(this.bookForm.get('pageCount').value) : 0,
    };

    if (this.isEdit) {
      this.http.put<object>(this.baseUrl + 'api/books', book).subscribe(result => {
        console.log(result);
        this.getBooks();
        $('#exampleModal').modal('hide')
      }, error => this.handleError(error));
    } else {
      this.http.post<object>(this.baseUrl + 'api/books', book).subscribe(result => {
        console.log(result);
        this.getBooks();
        $('#exampleModal').modal('hide')
      }, error => this.handleError(error));
    }
  }

  public delete(isbn: string): void {
    this.http.delete<object>(this.baseUrl + `api/books/${isbn}`).subscribe(result => {
      console.log(result);
      this.getBooks();
    }, error => this.handleError(error));
  }

  private handleError(error: HttpErrorResponse): void {
    var errors = error.error.errors ? error.error.errors : error.error;

    const isbnErrors = errors.ISBN as string[];
    const authorErrors = errors.Author as string[];
    const titleErrors = errors.Title as string[];
    const pageCountErrors = errors.PageCount as string[];

    this.isbnError = (isbnErrors && isbnErrors.length > 0) ? isbnErrors[0] : null;
    this.authorError = (authorErrors && authorErrors.length > 0) ? authorErrors[0] : null;
    this.titleError = (titleErrors && titleErrors.length > 0) ? titleErrors[0] : null;
    this.pageCountError = (pageCountErrors && pageCountErrors.length > 0) ? pageCountErrors[0] : null;
  }

  private clearModalErrors(): void {
    this.isbnError = null;
    this.authorError = null;
    this.titleError = null;
    this.pageCountError = null;
  }
}