import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IReturn } from 'src/app/core/interfaces/return.interface';
import { Usuario } from 'src/app/core/models/usuario.model';
import { AlertService } from 'src/app/services/alert.service';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
	selector: 'app-usuario-selecionar',
	templateUrl: './usuario-selecionar.component.html',
})
export class UsuarioSelecionarComponent implements OnInit {
	public usuarios!: Observable<IReturn<Usuario[]>>;

	constructor(private service: UsuarioService, private alert: AlertService) {}

	ngOnInit(): void {
		this.atualizarGrid();
	}

	public excluir(usuarioId: string) {
		if (!usuarioId) {
			this.alert.warning({ message: 'Código do usuário inválido' });
			return;
		}

		this.service.excluir(usuarioId).subscribe({
			next: (result) => {
				if (result && result.success) {
					this.alert.success(result);
					this.atualizarGrid();
				}
			},
			error: (err) => {
				this.alert.error({ err: err.error });
			},
		});
	}

	private atualizarGrid() {
		this.usuarios = this.service.obterLista();
	}
}
