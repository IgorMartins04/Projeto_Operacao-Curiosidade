import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { LogService } from '../../services/log.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

  @Input({ required: true }) user: string = 'Usuário';
  searchTerm: string = '';

  @Output() search = new EventEmitter<string>();

  isDarkMode = false;

  onInputChange(event: any) {
    this.searchTerm = event.target.value;
    this.search.emit(this.searchTerm);
  }

  constructor(
    private router: Router,
    private logService: LogService
  ) {}

  ngOnInit(): void {
    const theme = localStorage.getItem('theme');
    if (theme === 'dark') {
      document.documentElement.setAttribute('dark-theme', 'dark');
    }
  }

  toggleTheme(event: any) {
    const theme = document.getElementById("textTheme");
    if (!theme) return;

    if (event.checked) {
      document.documentElement.setAttribute('dark-theme', 'dark');
      localStorage.setItem('theme', 'dark');
    } else {
      document.documentElement.removeAttribute('dark-theme');
      localStorage.setItem('theme', 'root');
    }
  }

  logout(): void {
    const token = localStorage.getItem('token');
    let userName = 'usuário desconhecido';

    if (token) {
      try {
        const decoded: any = jwtDecode(token);
        if (decoded && decoded.unique_name) {
          userName = decoded.unique_name;
        }
      } catch {}
    }

    this.logService.createLog({ action: `Logout feito por: ${userName}` })
      .subscribe({
        next: () => this.performLogout(),
        error: () => this.performLogout() 
      });
    }

  private performLogout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }


}
