import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
//import { ToastrService } from 'ngx-toastr/public_api';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}//variable declared name model with type any initializes values with empty object
  //loggedIn = false;
 // currntuser$: Observable<User | null>= of(null);

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    //this.getCurrentUser();
   // this.currntuser$=this.accountService.currentUser$
  }

 
  login() {
    this.accountService.login(this.model).subscribe({
      next: _ => this.router.navigateByUrl('/members'),
      error: error => this.toastr.error(error.error)
    })
 
  }
  logout() {
    this.accountService.logout();
   this.router.navigateByUrl('/')
  }
}
