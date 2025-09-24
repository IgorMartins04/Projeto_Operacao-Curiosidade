import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { RegisterService } from '../../services/register.service';
import { jwtDecode } from 'jwt-decode';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LogService } from '../../services/log.service';

@Component({
  selector: 'app-registers',
  templateUrl: './registers.component.html',
  styleUrls: ['./registers.component.scss']
})
export class RegistersComponent implements OnInit, OnDestroy {
  user: string = 'Usuário';
  formCreate!: FormGroup;
  formEdit!: FormGroup;

  registers: any[] = [];
  filteredRegisters: any[] = [];
  paginatedRegisters: any[] = [];

  showCreatePopup = false;
  showDeletePopup = false;
  showEditPopup = false;
  selectedRegister: any = null;

  showDeleteSuccess: boolean = false;
  showEditSuccess: boolean = false;
  showCreateSuccess: boolean = false;

  currentPage: number = 1;
  registersPerPage: number = 6;
  totalPages: number = 1;

  showPopup: boolean = false;
  selectedRegisterIdToDelete: number | null = null;

  searchTerm: string = '';

  private destroy$ = new Subject<void>();

  private regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  private regexName = /^[A-ZÀ-Ÿ][A-ZÀ-Ÿa-zà-ÿ']+(?: [A-ZÀ-Ÿa-zà-ÿ']+)+$/;
  private regexAge = /^(1[89]|[2-9][0-9])$/;

  constructor(
    private registerService: RegisterService,
    private fb: FormBuilder, 
    private logService: LogService
  ) {}

  ngOnInit(): void {
    this.setUserFromToken();
    this.loadRegisters();

    this.formCreate = this.fb.group({
      status: [false],
      name: ['', [Validators.required, Validators.pattern(this.regexName)]],
      age: ['', [Validators.required, Validators.pattern(this.regexAge)]],
      email: ['', [Validators.required, Validators.pattern(this.regexEmail)]],
      address: ['', Validators.required],
      info: [''],
      interests: [''],
      feelings: [''],
      mValues: ['']
    }, { updateOn: 'submit'});

    this.formEdit = this.fb.group({
      'edit-id': [''],
      'edit-status': [false],
      'edit-name': ['', [Validators.required, Validators.pattern(this.regexName)]],
      'edit-age': ['', [Validators.required, Validators.pattern(this.regexAge)]],
      'edit-email': ['', [Validators.required, Validators.pattern(this.regexEmail)]],
      'edit-address': ['', Validators.required],
      'edit-infos': [''],
      'edit-interests': [''],
      'edit-feelings': [''],
      'edit-mValues': ['']
    });
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
    } catch {
      this.user = 'Usuário';
    }
  }

  loadRegisters(): void {
    this.registerService.getAll()
      .pipe(takeUntil(this.destroy$))
      .subscribe(data => {
        this.registers = data.map(r => ({
          ...r,
          Status: r.status
        }));
        this.filteredRegisters = [...this.registers];
        this.calculatePagination();
    });
  } 

  calculatePagination(): void {
  this.totalPages = Math.ceil(this.filteredRegisters.length / this.registersPerPage);
    this.changePage(this.currentPage);
  }

  changePage(page: number): void {
    if (page < 1) page = 1;
    if (page > this.totalPages) page = this.totalPages;

    this.currentPage = page;

    const start = (this.currentPage - 1) * this.registersPerPage;
    const end = start + this.registersPerPage;

    this.paginatedRegisters = this.filteredRegisters.slice(start, end);
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

  openCreatePopup(): void {
    this.showCreatePopup = true;
  }

  closeCreatePopup(): void {
    this.showCreatePopup = false;
    this.formCreate.reset();
  }

  openDeletePopup(id: number): void {
    this.selectedRegisterIdToDelete = id;
    this.showDeletePopup = true;
  }

  closeDeletePopup(): void {
    this.selectedRegisterIdToDelete = null;
    this.showDeletePopup = false;
  }

  openEditPopup(register: any): void {
    this.formEdit.patchValue({
      'edit-id': register.id,
      'edit-status': register.status,
      'edit-name': register.name,
      'edit-age': register.age,
      'edit-email': register.email,
      'edit-address': register.address,
      'edit-infos': register.info,
      'edit-interests': register.interests,
      'edit-feelings': register.feelings,
      'edit-mValues': register.mValues
    });
    this.showEditPopup = true;
  }

  closeEditPopup(): void {
    this.selectedRegister = null;
    this.showEditPopup = false;
    this.formEdit.reset();
  }

  createRegister(): void {
    if (this.formCreate.invalid) {
      this.formCreate.markAllAsTouched();
      return;
    }

    const payload = {
      Name: this.formCreate.value.name,
      Email: this.formCreate.value.email,
      Status: !!this.formCreate.value.status,
      Age: Number(this.formCreate.value.age),
      Address: this.formCreate.value.address,
      Info: this.formCreate.value.info,
      Interests: this.formCreate.value.interests,
      Feelings: this.formCreate.value.feelings,
      MValues: this.formCreate.value.mValues,
      DateRegister: new Date().toISOString().split('T')[0],
      Removed: false
    };

    this.registerService.create(payload)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (res) => {
          this.closeCreatePopup();
          this.loadRegisters();
          this.showCreateSuccess = true

          this.logService.createLog({ action: `Criado: ${payload.Name}`, registerId: res.id })
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            error: (err) => console.error('Erro ao criar log:', err)
          })
          
          setTimeout(() => {
            this.showCreateSuccess = false;
          }, 3000);
        },
        error: (err) => {
          console.error('Erro ao criar:', err);
        }
      });
  }

  onSaveEdit(): void {
    if (this.formEdit.invalid) {
      this.formEdit.markAllAsTouched();
      return;
    }

    const updated = {
      id: Number(this.formEdit.value['edit-id']),
      status: !!this.formEdit.value['edit-status'],
      name: this.formEdit.value['edit-name'],
      age: Number(this.formEdit.value['edit-age']),
      email: this.formEdit.value['edit-email'],
      address: this.formEdit.value['edit-address'],
      info: this.formEdit.value['edit-infos'],
      interests: this.formEdit.value['edit-interests'],
      feelings: this.formEdit.value['edit-feelings'],
      mValues: this.formEdit.value['edit-mValues']
    };

    const id = this.formEdit.value['edit-id'];

    this.registerService.update(id, updated)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          const index = this.registers.findIndex(r => r.id === id);
          if (index !== -1) {
            this.registers[index] = { ...updated };
            this.filteredRegisters = [...this.registers];
          }
          this.closeEditPopup();
          this.showEditSuccess = true;

          this.logService.createLog({ action: `Editado: ${updated.name}`, registerId: id })
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            error: (err) => console.error('Erro ao criar log:', err)
          });

          setTimeout(() => {
            this.showEditSuccess = false;
          }, 3000);
        },
        error: (err) => {
          console.error('Erro ao editar:', err);
        }
      });
  }

  deleteRegister(): void {
    if (!this.selectedRegisterIdToDelete) return;

    const idToDelete = this.selectedRegisterIdToDelete;
    const registerName = this.registers.find(r => r.id === idToDelete)?.name || 'Registro';

    this.registerService.delete(this.selectedRegisterIdToDelete)
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.loadRegisters(); 
        this.closeDeletePopup();
        this.showDeleteSuccess = true;

        this.logService.createLog({ action: `Deletado: ${registerName}`, registerId: idToDelete })
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            error: (err) => console.error('Erro ao criar log:', err)
          });

        setTimeout(() => {
          this.showDeleteSuccess = false;
        }, 3000);
        this.loadRegisters();
      });
  }

  searchRegisters(term: string): void {
  const search = term.trim().toLowerCase();

  if (!search) {
    this.filteredRegisters = [...this.registers];
  } else {
    this.filteredRegisters = this.registers.filter(r =>
      Object.values(r).some(val =>
        String(val).toLowerCase().includes(search)
      )
    );
  }

  this.currentPage = 1;
  this.calculatePagination();
}


  isCreateInvalid(field: string): boolean {
    const control = this.formCreate.get(field);
    return !!control && control.invalid && (control.dirty || control.touched);
  }

  isEditInvalid(field: string): boolean {
    const control = this.formEdit.get(field);
    return !!control && control.invalid && (control.dirty || control.touched);
  }

  getErrorMessage(field: string): string {
    const control = this.formCreate.get(field);

    if (!control) return '';

    if (control.hasError('required')) {
      return 'Campo obrigatório';
    }

    if (control.hasError('pattern')) {
      switch (field) {
        case 'name':
          return 'Digite um nome válido (completo, iniciando com letra maiúscula)';
        case 'age':
          return 'Idade deve ser maior ou igual a 18';
        case 'email':
          return 'Formato de e-mail inválido';
      }
    }

    return '';
  }

}
