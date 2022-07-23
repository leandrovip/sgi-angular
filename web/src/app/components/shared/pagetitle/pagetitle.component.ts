import { Component, Input } from '@angular/core';

@Component({
	selector: 'app-pagetitle',
	template: `
		<div class="pagetitle">
			<h1>{{ this.title }}</h1>
			<nav>
				<ol class="breadcrumb">
					<li class="breadcrumb-item">
						<app-breadcrumb content="Home" link="/"></app-breadcrumb>
					</li>

					<li class="breadcrumb-item" *ngIf="page" [ngClass]="{ active: !section }">
						<app-breadcrumb content="{{ page }}" link="{{ pageLink }}"></app-breadcrumb>
					</li>

					<li class="breadcrumb-item active" *ngIf="section">
						<app-breadcrumb content="{{ section }}" link="{{ sectionLink }}"></app-breadcrumb>
					</li>

					<li class="breadcrumb-item active" *ngIf="section2">
						<app-breadcrumb content="{{ section2 }}"></app-breadcrumb>
					</li>
				</ol>
			</nav>
		</div>
	`,
})
export class PagetitleComponent {
	@Input() title: string = '';
	@Input() page: string = '';
	@Input() pageLink: string;
	@Input() section: string = '';
	@Input() sectionLink: string = '';
	@Input() section2: string = '';
}
