// src/app/shared/messageHelper/message.helper.ts

import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root', // Make sure it's available application-wide
})
export class MessageHelper {
  constructor(private toastr: ToastrService) {}

  success(message: string, title?: string) {
    this.toastr.success(message, title);
  }

  error(message: string, title?: string) {
    this.toastr.error(message, title);
  }

  info(message: string, title?: string) {
    this.toastr.info(message, title);
  }

  warning(message: string, title?: string) {
    this.toastr.warning(message, title);
  }
}
