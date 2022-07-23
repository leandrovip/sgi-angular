import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { ToastrService } from 'ngx-toastr';
import { Return } from 'src/app/core/interfaces/return.interface';
import { Usuario } from 'src/app/core/models/usuario.model';
import { FormHelper } from 'src/app/helpers/form.helper';
import { AuthService } from 'src/app/services/auth.service';
import { PerfilService } from 'src/app/services/pefil.service';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
	selector: 'app-usuario-form',
	templateUrl: './usuario-form.component.html',
})
export class UsuarioFormComponent implements OnInit {
	public form: FormGroup;
	public formHelper: FormHelper;
	public urlName: string = '';
	public ocupado: boolean = false;

	constructor(
		private fb: FormBuilder,
		private perfilService: PerfilService,
		private usuarioService: UsuarioService,
		private location: Location,
		private router: Router,
		private toast: ToastrService
	) {
		this.form = this.fb.group({
			usuarioId: [''],
			nome: ['', Validators.compose([Validators.required])],
			email: ['', Validators.compose([Validators.required, FormHelper.email])],
			usuarioFuncao: ['', Validators.compose([Validators.required])],
			senha: ['', Validators.compose([Validators.required])],
		});
		this.formHelper = new FormHelper(this.form);
	}

	ngOnInit(): void {
		const endpoint = this.router.routerState.snapshot.url.split('/');
		const index = endpoint.length > 2 ? 2 : 1;
		this.urlName = endpoint[endpoint.length - index];

		this.obterUsuario();
	}

	submit() {
		this.ocupado = true;

		if (this.urlName == 'perfil') {
			this.perfilService.editar(this.form.getRawValue()).subscribe({
				next: (result) => {
					if (result && result.success) {
						this.ocupado = false;
						AuthService.setName(result.data.nome);
						this.toast.success('Perfil salvo com sucesso!');
						this.router.navigate(['']);
					}
				},
				error: (err) => {
					this.ocupado = false;
					this.toast.error(err.message, 'Que pena!');
					this.router.navigate(['']);
				},
			});
		} else if (this.urlName == 'incluir') {
			const usuarioId = this.form.get('usuarioId');
			if (!usuarioId?.value) {
				usuarioId?.setValue(Guid.create().toString());
			}

			this.usuarioService.incluir(this.form.value).subscribe({
				next: (result) => {
					if (result && result.success) {
						this.ocupado = false;
						this.toast.success('Usuário salvo com sucesso!');
						this.location.back();
					}
				},
				error: (err) => {
					this.ocupado = false;
					console.log(err);
					this.toast.error(err.error.events[0], 'Que pena!');
				},
			});
		}
	}

	obterUsuario(): void {
		this.ocupado = true;

		if (this.urlName == 'perfil') {
			if (!AuthService.isAuthenticate()) {
				this.toast.warning('Ação não permitida', 'Ops');
				this.router.navigate(['/login']);
				return;
			}

			this.form.get('email')?.disable();
			this.form.get('usuarioFuncao')?.disable();
			this.form.get('senha')?.clearValidators();

			this.perfilService.obter().subscribe({
				next: (result) => {
					if (result && result.success) {
						this.preencherForm(result.data);
					}

					this.ocupado = false;
				},
				error: (err) => {
					const message = (err.error as Return).message
						? (err.error as Return).message
						: 'Não foi possível obter o usuário';

					this.toast.error(message, 'Que pena!');
					this.router.navigate(['']);
					this.ocupado = false;
				},
			});
		}
	}

	preencherForm(usuario: Usuario) {
		this.form.patchValue({
			usuarioId: usuario.usuarioId,
			nome: usuario.nome,
			email: usuario.email,
			usuarioFuncao: usuario.usuarioFuncao,
		});
	}
}
