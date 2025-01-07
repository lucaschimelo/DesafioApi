# DesafioApi
Api para gerenciamento de livros

# Pré-Requisitos
Git
.Net Core 3.1
Visual Studio 2022
Sql Server 2008(ou superior)
Sql Server Management Studio

# Bibliotecas utilizadas
FluentValidation
Swagger
Entity Framework
Dapper
ITextSharp

# Obter projeto
Com o terminal aberto no diretório que deseja armazenar o projeto, execute o comando : git clone https://github.com/lucaschimelo/DesafioApi.git

# Criação da base de dados
Após se conectar a instância do Sql Server utilizando o Sql Server Management, executar os scripts enumerados na sequência que estão na pasta Desafio.Infra.Data\Scripts

# Configuração
Configurar a string de conexão com a base de dados no arquivo DesafioApi.Application\appsettings.json

# Execução do projeto
Clicar no botão Start Debugging, ou apertar F5

# Acesso a aplicação
No browser de sua preferência, acesse a url : https://localhost:5001/swagger/index.html
