import { Component, OnInit } from '@angular/core';
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

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    //this.getCurrentUser();
   // this.currntuser$=this.accountService.currentUser$
  }

 
  login() {
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
     
      },
      error: error => console.log(error)
    })
 
  }
  logout() {
    this.accountService.logout();
   
  }
}
