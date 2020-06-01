## Sobre a API
Esta web API implementa as operações CRUD usando Asp.NET Core. SQL Server é o banco de dados usado. Operações no banco são feitas com Dapper. A autenticação é feita por JWT. A API oferece uma interface Swagger. A aplicação foi construída usando Visual Studio.

#### Execução
O script sql `CidadesAuth.sql` deve ser executado no SQL Server para criação do banco de dados, das tabelas cidades e usuarios, inserção de dados e criação da procedure esperada. A string de conexão ao banco de dados criado deve ser passada para `appsettings.json` em `[ConnectionStrings: DefaultConnection]` para que a aplicação possa acessar o banco.
Usando a execução padrão do Visual Studio (IIS Express) é possível acessar a interface Swagger em localhost:44349/swagger/index.html .

#### Autenticação
É possível obter o token de autenticação pela operação de POST em /api/token usando **usuário e senha "admin"** (o campo id_Usuario é ignorado). Este token deve ser inserido na janela que aparece ao clicar no botão authorize **precedido de "Bearer" e um espaço**. Ex: 

    Bearer asdfghjkl...

#### Operações disponíveis
- GET (/api/cidades): retorna todas as cidades cadastradas no banco de dados.
- GET (/api/cidades/\<codigo\>): retorna a cidade que foi cadastrada com a chave primária \<codigo\>.
- GET (/api/cidades/cidades-por-uf): retorna a lista de estados para os quais há cidades cadastradas no banco de dados seguidos do número de cidades cadastradas para cada um.
- POST(/api/cidades): insere a cidade constante no corpo da requisição. O código fornecido é ignorado pois a chave primária é gerada automaticamente pelo banco de dados.
- POST(/api/token): retorna um token de autenticação JWT para usuário e senha constantes no corpo da requisição.
- PUT (/api/cidades/\<codigo\>): Atualiza a entrada que contém a chave primaria \<codigo\>.
- DELETE (/api/cidades/\<codigo\>): Atualiza a entrada que contém a chave primaria \<codigo\>.