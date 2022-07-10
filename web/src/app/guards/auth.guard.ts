import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
	constructor(private router: Router) {}

	canActivate() {
		if (!AuthService.isAuthenticate()) {
			this.router.navigate(['/login']);
			return false;
		}

		return true;
	}
}
