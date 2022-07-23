<div align="center">
  <img src="assets/logo.png" alt="logo" width="200" height="auto" />
  <h1>Vip.SGI - Sistema de Gerenciamento Interno</h1>
  <p> Em desenvolvimento </p>
</div>

<br />

## :star2: Sobre

Projeto desenvolvido com a intensão de utilizar internamente na empresa <a href="https://vipsolucoes.com/" target="_blank">VIP Soluções</a> e também como repositório de estudos sobre Angular.

### :space_invader: Stack Utilizada (por enquanto)

<ul>
  <li><a href="https://www.angular.io" target="_blank">Angular</a> - CLI 14.0.4</li>
  <li><a href="https://www.typescriptlang.org/" target="_blank">Typescript</a> - 4.7.4</li>
  <li><a href="https://www.nodejs.org/" target="_blank">NodeJS</a> - (npm) 16.15.1</li>
  <li><a href="https://dotnet.microsoft.com/" target="_blank">Net 6.0</a> - 6.0.302</li>
  <li><a href="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads target="_blank">MSSQL</a></li>
  <li><a href="https://getbootstrap.com/" target="_blank">Bootstrap 5</a></li>
</ul>

### :dart: Funcionalidades

- Login com autenticação via JWT
- Cadastro de Usuário
- _*Cadastro de Pessoas*_
- _*Parâmetros*_

### :key: Variáveis de Ambiente

- **Backend** - Configurar a connectionstring de acesso ao MSSQL

```powershell
.\api\src\Vip.SGI.Api\appsettings.Development.json
```

- **Frontend** - Caso seja necessário alterar a url da api, mude em:

```powershell
.\web\src\environments\environment.ts
```

## :toolbox: Como Iniciar

### :bangbang: Pré Requisitos

_Aqui assumo que você tenha o `dotnet`, `npm` e o `Angular CLI` já instalados nas suas respectivas versões acima informado_ 👌

Clonar o projeto:

```powershell
 git clone https://github.com/leandrovip/sgi-angular.git
 cd sgi-angular
```

### :gear: Instalação Backend (API)

Antes de iniciar vamos instalar a cli do EntityFrameworkCore, caso não tenha:

```powershell
dotnet tool update --global dotnet-ef
```

Compilar o projeto e criar o banco de dados:

```powershell
dotnet ef database update --project .\api\src\Vip.SGI.Infra\Vip.SGI.Infra.csproj --startup-project .\api\src\Vip.SGI.Api\Vip.SGI.Api.csproj
```

### :rocket: Instalação FrontEnt (Angular)

```powershell
cd sgi-angular\web
npm install
```

### :running: Executar o projeto

Iniciando a **api** em => https://localhost:7187

```powershell
cd sgi-angular
dotnet run --project .\api\src\Vip.SGI.Api\Vip.SGI.Api.csproj
```

Iniciando o **front** em => http://localhost:4200

```powershell
  cd sgi-angular\web
  ng serve
```

**DADOS INICIAIS**
Usuário: admin@admin.com
Senha: 123

## :handshake: Contatos

Leandro Ferreira - [@leandrovip29](https://twitter.com/leandrovip29) - leandro@vipsolucoes.com

## :gem: Agradecimentos

Qualquer sugestão ou crítica é muito bem-vinda! Obrigado ✅🙌
