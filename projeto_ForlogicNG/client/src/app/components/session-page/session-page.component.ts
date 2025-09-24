import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-session-page',
  templateUrl: './session-page.component.html',
  styleUrl: './session-page.component.scss'
})
export class SessionPageComponent {

  @Input({ required: true }) text: string = '';
}
