import { Component, OnInit, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Token } from 'src/app/core/models/token.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
	private sideStatus!: boolean;
	public profileImage: string = '';
	public firstName: string = '';
	public user: Token | null = null;

	constructor(private renderer: Renderer2, private router: Router, private toast: ToastrService) {}

	ngOnInit(): void {
		this.sideStatus = true;
		this.loadUser();
	}

	loadUser() {
		this.user = AuthService.getUser();
		if (this.user?.nome) {
			this.profileImage = 'https://ui-avatars.com/api/name=' + this.user.nome.charAt(0);
			this.firstName = this.user.nome.split(' ')[0].trim();
		}
	}

	logout() {
		AuthService.clear();
		this.router.navigate(['/login']);
		this.toast.info(`Até logo, ${this.firstName}`);
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
