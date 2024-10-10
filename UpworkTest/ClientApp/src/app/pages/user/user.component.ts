import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
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

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  users$: Observable<User[]> = this.store.pipe(select(selectAllUsers));
  loading$: Observable<boolean> = this.store.pipe(select(selectUserLoading));
  error$: Observable<string | null> = this.store.pipe(select(selectUserError));

  showModal = false;
  newUser: User = {
    id: '',
    firstName: '',
    lastName: '',
    email: '',
    mobileNumber: '',
    remainingDays: 0,
    isRequestedToDelete: false,
    isDeleted:false
  };
  newUserMarkedForDelete: User = {
    id: '',
    firstName: '',
    lastName: '',
    email: '',
    mobileNumber: '',
    remainingDays: 0,
    isRequestedToDelete: false,
    isDeleted:false
  };
  isEditing = false;
  showConfirmModal = false;
  isOpenConfirmation = false;
  userToDelete: string | null = null;

  constructor(
    private store: Store,
    private userDataAccess: UserDataAccessService
  ) {}

  ngOnInit() {
    this.store.dispatch(fetchUsers());
    this.loading$.subscribe((loading) => {});
    this.error$.subscribe((error) => {});
  }

  openModal(user: User | null) {
    if (user) {
      this.isEditing = true;
      this.newUser = { ...user };
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
      isDeleted:false
    };
    this.isEditing = false;
  }

  onSubmit() {
    if (this.isEditing) {
      this.store.dispatch(updateUser({ user: this.newUser }));
    } else {
      this.store.dispatch(createUser({ user: this.newUser }));
    }
 
    // Add a delay of 1 second (1000 milliseconds) before fetching users
    setTimeout(() => {
      this.store.dispatch(fetchUsers());
    }, 1000); // Change the delay time as needed
 
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
    }
  }

  closeConfirmModal() {
    this.showConfirmModal = false;
    this.userToDelete = null;
  }
}
