
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Desafio')
  BEGIN
    CREATE DATABASE [Desafio]
  END
GO