import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();

  registerForm: FormGroup;
  validationErrors: string[] = [];
  constructor(private accountService: AccountService, private toastr: ToastrService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeform();
  }

  register(): void{
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('succesfull-register');
      this.accountService.login(response);
      this.cancel();
    }, error => {
      console.log(error);
      this.validationErrors = error;
    });
  }
  cancel(): void{
    this.cancelRegister.emit(false);
  }


  initializeform(): void{
    this.registerForm = this.fb.group({
     userName: ['', [Validators.required, Validators.minLength(4)]],
     email: ['', [Validators.required, Validators.email]],
     password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(20), this.passwordNotSameAsUserName('userName')]],
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


}
