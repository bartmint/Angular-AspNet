import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();

  registerForm: FormGroup;
  validationErrors: string[] = [];
  baseUrl=environment.apiUrl;
  constructor(private accountService: AccountService, private toastr: ToastrService, private fb: FormBuilder, private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
    this.initializeform();
  }

  register(): void{
    this.accountService.register(this.registerForm.value).subscribe(response => {
        console.log(response)
    }, error => {
      console.log(error);
      this.validationErrors = error;
    });
  }
  cancel(): void{
    this.cancelRegister.emit(false);
    this.toastr.success('afdaf');
  }


  initializeform(): void{
    this.registerForm = this.fb.group({
     userName: ['', [Validators.required, Validators.minLength(4)]],
     email: ['', [Validators.required, Validators.email]],
     password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(20),
       this.passwordNotSameAsUserName('userName'), Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[!-~]{5,}$')]],

     confirmPassword: ['', [Validators.required, this.matchPassword('password')]],
    });
  }
  matchPassword(password: string): ValidatorFn{
    return(control: AbstractControl) => {
      return control?.value === control?.parent?.controls[password].value ? null : {isMatching: true};
    };
  }
  passwordNotSameAsUserName(userName: string): ValidatorFn{
    return(control: AbstractControl) => {
      return control?.value !== control?.parent?.controls[userName].value ? null : {isNotMatching: true};
    };
  }
  test(){
    return this.http.get(this.baseUrl+'account/test').subscribe(response=>{
      console.log(response);
    })
  }

}
