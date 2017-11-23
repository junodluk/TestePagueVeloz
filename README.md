# TestePagueVeloz

Projeto criado como um teste para vaga na PagueVeloz.

Criado por **Alcione de Lucca Júnior** <junior.dluk@gmail.com>

## Setup
1. `git clone https://github.com/junodluk/TestePagueVeloz.git`
2. Execute o comando `npm install`
3. Execute o comando `dotnet restore`

## Como Executar
1. Substitua as propriedades da variável `ConnectionString.Deafult` localizada no arquivo `appsettings.json` (ou `appsettings.Development.json` se a sua máquina estiver setada como ambiente de Desenvolvimento) com os dados do seu banco de dados Sql Server.
2. Execute o comando `dotnet watch run`
3. Navegar para o endereço padrão `http://localhost:5000/`