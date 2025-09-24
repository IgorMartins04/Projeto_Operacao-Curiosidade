import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode';
import { RegisterService } from '../../services/register.service';

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.scss']
})
export class DashBoardComponent implements OnInit, OnDestroy {

  user: string = 'Usuário';

  totalRegisters = 0;
  lastMonthRegisters = 0;
  pendencies = 0;

  registers: any[] = [];
  filteredRegisters: any[] = [];
  searchTerm: string = '';

  selectedRegister: any = null;
  showPopup: boolean = false;
  selectedCard: 'total-registers' | 'last-registers' | 'pendencies' | null = 'total-registers';


  private destroy$ = new Subject<void>();

  constructor(private registerService: RegisterService) {}

  ngOnInit(): void {
    this.setUserFromToken();
    this.loadCards();
    this.loadRegisters();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
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
    } catch (err) {
      console.error('Erro ao decodificar token', err);
      this.user = 'Usuário';
    }
  }

  private loadCards(): void {
    this.registerService.getTotal()
      .pipe(takeUntil(this.destroy$))
      .subscribe(total => this.totalRegisters = total);

    this.registerService.getLastMonth()
      .pipe(takeUntil(this.destroy$))
      .subscribe(last => this.lastMonthRegisters = last);

    this.registerService.getPendencies()
      .pipe(takeUntil(this.destroy$))
      .subscribe(p => this.pendencies = p);
  }

  private loadRegisters(): void {
    this.registerService.getAll()
      .pipe(takeUntil(this.destroy$))
      .subscribe(data => {
        this.registers = data;
        this.filteredRegisters = [...this.registers];
      });
  }

  searchRegisters(term: string) {
    const search = term.trim().toLowerCase();

    if (!search) {
      this.filteredRegisters = [...this.registers];
      return;
    }

    this.filteredRegisters = this.registers.filter(r => 
      Object.values(r).some(val => 
        String(val).toLowerCase().includes(search)
      )
    );
  }

  filterByCard(type: 'total-registers' | 'last-registers' | 'pendencies'): void {
    this.selectedCard = type;
    switch(type) {
      case 'total-registers':
        this.filteredRegisters = [...this.registers];
        break;
      case 'last-registers':
        const oneMonthAgo = new Date();
        oneMonthAgo.setMonth(oneMonthAgo.getMonth() - 1);
        this.filteredRegisters = this.registers.filter(r => new Date(r.dateRegister) >= oneMonthAgo);
        break;
      case 'pendencies':
        this.filteredRegisters = this.registers.filter(r => r.status.toLowerCase() === 'inativo');
        break;
      }
  }

  openRegisterPopup(id: number): void {
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
}
