import { UsuarioFuncao } from '../core/enums/usuarioFuncao.enum';
import { Token } from '../core/models/token.model';

export class AuthService {
	public static userPath: string = 'vipsgi.user';

	public static setUser(token: Token) {
		const data = JSON.stringify(token);
		localStorage.setItem(this.userPath, btoa(data));
	}

	public static setName(name: string) {
		let user = this.getUser();
		if (user) {
			user.nome = name;
			this.setUser(user);
		}
	}

	public static getUser(): Token | null {
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
		return this.roleStringToEnum(funcao ?? UsuarioFuncao.Colaborador);
	}

	public static roleStringToEnum(role: string): UsuarioFuncao {
		let funcaoEnum = role as keyof typeof UsuarioFuncao;
		return UsuarioFuncao[funcaoEnum];
	}

	public static isAuthenticate(): boolean {
		const user = this.getUser();
		return this.isValid(user);
	}

	public static clear() {
		localStorage.removeItem(this.userPath);
	}

	private static isValid(user: Token | null): boolean {
		return user && user.accessToken ? true : false;
	}
}
