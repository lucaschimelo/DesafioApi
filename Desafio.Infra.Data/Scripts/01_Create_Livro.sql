
USE [Desafio]
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Livro' and xtype='U')
	BEGIN
		CREATE TABLE Livro 
		(
			Codl INT IDENTITY (1,1) NOT NULL CONSTRAINT PK_Livro PRIMARY KEY,
			Titulo VARCHAR(40) NOT NULL,
			Editora VARCHAR(40) NOT NULL,
			Edicao INTEGER NOT NULL,
			AnoPublicacao VARCHAR(4) NOT NULL
		)
	END