import { UsuarioFuncao } from '../enums/usuarioFuncao.enum';
import { AccessToken } from '../models/accessToken.model';

export class AuthService {
	public static userPath: string = 'vipsgi.user';

	public static setUser(token: AccessToken) {
		const data = JSON.stringify(token);
		localStorage.setItem(this.userPath, btoa(data));
	}

	public static getUser(): AccessToken | null {
		const data = localStorage.getItem(this.userPath);
		return data ? JSON.parse(atob(data)) : null;
	}

	public static getToken(): string {
		const user = this.getUser();
		if (this.isValid(user)) {
			return user?.accessToken ?? '';
		}
		return '';
	}

	public static getRole(): UsuarioFuncao {
		const funcao = this.getUser()?.usuarioFuncao;
		let funcaoEnum = funcao as keyof typeof UsuarioFuncao;
		return UsuarioFuncao[funcaoEnum];
	}

	public static isAuthenticate(): boolean {
		const user = this.getUser();
		return this.isValid(user);
	}

	public static clear() {
		localStorage.removeItem(this.userPath);
	}

	private static isValid(user: AccessToken | null): boolean {
		return user && user.accessToken ? true : false;
	}
}
