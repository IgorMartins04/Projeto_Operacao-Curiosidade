import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-form-input-field',
  templateUrl: './form-input-field.component.html',
  styleUrls: ['./form-input-field.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FormInputFieldComponent),
      multi: true
    }
  ]
})
export class FormInputFieldComponent implements ControlValueAccessor {

  @Input() type: string = 'text';
  @Input() placeholder: string = '';
  @Input() id: string = '';

  value: any = '';
  disabled: boolean = false;

  private onChange: (value: any) => void = () => {};
  private onTouched: () => void = () => {};

  writeValue(value: any): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  inputChanged(event: any) {
    this.value = event.target.value;
    this.onChange(this.value);
  }

  inputBlur() {
    this.onTouched();
  }
}
