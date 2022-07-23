import { Component, Input } from '@angular/core';

@Component({
	selector: 'app-breadcrumb',
	template: `
		<div *ngIf="link; then withLink; else withoutLink"></div>

		<ng-template #withLink>
			<a [routerLink]="[link]">{{ content | titlecase }}</a>
		</ng-template>

		<ng-template #withoutLink>{{ content | titlecase }}</ng-template>
	`,
})
export class BreadcrumbComponent {
	@Input() content: string;
	@Input() link: string;
}
