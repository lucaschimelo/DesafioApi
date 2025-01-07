
USE [Desafio]
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FormaCompra' and xtype='U')
	BEGIN
		CREATE TABLE FormaCompra 
		(
			CodFor INT IDENTITY (1,1) NOT NULL CONSTRAINT PK_FormaCompra PRIMARY KEY,
			Descricao VARCHAR(40) NOT NULL
		)
	END