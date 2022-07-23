import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { IReturn } from '../core/interfaces/return.interface';
import { Token } from '../core/models/token.model';
import { Usuario } from '../core/models/usuario.model';
import { BaseService } from './base.service';

@Injectable({
	providedIn: 'root',
})
export class PerfilService extends BaseService<Usuario> {
	constructor(private http: HttpClient) {
		super(http, 'perfil');
	}

	autenticar(data: string) {
		return this.http.post<IReturn<Token>>(`${this.baseUrl}/autenticar`, data);
	}
}
