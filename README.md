# NOVA-teste
Sistema de armazenagem de dados feito com ASP.NET, SQL Server e EntityFramework

Resumo do projeto:
	- Armazenar arquivos (Sendo salvo no banco de dados e no "C:\arquivos")
	- Editar os arquivos 
	- Deletar arquivos
	- Fazer o Download 

-- Para um teste rápido basta criar o banco com esta Query:

	CREATE DATABASE [testeNOVA]
	GO
	USE [testeNOVA]
	GO

	CREATE TABLE [Arquivo] (
        [Id] INT NOT NULL IDENTITY(1, 1),
    	[Nome] VARCHAR(200) NULL,
    	[Descricao] NVARCHAR(150) NOT NULL,
    	[Status] VARCHAR(200) NOT NULL,
    	[Dados] VARBINARY(max) NULL,
     	CONSTRAINT [PK_Imagens] PRIMARY KEY CLUSTERED
     	(
         [Id] ASC
     	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
     	ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

Com o banco criado basta fazer a conexão dentro da aplicação:
	DataContext.cs:
	- protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer("Server=localhost,1433;Database=testeNOVA;User ID=sa;Password=269545");  *( Adicionar a senha do seu banco )*

Ferramentas e dependências utilizadas no projeto:
	-Docker / Azure Data Studio
	-Entity FrameworkCore
	-Entity FrameworkCore.SqlServer
	-Entity FrameworkCore.Design
	-SQL Server

Após a criação da aplicação foi adicionada as pastas e arquivos:
	- Data / DataContext.cs ( Conexão com o banco de dados );
	- Models / Arquivo.cs ( Modelo da nossa aplicação );
	- Mappings / ArquivoMap.cs ( Arquivo de mapeamento );

Feito isso, já podemos fazer as migrações e gerar o banco de dados de forma automática graças ao mapeamento.
Para isso vamos no terminal e vamos agora criar as migrações utilizando o entityframework:
	- dotnet ef migrations add PrimeiraMigracao

Terminado a execução desse comando as migrações já vão estar prontas dentro da pasta Migrations
Antes de executar as migrações vamos criar um novo banco chamado "testeNOVA" utilizando o Azure Data Studio
Agora iremos aplicar as migrações no banco com:
	- dotnet ef database update

Com isso já vamos poder testar a aplicação...	



Obs: Única coisa que ficou incompleta foi a parte da edição dos arquivos. Não deu tempo de fazer o arquivo persistir durante a edição.
Obs2: Testar o armazenamento de arquivos com .docx, por alguma razão os outros não estão sendo salvos corretamente no C: porém no banco salva normal.

