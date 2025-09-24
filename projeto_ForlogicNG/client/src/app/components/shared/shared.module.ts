import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonFormComponent } from '../button-form/button-form.component';
import { FormInputFieldComponent } from '../form-input-field/form-input-field.component';
import { FormComponent } from '../form/form.component';
import { SessionPageComponent } from '../session-page/session-page.component';
import { AsideComponent } from '../aside/aside.component';
import { HeaderComponent } from '../header/header.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MainComponent } from '../table-main/main.component';
import { FooterComponent } from '../footer/footer.component';
import { CardComponent } from '../card/card.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { PopupDatailsComponent } from '../popup-datails/popup-datails.component';

@NgModule({
  declarations: [
    ButtonFormComponent,
    FormInputFieldComponent,
    FormComponent,
    SessionPageComponent,
    AsideComponent,
    HeaderComponent,
    MainComponent,
    FooterComponent,
    CardComponent,
    PopupDatailsComponent,
  ],
  imports: [
    CommonModule,
    MatSlideToggleModule,
    RouterModule,
    ReactiveFormsModule, 
  ],
  exports:[
    ButtonFormComponent,
    FormInputFieldComponent,
    FormComponent,
    SessionPageComponent,
    AsideComponent,
    HeaderComponent,
    MainComponent,
    FooterComponent,
    CardComponent,
    RouterModule,
    PopupDatailsComponent,
  ]
})
export class SharedModule { }
