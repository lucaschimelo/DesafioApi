
USE [Desafio]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Livro_FormaCompra' and xtype='U')
	BEGIN
		CREATE TABLE Livro_FormaCompra 
		(
			Livro_Codl INTEGER NOT NULL CONSTRAINT FK_LivroFormaCompra_Livro FOREIGN KEY REFERENCES Livro(Codl),
			FormaCompra_CodFor INTEGER NOT NULL CONSTRAINT FK_LivroFormaCompra_FormaCompra FOREIGN KEY REFERENCES FormaCompra(CodFor),
			Valor DECIMAL(18,2) NOT NULL,
			CONSTRAINT UK_LivroCompraForma UNIQUE (Livro_Codl, FormaCompra_CodFor)
		)
	END