import { Component, OnInit, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { AccessToken } from 'src/app/models/accessToken.model';
import { Security } from 'src/app/utils/security.util';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
    private sideStatus!: boolean;
    public profileImage: string = '';
    public firstName: string = '';
    public user: AccessToken | null;

    constructor(private renderer: Renderer2, private router: Router) {}

    ngOnInit(): void {
        this.sideStatus = true;
        this.loadUser();
    }

    loadUser() {
        this.user = Security.get();
        if (this.user) {
            this.profileImage = 'https://ui-avatars.com/api/name=' + this.user.nome.charAt(0);
            this.firstName = this.user.nome.split(' ')[0];
        }
    }

    logout() {
        Security.clear();
        this.router.navigate(['/login']);
    }

    toggleSide() {
        if (this.sideStatus) {
            this.renderer.addClass(document.body, 'toggle-sidebar');
        } else {
            this.renderer.removeClass(document.body, 'toggle-sidebar');
        }
        this.sideStatus = !this.sideStatus;
    }
}
