import { Component } from '@angular/core';
import { RegisterService } from '../../services/register.service';
import { Subject, takeUntil } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { LogService } from '../../services/log.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrl: './report.component.scss'
})
export class ReportComponent {

  user: string = '';

  allRegisters: any [] = [];
  registers: any []  = [];
  paginatedRegisters: any[] = [];

  currentPage: number = 1;
  registersPerPage: number = 6;
  totalPages: number = 1;

  showPopup: boolean = false;
  selectedRegister: any = null;

  searchTerm: string = '';
  private destroy$ = new Subject<void>();

  constructor(
    private registerService : RegisterService,
    private logService: LogService
  ) {}

  ngOnInit(): void {
    this.setUserFromToken();
    this.loadRegisters();
    
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

  loadRegisters(): void {
    this.registerService.getAll()
      .pipe(takeUntil(this.destroy$))
      .subscribe(data => {
        this.allRegisters = data.map(r => ({
          ...r
        }));
        this.registers = [...this.allRegisters];
        this.calculatePagination();
      });
  }

  calculatePagination(): void {
    this.totalPages = Math.ceil(this.registers.length / this.registersPerPage);
    this.changePage(this.currentPage);
  }

  changePage(page: number): void {
    if (page < 1) page = 1;
    if (page > this.totalPages) page = this.totalPages;

    this.currentPage = page;

    const start = (this.currentPage - 1) * this.registersPerPage;
    const end = start + this.registersPerPage;

    this.paginatedRegisters = this.registers.slice(start, end);
  }

  openPopup(id: number): void{
    this.registerService.getById(id)
      .pipe(takeUntil(this.destroy$))
      .subscribe(register => {
        this.selectedRegister = register;
        this.showPopup = true;
      });
  }

  closePopup(): void {
    this.selectedRegister = null;
    this.showPopup = false;
  }

  printRegisters(): void {
    if (!this.registers || this.registers.length === 0) return;

    let html = `
      <h2>Relatório de Cadastros</h2>
      <table border="1" style="border-collapse: collapse; width: 100%;">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Email</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
    `;

    this.registers.forEach(r => {
      const status = r.status === 'Ativo' ? 'Ativo' : 'Inativo';
      html += `
        <tr>
          <td>${r.name || r.Name}</td>
          <td>${r.email || r.Email}</td>
          <td>${status}</td>
        </tr>
      `;
    });

    html += `</tbody></table>`;

    const printWindow = window.open('', '_blank', 'width=800,height=600');
    if (printWindow) {
      printWindow.document.write(html);
      printWindow.document.close();
      printWindow.focus();

      this.logService.createLog({action: `Impressão de relatório de cadastros`});

      printWindow.print();
      printWindow.close();
    }
  }

  searchRegisters(term: string): void {
    const search = term.trim().toLowerCase();

    if (!search) {
      this.registers = [...this.allRegisters];
      return;
    }

    this.registers = this.allRegisters.filter(r =>
      Object.values(r).some(val =>
        String(val).toLowerCase().includes(search)
      )
    );

    this.currentPage = 1;
    this.calculatePagination();
  }

}
