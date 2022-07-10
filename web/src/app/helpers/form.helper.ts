import { FormGroup, FormControl } from '@angular/forms';

export class FormHelper {
	constructor(private form: FormGroup) {}

	validate(value: string): string {
		let campo = this.form.get(value);
		if (!campo?.dirty) return '';
		return !campo?.valid && !campo?.pristine ? 'is-invalid' : 'is-valid';
	}

	static email(control: FormControl) {
		const re = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,6})$/;

		if (!re.test(control.value)) {
			return { 'Email invalido': true };
		}
		return null;
	}
}
