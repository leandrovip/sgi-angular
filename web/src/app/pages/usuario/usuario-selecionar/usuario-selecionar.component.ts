import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IReturn } from 'src/app/core/interfaces/return.interface';
import { Usuario } from 'src/app/core/models/usuario.model';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
	selector: 'app-usuario-selecionar',
	templateUrl: './usuario-selecionar.component.html',
})
export class UsuarioSelecionarComponent implements OnInit {
	public usuarios!: Observable<IReturn<Usuario[]>>;

	constructor(private service: UsuarioService, private router: Router) {}

	ngOnInit(): void {
		this.usuarios = this.service.obterLista();
	}
}
