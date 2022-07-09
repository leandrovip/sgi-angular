import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UsuarioLoginComponent } from './pages/usuario/usuario-login/usuario-login.component';
import { MainComponent } from './components/layout/main/main.component';
import { UsuarioCadastroComponent } from './pages/usuario/usuario-cadastro/usuario-cadastro.component';
import { UsuarioSelecionarComponent } from './pages/usuario/usuario-selecionar/usuario-selecionar.component';
import { AuthService } from './services/auth.service';

const routes: Routes = [
    {
        path: '',
        canActivate: [AuthService],
        component: MainComponent,
        children: [{ path: '', component: DashboardComponent }],
    },
    {
        path: 'usuarios',
        canActivate: [AuthService],
        component: MainComponent,
        children: [
            { path: '', component: UsuarioSelecionarComponent },
            { path: 'incluir', component: UsuarioCadastroComponent },
        ],
    },
    { path: 'login', component: UsuarioLoginComponent },
    { path: '**', component: UsuarioLoginComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
