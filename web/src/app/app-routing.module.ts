import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UsuarioLoginComponent } from './pages/usuario/usuario-login/usuario-login.component';
import { MainComponent } from './components/layout/main/main.component';
import { UsuarioCadastroComponent } from './pages/usuario/usuario-cadastro/usuario-cadastro.component';
import { UsuarioSelecionarComponent } from './pages/usuario/usuario-selecionar/usuario-selecionar.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'dashboard' },
	{
		path: 'dashboard',
		component: MainComponent,
		canActivate: [AuthGuard],
		children: [{ path: '', component: DashboardComponent }],
	},
	{
		path: 'usuarios',
		canActivate: [AuthGuard],
		component: MainComponent,
		children: [
			{ path: '', component: UsuarioSelecionarComponent },
			{ path: 'incluir', component: UsuarioCadastroComponent },
		],
	},
	{ path: 'login', component: UsuarioLoginComponent },
	{ path: '**', component: UsuarioLoginComponent }, // criar 404
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
