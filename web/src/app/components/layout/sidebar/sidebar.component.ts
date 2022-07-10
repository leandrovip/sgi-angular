import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
	selector: 'app-sidebar',
	templateUrl: './sidebar.component.html',
})
export class SidebarComponent implements OnInit {
	constructor(private router: Router) {}

	ngOnInit(): void {}

	logout() {
		AuthService.clear();
		this.router.navigate(['/login']);
	}
}
