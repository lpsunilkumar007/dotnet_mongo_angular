import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../../core/services/account/acount.service';
import { RegisterViewModel } from '../../../core/models/account/registerViewModel';
import { MessageHelper } from '../../../shared/messageHelper/messgae.helper';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
})
export class RegisterComponent {
  registerForm: FormGroup;
  isModalOpen = false;

  constructor(
    private fb: FormBuilder,
    private userService: AccountService,
    private messgaeHelper: MessageHelper,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{10,12}$')]],
      isTermAndPolicyAccepted: [false, Validators.requiredTrue],
    });
  }

  // Register User
  onSubmit() {
    this.registerForm.markAllAsTouched();
    if (this.registerForm.valid) {
      const registerData: RegisterViewModel = this.registerForm.value;
      this.userService.registerUser(registerData).subscribe(
        (user) => {
          this.messgaeHelper.success('User created successfully');
          this.router.navigate(['/login']);
        },
        (error) => {
          console.error('Error creating user:', error);
        }
      );
    } else {
      console.log('Form Invalid');
    }
  }

  // Open modal for terms and policies
  openTermsModal(): void {
    this.isModalOpen = true;
  }

  // Close modal
  closeModal(): void {
    this.isModalOpen = false;
  }
}
