import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/components/shared/shared.module';

import { UsuarioFormComponent } from './usuario-form/usuario-form.component';
import { UsuarioLoginComponent } from './usuario-login/usuario-login.component';
import { UsuarioSelecionarComponent } from './usuario-selecionar/usuario-selecionar.component';
import { UsuarioRoutingModule } from './usuario.rounting.module';

@NgModule({
	declarations: [UsuarioSelecionarComponent, UsuarioFormComponent, UsuarioLoginComponent],
	imports: [CommonModule, UsuarioRoutingModule, ReactiveFormsModule, SharedModule],
})
export class UsuarioModule {}
