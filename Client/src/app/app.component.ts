import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  baseUrl: string = environment.apiUrl;
  constructor(private accountService: AccountService, private http: HttpClient){}


  ngOnInit() {
    this.setCurrentUser();
    }
    setCurrentUser() {
      const user: User = JSON.parse(localStorage.getItem('user'));
      if(user){
        this.accountService.setCurrentUser(user);
      }}
      getInfo(){
        this.http.get(environment.apiUrl+'account').subscribe(response=>{
          console.log(response);
        })
      }
  }



