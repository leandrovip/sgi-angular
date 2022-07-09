import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Return } from 'src/app/interfaces/return.interface';
import { AccessToken } from 'src/app/models/accessToken.model';
import { UsuarioService } from 'src/app/services/usuario.service';
import { FormHelper } from 'src/app/utils/form.util';
import { Security } from 'src/app/utils/security.util';

@Component({
    selector: 'app-login',
    templateUrl: './usuario-login.component.html',
})
export class UsuarioLoginComponent implements OnInit {
    public form: FormGroup;
    public formHelper: FormHelper;
    public ocupado: boolean = false;

    constructor(
        private service: UsuarioService,
        private fb: FormBuilder,
        private router: Router,
        private toastr: ToastrService
    ) {
        this.form = this.fb.group({
            email: ['', Validators.compose([Validators.required, FormHelper.email])],
            senha: ['', Validators.compose([Validators.required])],
        });
        this.formHelper = new FormHelper(this.form);
    }

    ngOnInit(): void {
        const data = Security.get();
        if (data) {
            this.setUser(data);
        }
    }

    submit() {
        this.ocupado = true;
        this.service.autenticar(this.form.value).subscribe({
            next: (result) => {
                if (result && result.success) {
                    this.setUser(result.data);
                    this.toastr.success(`Olá, ${result.data.nome}`, 'Bem-vindo!');
                }
                this.ocupado = false;
            },
            error: (err) => {
                this.toastr.error((err.error as Return).message, 'Que pena!');
                this.form.get('senha')?.reset();
                this.ocupado = false;
            },
        });
    }

    setUser(accessToken: AccessToken) {
        Security.set(accessToken);
        this.router.navigate(['/']);
    }
}
