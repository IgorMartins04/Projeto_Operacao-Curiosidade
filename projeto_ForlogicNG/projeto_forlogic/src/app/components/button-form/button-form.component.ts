import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-button-form',
  templateUrl: './button-form.component.html',
  styleUrl: './button-form.component.scss'
})
export class ButtonFormComponent {

  @Input({ required: true}) textBt: string = '';
  @Input ({ required: true }) id: string = '';

}
