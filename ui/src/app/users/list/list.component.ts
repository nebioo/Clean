import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '../../_services/account.service';

import {ConfirmationService} from 'primeng/api';

@Component({
  templateUrl: './list.component.html',
})
export class ListComponent implements OnInit {
  users = null;

  constructor(
    private accountService: AccountService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.accountService
      .getAll()
      .pipe(first())
      .subscribe((users) => (this.users = users));
  }

  deleteUser(id: string) {
    this.confirmationService.confirm({
      message: 'Do you want to delete this record?',
      accept: () => {
        const user = this.users.find((x) => x.id === id);
        user.isDeleting = true;
        this.accountService
          .delete(id)
          .pipe(first())
          .subscribe(() => {
            this.users = this.users.filter((x) => x.id !== id);
          });
      },
    });
  }
}
