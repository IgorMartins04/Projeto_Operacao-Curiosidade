# Projeto Forlogic

Este projeto é uma aplicação full-stack desenvolvida para a Forlogic, composta por um frontend em Angular e um backend em .NET Core. O objetivo é fornecer uma plataforma robusta e escalável para gerenciamento de dados e interações de usuário.




## Contexto do Projeto

Este projeto foi desenvolvido como parte de uma turma de formação da empresa Forlogic, visando capacitar novos talentos e criar uma solução prática que demonstre as habilidades adquiridas e as tecnologias modernas utilizadas no desenvolvimento de software.




## Frontend (Angular)

O frontend da aplicação é construído utilizando o framework Angular, garantindo uma interface de usuário dinâmica e de alta performance. A estrutura do projeto segue as melhores práticas do Angular, com componentes bem definidos, módulos organizados e serviços para gerenciamento de estado e comunicação com a API.

### Tecnologias Utilizadas

- **Angular v17.x**: Framework principal para construção da interface de usuário.
- **Angular Material**: Biblioteca de componentes UI para uma experiência de usuário consistente e moderna.
- **TypeScript**: Linguagem de programação que adiciona tipagem estática ao JavaScript, melhorando a manutenibilidade e escalabilidade do código.
- **RxJS**: Biblioteca para programação reativa, utilizada para lidar com eventos assíncronos e gerenciamento de estado.
- **jwt-decode**: Para decodificação de tokens JWT (JSON Web Token) no lado do cliente.
- **SCSS**: Pré-processador CSS para estilos mais organizados e reutilizáveis.

### Estrutura de Pastas (client/client/src/app)

- `app-routing.module.ts`: Define as rotas de navegação da aplicação.
- `app.component.*`: Componente raiz da aplicação.
- `components/`: Contém componentes reutilizáveis como `aside`, `card`, `form`, `header`, `popup-details`, `session-page`, `shared` e `table-main`.
- `pages/`: Contém os módulos e componentes específicos de cada página, como `auth` (login, criação de usuário), `dash-board`, `history`, `registers` e `report`.
- `services/`: Contém os serviços para lógica de negócio e comunicação com a API, como `auth.services.ts`, `log.service.ts`, `register.service.ts` e `user.service.ts`.
- `assets/`: Para arquivos estáticos como imagens.
- `enviroment/`: Configurações de ambiente (e.g., URLs da API).

### Como Rodar o Frontend

1.  **Navegue até o diretório do frontend:**
    ```bash
    cd client/client
    ```
2.  **Instale as dependências:**
    ```bash
    npm install
    ```
3.  **Inicie o servidor de desenvolvimento:**
    ```bash
    ng serve
    ```
    O aplicativo estará disponível em `http://localhost:4200/`.

4.  **Construa o projeto para produção:**
    ```bash
    ng build
    ```
    Os arquivos de produção serão gerados na pasta `dist/`.




## Metodologia "Operação Curiosidade"

Este projeto foi desenvolvido seguindo a metodologia "Operação Curiosidade", uma abordagem que incentiva a exploração, a experimentação e a busca ativa por conhecimento. Essa metodologia visa estimular a criatividade, o pensamento crítico e a resolução de problemas de forma inovadora, características essenciais para o desenvolvimento de soluções eficazes e de alta qualidade. A "Operação Curiosidade" promove um ambiente de aprendizado contínuo e colaborativo, onde os participantes são encorajados a questionar, investigar e propor novas ideias.




## Backend (API .NET Core)

O backend é uma API RESTful desenvolvida em .NET Core, responsável por gerenciar a lógica de negócio, a persistência de dados e a autenticação de usuários. A arquitetura da API é modular, utilizando princípios de Clean Architecture e injeção de dependência para garantir escalabilidade, manutenibilidade e testabilidade.

### Tecnologias Utilizadas

- **.NET 8.0**: Framework para construção de aplicações web e APIs.
- **ASP.NET Core**: Para a criação da API RESTful.
- **Entity Framework Core**: ORM (Object-Relational Mapper) para interação com o banco de dados SQL Server.
- **SQL Server**: Sistema de gerenciamento de banco de dados relacional.
- **JWT (JSON Web Tokens)**: Para autenticação e autorização de usuários.
- **BCrypt.Net-Next**: Para hashing seguro de senhas.
- **Swashbuckle.AspNetCore**: Para geração de documentação Swagger/OpenAPI da API.

### Estrutura de Pastas (Api/Api)

- `Controllers/`: Contém os controladores da API, como `AuthController`, `LogController`, `RegisterController` e `UserController`.
- `DataBase/`: Contém o contexto do banco de dados (`DbContext.cs`).
- `Dtos/`: Contém os Data Transfer Objects (DTOs) para entrada e saída de dados.
- `Helpers/`: Classes auxiliares para funcionalidades como hashing de senhas (`PasswordHelper.cs`) e validações.
- `Interfaces/`: Define as interfaces para os repositórios e serviços, promovendo a injeção de dependência.
- `Middlewares/`: Contém middlewares personalizados, como `GeneralExceptionHandler.cs` para tratamento global de exceções.
- `Models/`: Define os modelos de dados (entidades) da aplicação, como `Log.cs`, `Register.cs` e `User.cs`.
- `Repository/`: Implementações dos repositórios para acesso aos dados.
- `Services/`: Implementações dos serviços que contêm a lógica de negócio.
- `Validations/`: Classes para validação de modelos.

### Como Rodar o Backend

1.  **Navegue até o diretório do backend:**
    ```bash
    cd Api/Api
    ```
2.  **Restaure as dependências:**
    ```bash
    dotnet restore
    ```
3.  **Construa o projeto:**
    ```bash
    dotnet build
    ```
4.  **Execute a aplicação:**
    ```bash
    dotnet run
    ```
    A API estará disponível em `https://localhost:7001` (ou outra porta configurada em `launchSettings.json`).




## Configuração do Banco de Dados

O projeto utiliza SQL Server para persistência de dados. As migrações do Entity Framework Core são usadas para gerenciar o esquema do banco de dados.

### Passos para Configuração

1.  **Certifique-se de ter o SQL Server instalado e configurado.**
2.  **Atualize a string de conexão** no arquivo `appsettings.json` (e `appsettings.Development.json`) no projeto da API para apontar para sua instância do SQL Server.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=seu_servidor;Database=nome_do_banco;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;"
    }
    ```
3.  **Aplique as migrações do Entity Framework Core:**
    Navegue até o diretório da API (`Api/Api`) e execute os seguintes comandos:
    ```bash
    dotnet ef database update
    ```
    Isso criará o banco de dados e as tabelas necessárias, ou aplicará quaisquer migrações pendentes.




## Estrutura Geral do Projeto

O projeto é organizado em dois diretórios principais:

-   `client/`: Contém todo o código-fonte do frontend Angular.
-   `Api/`: Contém todo o código-fonte da API backend em .NET Core.

Esta separação permite o desenvolvimento e a implantação independentes de cada parte da aplicação, seguindo uma arquitetura de microserviços ou de aplicações distribuídas.




## Contribuição

Contribuições são bem-vindas! Se você deseja contribuir para este projeto, por favor, siga estas diretrizes:

1.  Faça um fork do repositório.
2.  Crie uma nova branch para sua feature (`git checkout -b feature/minha-feature`).
3.  Faça suas alterações e teste-as.
4.  Faça commit de suas alterações (`git commit -m 'feat: Adiciona nova funcionalidade'`).
5.  Envie para a branch original (`git push origin feature/minha-feature`).
6.  Abra um Pull Request detalhando suas mudanças.





