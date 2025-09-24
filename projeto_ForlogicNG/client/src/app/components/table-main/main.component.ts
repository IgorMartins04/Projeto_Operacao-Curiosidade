import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent {

  @Input({ required: true }) titleTable: string = '';
  @Input({ required: true }) idTable: string = '';
  @Input({ required: true }) columns: { key: string, label: string }[] = [];
  @Input({ required: true }) registers: any[] = [];

  @Output() rowClick = new EventEmitter<number>();
  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();


  onRowClick(id: number) {
    this.rowClick.emit(id);
  }

  getStatusLabel(status: string | boolean): string {
  if (typeof status === 'boolean') {
    return status ? 'Ativo' : 'Inativo';
  }
  if (typeof status === 'string') {
    return status.toLowerCase() === 'ativo' ? 'Ativo' : 'Inativo';
  }
  return 'Inativo'; 
}

isInactive(register: any): boolean {
  const hasStatusColumn = this.columns?.some(c => c.key === 'status');
  return hasStatusColumn && this.getStatusLabel(register.status) === 'Inativo';
}



}
