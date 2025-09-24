import { Component } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { LogService } from '../../services/log.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrl: './history.component.scss'
})
export class HistoryComponent {

  user: string = '';

  allLogs: any [] = [];
  actionLogs: any [] = [];
  paginatedLogs: any[] = [];

  currentPage: number = 1;
  logsPerPage: number = 6;
  totalPages: number = 1;


  searchTerm: string = '';
  private destroy$ = new Subject<void>();

  constructor(
    private logService : LogService
  ) {}

  ngOnInit(): void {
    this.setUserFromToken();
    this.loadLogs();
  }
  
  private setUserFromToken(): void {
    const token = localStorage.getItem('token');
    if (!token) {
      window.location.href = '/login';
      return;
    }

    try {
      const decoded: any = jwtDecode(token);
      this.user = decoded.unique_name || 'Usuário';
    } catch {
      this.user = 'Usuário';
    }
  }

  loadLogs(): void {
    this.logService.getAllLogs()
      .pipe(takeUntil(this.destroy$))
      .subscribe(data => {
        this.allLogs = data.map(l => ({
          ...l
        }));
        this.actionLogs = [...this.allLogs];
        this.calculatePagination();
      });
  }

  calculatePagination(): void {
    this.totalPages = Math.ceil(this.actionLogs.length / this.logsPerPage);
    this.changePage(this.currentPage);
  }

  changePage(page: number): void {
    if (page < 1) page = 1;
    if (page > this.totalPages) page = this.totalPages;

    this.currentPage = page;

    const start = (this.currentPage - 1) * this.logsPerPage;
    const end = start + this.logsPerPage;

    this.paginatedLogs = this.actionLogs.slice(start, end);
  }

  searchLogs(term: string): void{
    const search = term.trim().toLowerCase();

    if(!search) {
      this.actionLogs = [...this.allLogs];
      return;
    }

    this.actionLogs = this.allLogs.filter(l =>
      Object.values(l).some(val =>
        String(val).toLowerCase().includes(search)
      )
    );
    this.currentPage = 1;
    this.calculatePagination();
  }

}
