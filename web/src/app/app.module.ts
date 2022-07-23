import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app.routing.module';
import { AppComponent } from './app.component';
import { LayoutModule } from './components/layout/layout.module';
import { SharedModule } from './components/shared/shared.module';
import { JwtInterceptor } from './middlewares/jwt-interceptor';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UsuarioModule } from './pages/usuario/usuario.module';

@NgModule({
	declarations: [AppComponent, DashboardComponent],
	imports: [
		BrowserModule,
		HttpClientModule,
		BrowserAnimationsModule,
		ToastrModule.forRoot(),
		LayoutModule,
		SharedModule,
		UsuarioModule,
		AppRoutingModule,
	],
	providers: [{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }],
	bootstrap: [AppComponent],
})
export class AppModule {}
