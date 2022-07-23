import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
	constructor() {}

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		const token = AuthService.getToken();

		if (token) {
			request = request.clone({
				setHeaders: {
					Authorization: `bearer ${token}`,
				},
			});
		}

		// const headers = req.headers.set('Content-Type', 'application/json');
		// const authReq = req.clone({ headers });

		return next.handle(request);
	}
}
