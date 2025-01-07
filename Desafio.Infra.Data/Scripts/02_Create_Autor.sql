
USE [Desafio]
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Autor' and xtype='U')
	BEGIN
		CREATE TABLE Autor 
		(
			CodAu INT IDENTITY (1,1) NOT NULL CONSTRAINT PK_Autor PRIMARY KEY,
			Nome VARCHAR(40) NOT NULL			
		)
	END