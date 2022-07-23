import { Location } from '@angular/common';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { IReturn, Return } from '../core/interfaces/return.interface';

@Injectable({ providedIn: 'root' })
export class AlertService {
	constructor(private toast: ToastrService, private location: Location, private router: Router) {}

	success(options: { result?: IReturn<any>; pageBack?: boolean; urlNavigate?: any[]; message?: string }) {
		let title = 'Boa notícia!';
		let message = options?.message ? options.message : 'Ação executada com sucesso';

		if (options.result && options.result.message) {
			message = options.result.message;
		}

		this.toast.success(message, title);
		this.navigate(options?.pageBack, options?.urlNavigate);
	}

	error(options: { err?: Return; pageBack?: boolean; urlNavigate?: any[]; message?: string }) {
		let title = 'Que pena!';
		let message = options?.message ? options.message : 'Erro não identificado';

		if (options.err && options.err.events) {
			title = options.err.message;
			message = options.err.events[0];
		}

		this.toast.error(message, title);
		this.navigate(options?.pageBack, options?.urlNavigate);
	}

	warning(options?: { err?: Return; pageBack?: boolean; urlNavigate?: any[]; message: string }) {
		let title = 'Ops!';
		let message = options?.message ? options.message : 'Ação não executada';

		if (options?.err && options.err.events) {
			title = options.err.message;
			message = options.err.events[0];
		}

		this.toast.warning(message, title);
		this.navigate(options?.pageBack, options?.urlNavigate);
	}

	getUrl(): string {
		return this.location.path();
	}

	private navigate(pageBack?: boolean, urlNavigate?: any[]) {
		if (pageBack) {
			this.location.back();
			return;
		}

		if (urlNavigate) {
			this.router.navigate(urlNavigate);
			return;
		}
	}
}
