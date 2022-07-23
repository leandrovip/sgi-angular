import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';

import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
	constructor(private router: Router, private toast: ToastrService) {}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
		let user = AuthService.getUser();
		if (user) {
			// Verifica se token expirado
			if (new JwtHelperService().isTokenExpired(user.accessToken)) {
				AuthService.clear();
				this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
				return false;
			}

			// Veririca função do usuário
			let role = route.data['role'];
			if (role) {
				if (AuthService.roleStringToEnum(role) != AuthService.roleStringToEnum(user.usuarioFuncao)) {
					this.toast.warning('Acesso não autorizado', 'Ops');
					this.router.navigate(['/login']); // Todo: criar unauthorized page
					return false;
				}
			}

			return true;
		}

		this.router.navigate(['/login']);
		return false;
	}
}
