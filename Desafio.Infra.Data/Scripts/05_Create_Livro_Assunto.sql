
USE [Desafio]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Livro_Assunto' and xtype='U')
	BEGIN
		CREATE TABLE Livro_Assunto 
		(
			Livro_Codl INTEGER NOT NULL CONSTRAINT FK_LivroAssunto_Livro FOREIGN KEY REFERENCES Livro(Codl),
			Assunto_CodAs INTEGER NOT NULL CONSTRAINT FK_LivroAssunto_Assunto FOREIGN KEY REFERENCES Assunto(CodAs)
		)
	END