import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainComponent } from '../../components/layout/main/main.component';
import { UsuarioFuncao } from '../../core/enums/usuarioFuncao.enum';
import { AuthGuard } from '../../guards/auth.guard';
import { UsuarioFormComponent } from './usuario-form/usuario-form.component';
import { UsuarioSelecionarComponent } from './usuario-selecionar/usuario-selecionar.component';

const usuarioRoutes: Routes = [
	{
		path: 'usuarios',
		canActivate: [AuthGuard],
		component: MainComponent,
		data: { role: [UsuarioFuncao.Administrador] },
		children: [
			{ path: '', component: UsuarioSelecionarComponent },
			{ path: 'incluir', component: UsuarioFormComponent },
			{ path: 'editar/:id', component: UsuarioFormComponent },
			{ path: 'excluir/:id', component: UsuarioFormComponent },
		],
	},
	{
		path: 'perfil',
		component: MainComponent,
		canActivate: [AuthGuard],
		children: [{ path: '', component: UsuarioFormComponent }],
	},
];

@NgModule({
	imports: [RouterModule.forChild(usuarioRoutes)],
	exports: [RouterModule],
})
export class UsuarioRoutingModule {}
