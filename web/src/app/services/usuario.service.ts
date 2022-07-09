import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IReturn } from '../interfaces/return.interface';
import { AccessToken } from '../models/accessToken.model';
import { Usuario } from '../models/usuario.model';

@Injectable({
    providedIn: 'root',
})
export class UsuarioService {
    public url = 'https://localhost:7187/usuario';

    constructor(private http: HttpClient) {}

    obterLista() {
        var usuarios = this.http.get<IReturn<Usuario[]>>(this.url);
        console.log('passou no service');
        return usuarios;
    }

    autenticar(data: string) {
        return this.http.post<IReturn<AccessToken>>(`${this.url}/autenticar`, data);
    }
}
