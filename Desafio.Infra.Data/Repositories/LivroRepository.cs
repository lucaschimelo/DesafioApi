using Dapper;
using Desafio.Domain.DTOs;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repositories;
using Desafio.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Infra.Data.Repositories
{
    public class LivroRepository : BaseRepository<Livro>, ILivroRepository
    {
        private readonly DesafioContext _context;        
        public LivroRepository(DesafioContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task<Livro> GetAsync(int id)
        {
            Livro livro = await base.GetAsync(id);
            livro.Assuntos = await this.GetLivroAssuntoAsync(id);
            livro.Autores = await this.GetLivroAutorAsync(id);
            livro.LivroFormaComprasDTO = await this.GetFormaCompraAsync(id);

            return livro;
        }
        public async Task<IEnumerable<LivroFormaComprasDTO>> GetFormaCompraAsync(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                return await connection.QueryAsync<LivroFormaComprasDTO>(@"SELECT LFC.Livro_Codl AS Codl, FC.CodFor, FC.Descricao, LFC.Valor
                                                                          FROM Livro_FormaCompra LFC 
                                                                          INNER JOIN FormaCompra FC ON FC.CodFor = LFC.FormaCompra_CodFor 
                                                                          WHERE LFC.Livro_Codl = @Livro_Codl;", new { Livro_Codl = id });
            }
        }
        public async Task<IEnumerable<LivroAutor>> GetLivroAutorAsync(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                return await connection.QueryAsync<LivroAutor>("SELECT * FROM Livro_Autor WHERE Livro_Codl = @Livro_Codl;", new { Livro_Codl = id });
            }
        }
        public async Task<IEnumerable<LivroAssunto>> GetLivroAssuntoAsync(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                return await connection.QueryAsync<LivroAssunto>("SELECT * FROM Livro_Assunto WHERE Livro_Codl = @Livro_Codl;", new { Livro_Codl = id });
            }
        }

        public int Update(Livro livro)
        {
            int id = 0;

            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int codl = livro.Codl;

                        connection.Execute(this.UpdateLivroSql(), livro, transaction);
                        connection.Execute(this.DeleteLivroFormaCompraSql(), new { Codl = codl }, transaction);
                        connection.Execute(this.DeleteLivroAutorSql(), new { Codl = codl }, transaction);
                        connection.Execute(this.DeleteLivroAssuntoSql(), new { Codl = codl }, transaction);                       

                        livro.Assuntos?.ToList()?.ForEach(item =>
                        {
                            item.Livro_Codl = codl;
                            connection.Execute(this.InsertLivroAssuntoSql(), item, transaction);
                        });

                        livro.Autores?.ToList()?.ForEach(item =>
                        {
                            item.Livro_Codl = codl;
                            connection.Execute(this.InsertLivroAutorSql(), item, transaction);
                        });

                        livro.FormaCompras?.ToList()?.ForEach(item =>
                        {
                            item.Livro_Codl = codl;
                            connection.Execute(this.InsertLivroFormaCompraSql(), item, transaction);
                        });

                        transaction.Commit();

                        id = codl;
                    }
                    catch (Exception ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                            transaction.Dispose();
                        }
                    }
                    finally
                    {
                        if (connection != null)
                            connection.Dispose();
                    }
                }
            }

            return id;
        }

        public bool Delete(int codl)
        {
            bool success = false;

            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(this.DeleteLivroFormaCompraSql(), new { Codl = codl}, transaction);
                        connection.Execute(this.DeleteLivroAutorSql(), new { Codl = codl }, transaction);
                        connection.Execute(this.DeleteLivroAssuntoSql(), new { Codl = codl }, transaction);
                        connection.Execute(this.DeleteLivroSql(), new { Codl = codl }, transaction);

                        transaction.Commit();

                        success = true;
                    }
                    catch (Exception ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                            transaction.Dispose();
                        }
                    }
                    finally
                    {
                        if (connection != null)
                            connection.Dispose();
                    }
                }
            }

            return success;
        }

        public int Insert(Livro livro)
        {
            int id = 0;

            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int codl = connection.ExecuteScalar<int>(this.InsertLivroSql(), livro, transaction);

                        livro.Assuntos?.ToList()?.ForEach(item =>
                        {
                            item.Livro_Codl = codl;
                            connection.Execute(this.InsertLivroAssuntoSql(), item, transaction);
                        });

                        livro.Autores?.ToList()?.ForEach(item =>
                        {
                            item.Livro_Codl = codl;
                            connection.Execute(this.InsertLivroAutorSql(), item, transaction);
                        });

                        livro.FormaCompras?.ToList()?.ForEach(item =>
                        {
                            item.Livro_Codl = codl;
                            connection.Execute(this.InsertLivroFormaCompraSql(), item, transaction);
                        });

                        transaction.Commit();

                        id = codl;
                    }
                    catch(Exception ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                            transaction.Dispose();
                        }
                    }
                    finally
                    {
                        if (connection != null)
                        {
                            connection.Close();
                            connection.Dispose();
                        }
                    }
                }
            }

            return id;
        }

        private string InsertLivroSql()
        {
            return "INSERT INTO Livro(Titulo, Editora, Edicao, AnoPublicacao)VALUES(@Titulo, @Editora, @Edicao, @AnoPublicacao);SELECT SCOPE_IDENTITY();";
        }

        private string InsertLivroAssuntoSql()
        {
            return "INSERT INTO Livro_Assunto(Livro_Codl, Assunto_CodAs)VALUES(@Livro_Codl, @Assunto_CodAs);";
        }

        private string InsertLivroAutorSql()
        {
            return "INSERT INTO Livro_Autor(Livro_Codl, Autor_CodAu)VALUES(@Livro_Codl, @Autor_CodAu);";
        }

        private string InsertLivroFormaCompraSql()
        {
            return "INSERT INTO Livro_FormaCompra(Livro_Codl, FormaCompra_CodFor, Valor)VALUES(@Livro_Codl, @FormaCompra_CodFor, @Valor);";
        }

        private string DeleteLivroFormaCompraSql()
        {
            return "DELETE FROM Livro_FormaCompra WHERE Livro_Codl = @Codl;";
        }

        private string DeleteLivroAssuntoSql()
        {
            return "DELETE FROM Livro_Assunto WHERE Livro_Codl = @Codl;";
        }

        private string DeleteLivroAutorSql()
        {
            return "DELETE FROM Livro_Autor WHERE Livro_Codl = @Codl;";
        }

        private string DeleteLivroSql()
        {
            return "DELETE FROM Livro WHERE Codl = @Codl;";
        }

        private string UpdateLivroSql()
        {
            return "UPDATE Livro SET Titulo = @Titulo, Editora = @Editora, Edicao = @Edicao, AnoPublicacao = @AnoPublicacao WHERE Codl = @Codl;";
        }
    }
}
