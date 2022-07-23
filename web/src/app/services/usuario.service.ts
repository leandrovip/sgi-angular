import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Usuario } from '../core/models/usuario.model';
import { BaseService } from './base.service';

@Injectable({
	providedIn: 'root',
})
export class UsuarioService extends BaseService<Usuario> {
	constructor(private http: HttpClient) {
		super(http, 'usuario');
	}
}
