USE [Desafio]
GO

IF NOT EXISTS(SELECT CodFor FROM FormaCompra WHERE Descricao = 'Balc�o')
	BEGIN
		INSERT INTO FormaCompra(Descricao)VALUES('Balc�o');
	END;
	
IF NOT EXISTS(SELECT CodFor FROM FormaCompra WHERE Descricao = 'Self-Service')
	BEGIN
		INSERT INTO FormaCompra(Descricao)VALUES('Self-Service');
	END;
	
IF NOT EXISTS(SELECT CodFor FROM FormaCompra WHERE Descricao = 'Internet')
	BEGIN
		INSERT INTO FormaCompra(Descricao)VALUES('Internet');
	END;
	
IF NOT EXISTS(SELECT CodFor FROM FormaCompra WHERE Descricao = 'Evento')
	BEGIN
		INSERT INTO FormaCompra(Descricao)VALUES('Evento');
	END;