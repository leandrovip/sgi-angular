import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

import { IReturn } from '../core/interfaces/return.interface';

export abstract class BaseService<T> {
	protected baseApi: string = '';
	protected baseUrl: string = '';

	constructor(protected httpClient: HttpClient, baseRoute: string) {
		this.baseApi = environment.baseUrlAPI;
		this.baseUrl = `${this.baseApi}/${baseRoute}`;
	}

	//#region métodos básicos

	obter(id?: string) {
		const endpoint = id ? `/${id}` : '';
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

	//#endregion
}
