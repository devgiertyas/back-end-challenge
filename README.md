# API feita em C# para criação de Todos/Tasks
- API com autenticação JWT para armazenar tasks e seus respectivos usuários, essa aplicação possui a documentação feita com Swagger. 

# Requisitos básicos para rodar o Projeto
- SDK .Net 6.0
- Banco de Dados PostgreSQL na versão mais atualizada 
- Postman para importação do Workspace com as requisições para a API.  
- Visual Studio

# Como Rodar o Projeto
### Siga as seguintes instruções
                
+ Clone o repositório;
+ Com os arquivos do repositório em uma pasta abra o arquivo TodoAPI.sln com o Visual Studio;
+ Com a solução aberta no Visual Studio é necessário Configurar a conexão com o Banco de Dados Postgres
    * Abra o arquivo Program.cs e no seguinte trecho de código coloque a sua string de conexão com o banco de dados
     ![Banco-de-dados](https://i.ibb.co/PCqKhzF/baixados.png)
    * Depois de configurar a conexão com o banco de dados é necessário rodar as migrations no banco de dados. Para isso abra o console do Gerenciador de Pacotes do Visual Studio e rode o comando Update-database.
    ![Comando-Update-database](https://i.ibb.co/mTTSNBF/console.png)
    * Após realizar essas alterações você deve verificar no PostgresSQL se o banco de dados foi criado com as respectivas tabelas.


# Como testar
+ Após realizar os passos para rodar o projeto, você precisa iniciar a API, você tem dois modos o primeiro é iniciar pelo modo debug do Visual Studio ou abrir uma janela do Powershell na pasta da solução e executar os comandos dotnet build e dotnet run. 
![](https://i.ibb.co/dcCjPTp/run-api.png)
+ Após o projeto irá iniciar e você pode verificar a URL da Api da documentação no Swagger que geralmente para API é http://localhost:5003 e pra documentação https://localhost:7003/swagger/index.html. Nessa etapa você pode ter alguns erros de SSL, devido ao projeto estar configurado para usar SSL, nesse caso se você tiver problemas, você deve permitir certificados inválidos de origem local no seu navegador
+ Você pode importar o arquivo Todo API.postman_collection.json no postam com as requisições já prontas.
+ As requisições devem ser todas autenticadas então o primeiro endpoint que você tem que consumir é o do login. O usuário padrão é: email: admin@admin.com senha: 123. Com a requisições do login feita, é necessário copiar o token que a aplicação irá retonar e em todas as outros requisições é necessário enviar esse token no Header na propriedade Authorization com o tipo do token Bearer Token.


