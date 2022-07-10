import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Return } from 'src/app/interfaces/return.interface';
import { AccessToken } from 'src/app/models/accessToken.model';
import { UsuarioService } from 'src/app/services/usuario.service';
import { FormHelper } from 'src/app/helpers/form.helper';
import { AuthService } from 'src/app/services/auth.service';

@Component({
	selector: 'app-login',
	templateUrl: './usuario-login.component.html',
})
export class UsuarioLoginComponent implements OnInit {
	public form: FormGroup;
	public formHelper: FormHelper;
	public ocupado: boolean = false;

	constructor(
		private service: UsuarioService,
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

	login(user: AccessToken | null = null) {
		if (user) AuthService.setUser(user);
		this.router.navigate(['/']);
	}
}
