import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IReturn } from '../interfaces/return.interface';
import { AccessToken } from '../models/accessToken.model';
import { Usuario } from '../models/usuario.model';

@Injectable({
	providedIn: 'root',
})
export class UsuarioService {
	// Refatorar
	private url = 'https://localhost:7187/usuario';

	constructor(private http: HttpClient) {}

	// Todo: Refatorar
	obterLista() {
		return this.http.get<IReturn<Usuario[]>>(this.url);
	}

	autenticar(data: string) {
		return this.http.post<IReturn<AccessToken>>(`${this.url}/autenticar`, data);
	}
}
