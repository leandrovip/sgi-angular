import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { MainComponent } from './main/main.component';
import { SidebarComponent } from './sidebar/sidebar.component';

@NgModule({
	imports: [CommonModule, RouterModule],
	declarations: [MainComponent, HeaderComponent, SidebarComponent, FooterComponent],
	providers: [],
})
export class LayoutModule {}
