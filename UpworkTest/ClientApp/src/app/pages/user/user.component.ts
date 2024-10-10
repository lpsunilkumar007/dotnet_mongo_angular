import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import { User } from '../../core/models/user/userModel';
import {
  fetchUsers,
  createUser,
  updateUser,
  deleteUser,
  deleteUserData,
} from '../../store/user/user.actions';
import {
  selectAllUsers,
  selectUserLoading,
  selectUserError,
} from '../../store/user/user.selectors';
import { UserDataAccessService } from '../../core/services/user-data-access/user-data-access.service';
import * as AuthActions from '../../store/auth/auth.actions';
import { Router } from '@angular/router';
import { faL } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  users$: Observable<User[]> = this.store.pipe(select(selectAllUsers));
  loading$: Observable<boolean> = this.store.pipe(select(selectUserLoading));
  error$: Observable<string | null> = this.store.pipe(select(selectUserError));
  userForm: FormGroup;
  showModal = false;
  newUser: User = {
    id: '',
    firstName: '',
    lastName: '',
    email: '',
    mobileNumber: '',
    remainingDays: 0,
    isRequestedToDelete: false,
    isDeleted: false,
  };
  newUserMarkedForDelete: User = {
    id: '',
    firstName: '',
    lastName: '',
    email: '',
    mobileNumber: '',
    remainingDays: 0,
    isRequestedToDelete: false,
    isDeleted: false,
  };
  isEditing = false;
  showConfirmModal = false;
  isOpenConfirmation = false;
  userToDelete: string | null = null;

  constructor(
    private store: Store,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      mobileNumber: [
        '',
        [Validators.required, Validators.pattern('^[0-9]{10,12}$')],
      ],
      // remainingDays: [0, Validators.nullValidator],
      id: [null, Validators.nullValidator],
      // isRequestedToDelete: [false, Validators.nullValidator],
      // isDeleted: [false, Validators.nullValidator],
    });
  }

  ngOnInit() {
    this.store.dispatch(fetchUsers());
    this.loading$.subscribe((loading) => {});
    this.error$.subscribe((error) => {});
  }

  openModal(user: User | null) {
    if (user) {
      this.isEditing = true;
      this.newUser = { ...user };
      this.userForm.patchValue({
        id: user.id,
        firstName: user.firstName,
        lastName: user.lastName,
        email: user.email,
        mobileNumber: user.mobileNumber,
        remainingDays: user.remainingDays,
        isRequestedToDelete: user.isRequestedToDelete,
        isDeleted: user.isDeleted,
      });
    } else {
      this.isEditing = false;
      this.resetForm();
    }
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.resetForm();
  }

  resetForm() {
    this.newUser = {
      id: '',
      firstName: '',
      lastName: '',
      email: '',
      mobileNumber: '',
      remainingDays: 0,
      isRequestedToDelete: false,
      isDeleted: false,
    };
    this.isEditing = false;
    this.userForm.patchValue({
      id: '',
      firstName: '',
      lastName: '',
      email: '',
      mobileNumber: '',
      remainingDays: 0,
      isRequestedToDelete: false,
      isDeleted: false,
    });
    this.userForm.markAsPristine();
    this.userForm.markAsUntouched();
  }
  logout() {
    localStorage.setItem('isAuthenticated', 'false');
    this.store.dispatch(
      AuthActions.setAuthenticated({ isAuthenticated: false })
    );
    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
  }
  onSubmit() {
    if (this.userForm.invalid) {
      Object.keys(this.userForm.controls).forEach((field) => {
        const control = this.userForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
      return;
    }
    const registerData: User = this.userForm.value;
    debugger;
    if (this.isEditing) {
      this.store.dispatch(updateUser({ user: registerData }));
    } else {
      this.store.dispatch(createUser({ user: registerData }));
    }
    this.userForm.reset();
    setTimeout(() => {
      this.store.dispatch(fetchUsers());
    }, 1000);

    this.closeModal();
  }

  confirmDelete(userId: string) {
    this.userToDelete = userId;
    this.showConfirmModal = true;
  }
  markForDelete() {
    this.store.dispatch(deleteUserData({ user: this.newUserMarkedForDelete }));
    setTimeout(() => {
      this.store.dispatch(fetchUsers());
    }, 1000);
    this.isOpenConfirmation = false;
  }

  confirmConsentToDelete(user: User) {
    this.newUserMarkedForDelete = { ...user };
    this.isOpenConfirmation = true;
  }

  closeConsentModal() {
    this.isOpenConfirmation = false;
  }
  deleteConfirmed() {
    if (this.userToDelete) {
      this.store.dispatch(deleteUser({ userId: this.userToDelete }));
      this.closeConfirmModal();
      this.resetForm();
    }
  }

  closeConfirmModal() {
    this.showConfirmModal = false;
    this.userToDelete = null;
  }
}
