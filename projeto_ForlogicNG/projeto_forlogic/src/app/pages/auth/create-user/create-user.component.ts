import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { UserService } from '../../../services/user.service';



@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit, OnDestroy {

  form!: FormGroup;
  apiError: string | null = null;
  notificationMessage: string | null = null;

  submitted = false;
  hideNameError = false;
  hideEmailError = false;
  hidePasswordError = false;

  private destroy$ = new Subject<void>()
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private userService: UserService
  ) {}
  

  ngOnInit() {
    document.body.classList.add('login');

    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });

    this.form.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        if (this.apiError) this.apiError = null;
      });
  }

  ngOnDestroy() {
    document.body.classList.remove('login');
    this.destroy$.next();
    this.destroy$.complete();
  }

  hideError(field: 'email' | 'password' | 'name') {
    if (field === 'email') this.hideEmailError = true;
    if (field === 'password') this.hidePasswordError = true;
    if (field === 'name') this.hideNameError = true;
  }

  backPage(){
    this.router.navigate(['/login']);
  }

  onSubmit() {
    this.submitted = true;
    this.apiError = null;


    this.hideEmailError = false;
    this.hidePasswordError = false;
    this.hideNameError = false;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const newUser = this.form.value;
  
    this.userService.createUser(newUser)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          this.notificationMessage = 'Usuário criado com sucesso!';
          setTimeout(() => {
            this.notificationMessage = null;
            this.router.navigate(['/login']);
          }, 1500);
        },
        error: (err) => {
          this.apiError = err.error?.message || 'Erro ao criar usuário. Tente novamente.';
        }
      });

  }
}