import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-popup-datails',
  templateUrl: './popup-datails.component.html',
  styleUrl: './popup-datails.component.scss'
})
export class PopupDatailsComponent {

  @Input() register: any;
  @Output() close = new EventEmitter<void>();

  closePopup() {
    this.close.emit(); 
  }

}
