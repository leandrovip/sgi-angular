import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormHelper } from 'src/app/helpers/form.helper';
import { Return } from 'src/app/core/interfaces/return.interface';
import { Token } from 'src/app/core/models/token.model';
import { AuthService } from 'src/app/services/auth.service';
import { PerfilService } from 'src/app/services/pefil.service';

@Component({
	selector: 'app-login',
	templateUrl: './usuario-login.component.html',
})
export class UsuarioLoginComponent implements OnInit {
	public form: FormGroup;
	public formHelper: FormHelper;
	public ocupado: boolean = false;

	constructor(
		private service: PerfilService,
		private fb: FormBuilder,
		private router: Router,
		private toastr: ToastrService
	) {
		this.form = this.fb.group({
			email: ['', Validators.compose([Validators.required, FormHelper.email])],
			senha: ['', Validators.compose([Validators.required])],
		});
		this.formHelper = new FormHelper(this.form);
	}

	ngOnInit(): void {
		if (AuthService.isAuthenticate()) {
			this.login();
		}
	}

	submit() {
		this.ocupado = true;
		this.service.autenticar(this.form.value).subscribe({
			next: (result) => {
				if (result && result.success) {
					this.login(result.data);
					this.toastr.success(`Olá, ${result.data.nome}`, 'Bem-vindo!');
				}
				this.ocupado = false;
			},
			error: (err) => {
				const message = (err.error as Return).message
					? (err.error as Return).message
					: 'Não foi possível efetuar o login';

				this.toastr.error(message, 'Que pena!');
				this.form.get('senha')?.reset();
				this.ocupado = false;
			},
		});
	}

	login(user: Token | null = null) {
		if (user) AuthService.setUser(user);
		this.router.navigate(['/']);
	}
}
