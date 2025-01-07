
USE [Desafio]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Assunto' and xtype='U')
	BEGIN
		CREATE TABLE Assunto 
		(
			CodAs INT IDENTITY (1,1) NOT NULL CONSTRAINT PK_Assunto PRIMARY KEY,
			Descricao VARCHAR(20) NOT NULL			
		)
	END	