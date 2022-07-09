import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IReturn } from 'src/app/interfaces/return.interface';
import { Usuario } from 'src/app/models/usuario.model';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
    selector: 'app-usuario-selecionar',
    templateUrl: './usuario-selecionar.component.html',
})
export class UsuarioSelecionarComponent implements OnInit {
    public usuarios!: Observable<IReturn<Usuario[]>>;

    constructor(private service: UsuarioService) {}

    ngOnInit(): void {
        this.usuarios = this.service.obterLista();
    }
}
