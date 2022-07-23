import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { IReturn } from 'src/app/core/interfaces/return.interface';
import { Usuario } from 'src/app/core/models/usuario.model';
import { FormHelper } from 'src/app/helpers/form.helper';
import { AlertService } from 'src/app/services/alert.service';
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

	@ViewChild('txtNome') txtNome: ElementRef;

	constructor(
		private fb: FormBuilder,
		private perfilService: PerfilService,
		private usuarioService: UsuarioService,
		private route: ActivatedRoute,
		private alert: AlertService
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
		const endpoint = this.alert.getUrl().split('/');
		const index = endpoint.length > 3 ? 2 : 1;
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
						this.alert.success({ message: 'Perfil salvo com sucesso', urlNavigate: [''] });
					}
				},
				error: (result) => {
					this.ocupado = false;
					this.alert.error({ err: result.error, urlNavigate: [''] });
				},
			});
		} else if (this.urlName == 'incluir') {
			const usuarioId = this.form.get('usuarioId');
			usuarioId?.setValue(Guid.create().toString());

			this.usuarioService.incluir(this.form.value).subscribe({
				next: (result) => {
					if (result && result.success) {
						this.ocupado = false;
						this.alert.success({ result: result, pageBack: true });
					}
				},
				error: (result) => {
					this.ocupado = false;
					this.alert.error({ err: result.error });
				},
			});
		} else if (this.urlName == 'editar') {
			this.usuarioService.editar(this.form.value).subscribe({
				next: (result) => {
					if (result && result.success) {
						this.ocupado = false;
						this.alert.success({ result: result, pageBack: true });
					}
				},
				error: (result) => {
					this.ocupado = false;
					this.alert.error({ err: result.error });
				},
			});
		}
	}

	private obterUsuario(): void {
		this.ocupado = true;

		if (this.urlName == 'perfil') {
			if (!AuthService.isAuthenticate()) {
				this.alert.warning({ message: 'Ação não permitida', urlNavigate: ['/login'] });
				return;
			}

			this.form.get('email')?.disable();
			this.form.get('usuarioFuncao')?.disable();
			this.form.get('senha')?.clearValidators();

			this.perfilService.obter().subscribe({
				next: (result) => {
					this.preencherCampos(result);
					this.ocupado = false;
				},
				error: (result) => {
					this.alert.error({ err: result.error, urlNavigate: [''] });
					this.ocupado = false;
				},
			});
		} else if (this.urlName == 'editar') {
			const usuarioId = this.route.snapshot.params['id'];
			if (!usuarioId) {
				this.alert.warning({ message: 'Usuário não encontrado', pageBack: true });
				return;
			}

			this.form.get('senha')?.clearValidators();

			this.usuarioService.obter(usuarioId).subscribe({
				next: (result) => {
					this.preencherCampos(result);
					this.ocupado = false;
				},
				error: (result) => {
					this.alert.error({ err: result.error, pageBack: true });
					this.ocupado = false;
				},
			});
		} else if (this.urlName == 'incluir') {
			this.ocupado = false;
		}
	}

	private preencherCampos(result: IReturn<Usuario>) {
		if (!result || !result.success) {
			return;
		}

		const usuario = result.data;
		this.form.patchValue({
			usuarioId: usuario.usuarioId,
			nome: usuario.nome,
			email: usuario.email,
			usuarioFuncao: usuario.usuarioFuncao,
		});

		this.txtNome.nativeElement.focus();
	}
}
