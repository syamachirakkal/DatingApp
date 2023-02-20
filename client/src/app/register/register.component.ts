import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
//import { ToastrService } from 'ngx-toastr/toastr/toastr.service';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //@Input() usersfromHomeComponent: any;//to enable parent child communication here home is parent and register component is child component
  @Output() cancelRegister = new EventEmitter();
  model: any = {};


  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {

        this.cancel();
      },
      error: error => this.toastr.error(error.error)
    })

  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
