import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any={};
  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }
  login() {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/admin'); // po zalogowaniu przenosi do members
      console.log(response);
      

    }
    );
  }
  // tslint:disable-next-line: typedef
  logout(){
    this.accountService.logout();

  }

}
