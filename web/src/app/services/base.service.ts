import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

import { IReturn, Return } from '../core/interfaces/return.interface';

export abstract class BaseService<T> {
	protected baseApi: string = '';
	protected baseUrl: string = '';

	constructor(protected httpClient: HttpClient, baseRoute: string) {
		this.baseApi = environment.baseUrlAPI;
		this.baseUrl = `${this.baseApi}/${baseRoute}`;
	}

	obter(id?: string) {
		const endpoint = id != undefined ? `/${id}` : '';
		console.log(endpoint);
		return this.httpClient.get<IReturn<T>>(`${this.baseUrl}${endpoint}`);
	}

	obterLista() {
		return this.httpClient.get<IReturn<T[]>>(`${this.baseUrl}`);
	}

	incluir(data: string) {
		return this.httpClient.post<IReturn<T>>(this.baseUrl, data);
	}

	editar(data: string) {
		return this.httpClient.put<IReturn<T>>(this.baseUrl, data);
	}

	excluir(id: string) {
		return this.httpClient.delete<Return>(`${this.baseUrl}/${id}`);
	}
}
