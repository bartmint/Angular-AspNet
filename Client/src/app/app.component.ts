import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  value: any;
  title = 'Client';
  baseUrl: string = environment.apiUrl;
  constructor(private http: HttpClient){}


  ngOnInit() {
    this.get();
  }
  get(){
    this.http.get(this.baseUrl+'user').subscribe(response=>{
      this.value=response;
      console.log(this.value);
    }, error=>{
      console.log(error);
    })
  }
 
}
