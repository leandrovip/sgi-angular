import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './components/layout/main/main.component';
import { HeaderComponent } from './components/layout/header/header.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UsuarioLoginComponent } from './pages/usuario/usuario-login/usuario-login.component';
import { UsuarioCadastroComponent } from './pages/usuario/usuario-cadastro/usuario-cadastro.component';
import { UsuarioSelecionarComponent } from './pages/usuario/usuario-selecionar/usuario-selecionar.component';
import { UsuarioMinhacontaComponent } from './pages/usuario/usuario-minhaconta/usuario-minhaconta.component';

@NgModule({
	declarations: [
		AppComponent,
		MainComponent,
		HeaderComponent,
		SidebarComponent,
		FooterComponent,
		DashboardComponent,
		UsuarioLoginComponent,
		UsuarioCadastroComponent,
		UsuarioSelecionarComponent,
		UsuarioMinhacontaComponent,
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		ReactiveFormsModule,
		HttpClientModule,
		BrowserAnimationsModule,
		ToastrModule.forRoot(),
	],
	providers: [],
	bootstrap: [AppComponent],
})
export class AppModule {}
