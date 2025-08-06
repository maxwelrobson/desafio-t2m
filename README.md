# 🚀 Gerenciador de Tarefas - API REST

Este projeto consiste em uma API REST robusta para gerenciamento de tarefas, desenvolvida como parte de um desafio de processo seletivo. A solução foi construída com .NET 9, seguindo os princípios da arquitetura em camadas do Domain-Driven Design (DDD) para garantir um código limpo, testável e de fácil manutenção.

## ✨ Funcionalidades

- **Arquitetura DDD:** Separação clara das responsabilidades em camadas de Domínio, Aplicação, Infraestrutura e Apresentação (API).
- **CRUD Completo de Tarefas:** Endpoints para Criar, Ler, Atualizar e Deletar tarefas.
  - `[POST] /api/tarefas`: Cria uma nova tarefa.
  - `[GET] /api/tarefas`: Lista todas as tarefas existentes.
  - `[GET] /api/tarefas/{id}`: Busca uma tarefa específica pelo seu ID.
  - `[PUT] /api/tarefas/{id}`: Atualiza uma tarefa existente.
  - `[DELETE] /api/tarefas/{id}`: Remove uma tarefa.
- **Testes Abrangentes:**
  - **Testes Unitários:** Utilizando `xUnit` e `Moq` para validar a lógica de negócio na camada de Aplicação de forma isolada.
  - **Testes de Integração:** Utilizando `xUnit` e `WebApplicationFactory` para testar todo o fluxo da API, desde a requisição HTTP até a interação com um banco de dados de teste, garantindo a integração entre as camadas.
- **Acesso a Dados Otimizado:** Uso do Micro ORM **Dapper** para comunicação performática com o banco de dados PostgreSQL.
- **Documentação de API:** Geração automática de uma interface interativa com **Swagger (OpenAPI)**.

## 🛠️ Tecnologias Utilizadas

- **Backend:** .NET 9, ASP.NET Core, C#
- **Banco de Dados:** PostgreSQL
- **Acesso a Dados:** Dapper
- **Testes:** xUnit, Moq, WebApplicationFactory

## ⚙️ Como Executar o Projeto

### Pré-requisitos
- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### Passos para Execução
1.  Clone este repositório para a sua máquina local.
2.  No seu ambiente PostgreSQL, crie dois bancos de dados: `TarefasDb` e `TarefasDb_Tests`.
3.  Execute o script SQL localizado em `/database/init.sql` em ambos os bancos de dados para criar a tabela `Tarefas`.
4.  **Configurar Segredos do Projeto:**
    * No Visual Studio, clique com o botão direito no projeto `GerenciadorDeTarefas.Api` e selecione **"Manage User Secrets"**.
    * No arquivo `secrets.json` que abrir, cole o bloco abaixo e preencha com suas credenciais do PostgreSQL. Este arquivo conterá as senhas para o banco de dados principal e o de testes:
        ```json
        {
          "ConnectionStrings": {
            "DefaultConnection": "Server=localhost;Port=5432;Database=TarefasDb;User Id=SEU_USUARIO;Password=SUA_SENHA;",
            "TestConnection": "Server=localhost;Port=5432;Database=TarefasDb_Tests;User Id=SEU_USUARIO;Password=SUA_SENHA;"
          }
        }
        ```
5.  Abra a solução (`.sln`) no Visual Studio 2022.
6.  Pressione **F5** para iniciar a API.
7.  Acesse a documentação interativa do Swagger no endereço que abrir no seu navegador (ex: `https://localhost:7123/swagger`).

## 🧪 Como Rodar os Testes

1.  Abra a solução no Visual Studio.
2.  Vá até o menu superior e selecione **Test > Test Explorer**.
3.  Com o Test Explorer aberto, clique no botão **"Run All Tests"** (ícone de play duplo) para executar todos os testes unitários e de integração. Os resultados aparecerão na janela.

## 👤 Autor

**Maxwel Robson**
- [LinkedIn] https://www.linkedin.com/in/maxwel-robson/
- [GitHub] https://github.com/maxwelrobson