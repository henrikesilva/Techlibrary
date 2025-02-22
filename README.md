# TechLibrary.API

## Descrição

Este projeto foi desenvolvido como parte do acompanhamento de aulas do evento NLW Connect 2025 da Rocketseat, ministrado por Wellison Arley e tem o objetivo de criar uma API para efetuar o gerenciamento de uma biblioteca de livros.

- Utilizando linguagem .Net 9, swagger para a documentação e Jwt para a autenticação.
- A aplicação permite:
  - Cadastrar um usuário
  - Efetuar login com usuário cadastrado
  - Reservar um livro que esteja disponível
  - Efetuar a busca paginada de livros

## Estrutura do Projeto

A estrutura do projeto segue o DDD (Domain-Driven Design) e é composta por seis projetos:

TechLibrary/ ├── TechLibrary.API ├── TechLibrary.Application ├── TechLibrary.Communication ├── TechLibrary.Domain ├── TechLibrary.Exception ├── TechLibrary.Infraestructure


## Documentação dos Endpoints

A documentação completa dos endpoints pode ser acessada através do Swagger disponível na aplicação.

## Instalação

Siga as etapas abaixo para configurar o ambiente e executar o projeto:

1. Clone o repositório:
   ```sh
   git clone https://github.com/seu-usuario/techlibrary-api.git

2. Navegue até o diretório:
  ```cd techlibrary-api```

3. Instale as dependências:
   ```dotnet restore```

4. Configure a string de conexão no arquivo ```appsettings.json```

5. Execute a aplicação ```.Net Run```

## Uso
Após iniciar a aplicação, você pode acessar a documentação do Swagger em ```http://localhost:5000/swagger``` para interagir com os endpoints.

## Contribuição
Contribuições são bem-vindas! 
Sinta-se à vontade para abrir issues e enviar pull requests.

## Licença
Espero que isso ajude! Se precisar de mais alguma coisa ou quiser fazer ajustes, estou por aqui.

