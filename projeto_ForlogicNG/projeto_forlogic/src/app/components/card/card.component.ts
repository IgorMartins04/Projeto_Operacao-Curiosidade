import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss'
})
export class CardComponent {

  @Input({ required: true }) idCard: string = '';
  @Input({ required: true }) classCard: string = '';
  @Input({ required: true }) idData: string = '';
  @Input({ required: true }) textCard: string = '';
  @Input({ required: true }) numberCard: number = 0;

}
