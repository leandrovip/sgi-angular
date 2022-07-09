import { AccessToken } from '../models/accessToken.model';

export class Security {
    public static userPath: string = 'vipsgi.user';

    public static set(token: AccessToken) {
        const data = JSON.stringify(token);
        localStorage.setItem(this.userPath, btoa(data));
    }

    public static get(): AccessToken | null {
        const data = localStorage.getItem(this.userPath);
        if (data) {
            return JSON.parse(atob(data));
        }

        return null;
    }

    public static getToken(): string {
        const user = this.get();
        if (user && user.accessToken) {
            return user.accessToken;
        }

        return '';
    }

    public static clear() {
        localStorage.removeItem(this.userPath);
    }
}
