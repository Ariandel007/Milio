import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Client } from '../_models/client';
import { Carer } from '../_models/carer';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  iWantToBeCarer: boolean = false;

  client: Client;
  carer: Carer;
  registerFormClient: FormGroup;
  registerFormCarer: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private toastr: ToastrService,
              private router: Router,
              private authService: AuthService,
              private fb: FormBuilder) { }


  ngOnInit(): void {
    this.bsConfig = {
      containerClass: 'theme-red'
    };

    this.createRegisterFormClient();
    this.createRegisterFormCarer();
  }

  createRegisterFormClient(): void {
    this.registerFormClient = this.fb.group(
      {
        userName: ['', Validators.required],
        name: ['', Validators.required],
        lastName: ['', Validators.required],
        gender: ['male'],
        dateOfBirth: [null, Validators.required],
        city: ['', Validators.required],
        country: ['', Validators.required],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(16)
          ]
        ],
        confirmPassword: ['', Validators.required],
        address: ['', Validators.required]
      },
      { validator: this.passwordMatchValidator }
    );
  }

  createRegisterFormCarer(): void {
    this.registerFormCarer = this.fb.group(
      {
        userName: ['', Validators.required],
        name: ['', Validators.required],
        lastName: ['', Validators.required],
        gender: ['male'],
        dateOfBirth: [null, Validators.required],
        city: ['', Validators.required],
        country: ['', Validators.required],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(16)
          ]
        ],
        confirmPassword: ['', Validators.required],
        fareForHour: ['', Validators.required]
      },
      { validator: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { mismatch: true };
  }

  registerClient(): void {
    if (this.registerFormClient.valid) {
      //copia en profundidad
      this.client = Object.assign({}, this.registerFormClient.value);

      this.authService.registerClient(this.client).subscribe(
        () => {
          this.toastr.success('Registro exitoso');
        },
        error => {
          this.toastr.error(error);
        },
        () => {
          this.authService.login(this.client).subscribe(() => {
            this.router.navigate(['/messages']);
          });
        }
      );
    }
  }

  registerCarer(): void {
    if (this.registerFormCarer.valid) {
      //copia en profundidad
      this.carer = Object.assign({}, this.registerFormCarer.value);
      this.authService.registerCarer(this.carer).subscribe(
        () => {
          this.toastr.success('Registro exitoso');
        },
        error => {
          this.toastr.error(error);
        },
        () => {
          this.authService.login(this.carer).subscribe(() => {
            this.router.navigate(['/messages']);
          });
        }
      );
    }
  }

  toggleOptions(){
    return this.iWantToBeCarer = !this.iWantToBeCarer;
  }

}
