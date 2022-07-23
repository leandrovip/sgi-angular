import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { UsuarioLoginComponent } from './pages/usuario/usuario-login/usuario-login.component';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'dashboard' },
	{ path: 'login', component: UsuarioLoginComponent },
	{ path: '**', component: UsuarioLoginComponent }, // criar pagina 404
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
