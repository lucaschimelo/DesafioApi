
USE [Desafio]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Livro_Autor' and xtype='U')
	BEGIN
		CREATE TABLE Livro_Autor 
		(
			Livro_Codl INTEGER NOT NULL CONSTRAINT FK_LivroAutor_Livro FOREIGN KEY REFERENCES Livro(Codl),
			Autor_CodAu INTEGER NOT NULL CONSTRAINT FK_LivroAutor_Autor FOREIGN KEY REFERENCES Autor(CodAu)
		)
	END