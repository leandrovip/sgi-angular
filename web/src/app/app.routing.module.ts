import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainComponent } from './components/layout/main/main.component';
import { AuthGuard } from './guards/auth.guard';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UsuarioLoginComponent } from './pages/usuario/usuario-login/usuario-login.component';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'dashboard' },
	{
		path: 'dashboard',
		component: MainComponent,
		canActivate: [AuthGuard],
		children: [{ path: '', component: DashboardComponent }],
	},
	{ path: 'login', component: UsuarioLoginComponent },
	{ path: '**', component: UsuarioLoginComponent }, // criar 404
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
