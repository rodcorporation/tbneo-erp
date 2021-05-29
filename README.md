# tbneo-erp
Gestão de projetos e suas features flag.

# Run Project

Para rodar é precisar passar pelas seguintes etapas:

1. Corrigir a ConnectionString em appsettings.json na API apontando para seu Sql Server de preferência.
2. Executar o projeto API em asp.net core.
3. Verificar a URL da API e alterar na SPA. Atributo urlBase no arquivo src\apis\requestServiceBase.js
4. Agora dê os comandos yarn install e yarn serve.

# Stack

* Net Core 5.0
* Node v14.17.x
* Vue 2
* Sql Server
* Vuetify
* Vue-Router
