import { Routes } from '@angular/router';
import { BookListComponent } from './components/book-list/book-list.component';
import { BookFormComponent } from './components/book-form/book-form.component';

export const routes: Routes = [
  { path: 'books', component: BookListComponent },
  { path: 'books/form', component: BookFormComponent },
  { path: 'books/edit/:id', component: BookFormComponent },
  { path: '**', redirectTo: '/books' },
];
