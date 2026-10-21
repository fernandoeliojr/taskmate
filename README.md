# TaskMate — Gerenciador de Tarefas

API para gerenciamento de tarefas desenvolvida como projeto de estudo e portfólio, com foco em desenvolvimento backend utilizando C#, ASP.NET Core e boas práticas de organização de código.

> **Status:** projeto em desenvolvimento. Antes de executar os comandos abaixo, confirme que os projetos de código-fonte estão presentes no repositório e que os caminhos correspondem à versão atual.

## 📌 Sobre o projeto

O **TaskMate** tem como objetivo permitir o gerenciamento de tarefas por meio de uma API REST. O projeto foi criado para praticar conceitos de desenvolvimento de software, persistência de dados, validação de informações, autenticação e organização da aplicação em camadas.

## ✨ Funcionalidades

- Operações CRUD para tarefas: criar, consultar, atualizar e excluir.
- Validação dos dados de entrada com Data Annotations e DTOs.
- Persistência de dados com Entity Framework Core e SQL Server.
- Documentação e testes de endpoints com Swagger e Postman.
- Autenticação de usuários com JWT em desenvolvimento.

## 🧰 Tecnologias utilizadas

| Tecnologia | Finalidade |
|---|---|
| C# | Linguagem de programação |
| ASP.NET Core Web API | Construção da API REST |
| Entity Framework Core | Mapeamento objeto-relacional e migrations |
| SQL Server | Banco de dados relacional |
| JWT | Autenticação baseada em tokens |
| Swagger / OpenAPI | Exploração e documentação dos endpoints |
| Postman | Testes manuais da API |
| Git e GitHub | Versionamento e hospedagem do código |

## 🏗️ Organização da solução

A solução foi organizada em projetos com responsabilidades separadas:

```text
TaskMate/
├── TaskMate.sln
└── scr/
    ├── TaskMate.Api/             # Endpoints e configuração da API
    ├── TaskMate.Application/     # Casos de uso e regras da aplicação
    ├── TaskMate.Domain/          # Entidades e regras de domínio
    └── TaskMate.Infrastructure/  # Banco de dados e persistência
```

### Estrutura de pastas

![Estrutura de pastas da solução TaskMate](estrutura%20de%20pastas.jpg)

## ⚙️ Como executar localmente

### Pré-requisitos

- Windows, Linux ou macOS.
- [.NET SDK](https://dotnet.microsoft.com/download) compatível com a versão definida nos arquivos `.csproj`.
- [SQL Server](https://www.microsoft.com/sql-server) em execução.
- Visual Studio, Visual Studio Code ou outro editor de sua preferência.
- (Opcional) Postman para testar os endpoints.

### 1. Clonar o repositório

Abra o terminal e execute:

```bash
git clone https://github.com/fernandoeliojr/taskmate.git
cd taskmate
```

### 2. Restaurar as dependências

Na pasta raiz do repositório:

```bash
dotnet restore TaskMate.sln
```

### 3. Configurar o banco de dados

Confira a connection string no arquivo de configuração da API, normalmente em `scr/TaskMate.Api/appsettings.json` ou em `appsettings.Development.json`.

Ajuste o servidor, o nome do banco e o método de autenticação de acordo com seu ambiente. Exemplo para SQL Server com autenticação do Windows:

```json
{
  "ConnectionStrings": {
    "DataBase": "Server=.;Database=DB_SistemaTarefas;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> Use o nome da connection string que estiver configurado no código da aplicação. Não publique senhas, tokens ou outras credenciais no repositório.

### 4. Aplicar as migrations

Se o projeto utilizar migrations do Entity Framework Core, instale a ferramenta caso ainda não esteja disponível:

```bash
dotnet tool install --global dotnet-ef
```

Se ela já estiver instalada, atualize-a quando necessário:

```bash
dotnet tool update --global dotnet-ef
```

Na raiz da solução, aplique as migrations usando os projetos de infraestrutura e API:

```bash
dotnet ef database update --project scr/TaskMate.Infrastructure --startup-project scr/TaskMate.Api
```

### 5. Executar a API

```bash
dotnet run --project scr/TaskMate.Api
```

O terminal informará o endereço local em que a aplicação está ouvindo. No ambiente de desenvolvimento usado durante o projeto, a API foi executada em:

- API: `http://localhost:5114`
- Swagger: `http://localhost:5114/swagger`

Se a porta mudar, utilize o endereço exibido no terminal.

### 6. Testar os endpoints

Abra o Swagger no navegador ou utilize o Postman. As operações principais do CRUD são:

| Método HTTP | Objetivo |
|---|---|
| `GET` | Consultar tarefas |
| `POST` | Criar uma tarefa |
| `PUT` | Atualizar uma tarefa |
| `DELETE` | Excluir uma tarefa |

Os caminhos exatos dos endpoints e os campos obrigatórios devem ser conferidos no controller e nos DTOs da versão atual do código.

## 🖼️ Capturas de tela

A imagem da estrutura de pastas já está incluída acima. Para completar a apresentação do projeto, adicione mais duas capturas reais:

1. **Swagger:** endpoints da API visíveis no navegador.
2. **Postman:** exemplo de requisição com resposta de sucesso (por exemplo, consulta ou criação de tarefa).

Salve os arquivos com estes caminhos e nomes:

```text
docs/
└── screenshots/
    ├── swagger.png
    └── postman-crud.png
```

Depois, adicione as imagens ao README usando:

```markdown
### Documentação da API

![Endpoints do TaskMate no Swagger](docs/screenshots/swagger.png)

### Teste da API

![Requisição do TaskMate no Postman](docs/screenshots/postman-crud.png)
```

## 🎯 Objetivo de aprendizado

Este projeto faz parte da minha evolução na área de desenvolvimento de sistemas e reúne práticas de construção de APIs, persistência de dados, validação, autenticação, testes e versionamento com Git.

## 👨‍💻 Autor

**Fernando Junior**

- GitHub: [@fernandoeliojr](https://github.com/fernandoeliojr)
- Projeto: [TaskMate](https://github.com/fernandoeliojr/taskmate)

---

*Projeto desenvolvido para fins de aprendizado e portfólio, com melhorias contínuas.*
