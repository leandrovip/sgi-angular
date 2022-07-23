import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { PagetitleComponent } from './pagetitle/pagetitle.component';

@NgModule({
	imports: [CommonModule, RouterModule],
	declarations: [PagetitleComponent, BreadcrumbComponent],
	exports: [PagetitleComponent, BreadcrumbComponent],
})
export class SharedModule {}
