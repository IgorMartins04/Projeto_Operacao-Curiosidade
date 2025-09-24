import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AuthService } from '../../../services/auth.services';
import { LogService } from '../../../services/log.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  apiError: string | null = null;
  submitted = false;

  hideEmailError = false;
  hidePasswordError = false;

  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private logService: LogService
  ) {}

  ngOnInit() {
    document.body.classList.add('login');

    this.form = this.fb.group({
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

  get email(): AbstractControl { return this.form.get('email')!; }
  get password(): AbstractControl { return this.form.get('password')!; }

  hideError(field: 'email' | 'password') {
    if (field === 'email') this.hideEmailError = true;
    if (field === 'password') this.hidePasswordError = true;
  }

  loggedUserName: string | null = null;
  notificationMessage: string | null = null; 

  login() {
    this.submitted = true;
    this.apiError = null;
    this.hideEmailError = false;
    this.hidePasswordError = false;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const credentials = this.form.value;

    this.authService.login(credentials)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          const user = this.authService.getUserFromToken();
          const userName = user ? user.name : 'Usuário desconhecido';
          this.loggedUserName = userName;
          this.notificationMessage = `Bem-vindo, ${userName}!`;

          this.logService.createLog({ action: `Login feito por: ${userName}` })
            .pipe(takeUntil(this.destroy$))
            .subscribe({
              next: () => {
                setTimeout(() => {
                  this.notificationMessage = null;
                  this.router.navigate(['/dash']);
                }, 2000);
              },
              error: (err) => {
                console.error('Erro ao criar log de login:', err);

                setTimeout(() => {
                  this.notificationMessage = null;
                  this.router.navigate(['/dash']);
                }, 2000);
              }
            });
        },
        error: (err) => {
          this.apiError = err.status === 404
            ? 'Usuário ou senha inválidos.'
            : 'Erro ao conectar com o servidor.';
        }
    });
  }
}