import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../services/book.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.scss'],
})
export class BookFormComponent implements OnInit {
  bookForm!: FormGroup;
  isEditMode = false;
  bookId?: number;

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.bookForm = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(1)]],
    });

    this.route.params.subscribe((params) => {
      const id = params['id'];
      if (id) {
        this.isEditMode = true;
        this.bookId = +id;
        this.loadBookData(this.bookId);
      }
    });
  }

  public loadBookData(id: number): void {
    this.bookService.getBookById(id).subscribe({
      next: (book) => {
        this.bookForm.patchValue(book);
      },
      error: (err) => {
        console.error('Error loading book data:', err);
      },
    });
  }

  onSubmit(): void {
    if (this.bookForm.valid) {
      const book = this.bookForm.value;

      if (this.isEditMode) {
        this.bookService.updateBook(this.bookId!, book).subscribe({
          next: (response) => {
            this.router.navigate(['/books']);
          },
          error: (err) => {
            console.error('Error updating book:', err);
          },
        });
      } else {
        const { id, ...bookWithoutId } = book;
        this.bookService.createBook(bookWithoutId).subscribe({
          next: (response) => {
            this.router.navigate(['/books']);
          },
          error: (err) => {
            console.error('Error creating book:', err);
          },
        });
      }
    }
  }
}
