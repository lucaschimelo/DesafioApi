
USE [Desafio]
GO

IF EXISTS(SELECT 1 FROM sys.views WHERE Name = 'Vi_AutorLivro')
	BEGIN
		DROP VIEW Vi_AutorLivro;
	END;
	
GO

CREATE VIEW Vi_AutorLivro
AS
	
	SELECT	
			A.CodAu AS Codigo,
			A.Nome AS Autor,
			L.Titulo AS Livro,	
					
			STUFF((SELECT '; ' + A.Descricao
				   FROM Livro_Assunto LA1
				   INNER JOIN Assunto A ON A.CodAs = LA1.Assunto_CodAs
				   WHERE LA1.Livro_Codl = LAS.Livro_Codl
				   FOR XML PATH('')), 1, 1, '') AS Assuntos
	FROM Livro_Assunto LAS
	INNER JOIN Livro L ON L.Codl = LAS.Livro_Codl
	INNER JOIN Livro_Autor LA ON LA.Livro_Codl = L.Codl
	INNER JOIN Autor A ON A.CodAu = LA.Autor_CodAu
	GROUP BY
				A.CodAu,
				A.Nome,
				L.Titulo,		
				LAS.Livro_Codl	
;