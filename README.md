# 📝 TaskMate

O **TaskMate** é um sistema de gerenciamento de tarefas desenvolvido em
**C# com ASP.NET Core**, utilizando **Entity Framework Core** e **SQL
Server**.\
O objetivo é fornecer uma API organizada, validada e pronta para
evolução, acompanhada de perto pelo **Elvis**, que contribui com
feedbacks e acompanhamento do desenvolvimento.

------------------------------------------------------------------------

## 🚀 Tecnologias utilizadas

-   C#\
-   ASP.NET Core Web API\
-   Entity Framework Core\
-   SQL Server\
-   Swagger\
-   Postman (para testes)

------------------------------------------------------------------------

## 📌 Status do Projeto

✅ Estrutura inicial criada\
✅ Banco configurado e migrations aplicadas\
✅ Controller de Tarefas (TasksController) implementado\
✅ DTOs (Create, Update, Read) criados\
✅ Validações adicionadas com Data Annotations\
✅ CRUD completo testado no Postman (GET, POST, PUT, DELETE)

------------------------------------------------------------------------

## 🛠️ Como rodar o projeto

### 1. Clonar o repositório

``` bash
git clone https://github.com/seu-usuario/taskmate.git
```

### 2. Acessar a pasta do projeto

``` bash
cd taskmate/scr/TaskMate.API
```

### 3. Configurar o banco de dados

Verifique se o **SQL Server** está rodando e ajuste a connection string
no arquivo `appsettings.json` caso necessário:

``` json
"ConnectionStrings": {
  "DataBase": "Server=.;Database=DB_SistemaTarefas;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Rodar migrations (se necessário)

``` bash
dotnet ef database update
```

### 5. Executar a aplicação

``` bash
dotnet run
```

A API estará disponível em:\
👉 `http://localhost:5114/swagger`

------------------------------------------------------------------------

## 📡 Endpoints principais

### GET todas as tarefas

    GET /api/tasks

### GET tarefa por ID

    GET /api/tasks/{id}

### POST criar tarefa

``` json
{
  "title": "Estudar C#",
  "description": "Revisar controllers e APIs"
}
```

### PUT atualizar tarefa

``` json
{
  "title": "Estudar C# - atualizado",
  "description": "Revisar controllers, APIs e DTOs",
  "isCompleted": true
}
```

### DELETE remover tarefa

    DELETE /api/tasks/{id}

------------------------------------------------------------------------

## 🚧 Próximos passos

-   Refatorar com **AutoMapper** para simplificar conversões entre
    entidades e DTOs\
-   Criar camada de **Services/Repository**\
-   Melhorar tratamento de erros\
-   Documentar melhor no Swagger (com exemplos)\
-   Implementar autenticação (opcional)

------------------------------------------------------------------------

📌 Projeto em constante evolução, feito com dedicação e aprendizado
contínuo! 🚀
