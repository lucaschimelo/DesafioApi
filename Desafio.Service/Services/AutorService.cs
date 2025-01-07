using Desafio.Domain.DTOs;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repositories;
using Desafio.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System;

namespace Desafio.Service.Services
{
    public class AutorService : BaseService<Autor>, IAutorService
    {
        public readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
            : base(autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<ReportAutorLivroDTO> ReportAutorLivroAsync()
        {
            ReportAutorLivroDTO reportAutorLivro = new ReportAutorLivroDTO();

            string html = string.Empty;

            html += "<div>";
            html += "<h2>Relatório Autor x Livro</h2>";
            html += "<table>";
            html += "<tr>";
            html += "<td>Autor</td>";
            html += "<td>Código</td>";
            html += "<td>Livros</td>";
            html += "</tr>";

            IEnumerable<AutorLivroDTO> list = await _autorRepository.GetLivroAutorAsync();

            if (list == null || list.Count() <= 0)
                return null;

            var lstGroupped = list.GroupBy(x => x.Codigo).ToList();

            lstGroupped.ForEach(item =>
            {
                List<AutorLivroDTO> lstautorLivro = item.ToList();
                int codigo = item.Key;
                string autor = lstautorLivro.FirstOrDefault().Autor;

                html += "<tr>";

                html += $"<td>{codigo}</td>";
                html += $"<td>{autor}</td>";

                html += "<td>";
                html += "<table>";

                lstautorLivro.ForEach(x =>
                {
                    html += "<tr>";
                    html += $"<td>Título : {x.Livro}</td>";
                    html += "</tr>";

                    html += "<tr>";
                    html += $"<td>Assuntos : {x.Assuntos}</td>";
                    html += "</tr>";
                });

                html += "</table>";
                html += "</td>";

                html += "</tr>";
            });

            html += "</table>";
            html += "</div>";

            StringReader sr = new StringReader(html);
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            MemoryStream memoryStream = new MemoryStream();

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();

            htmlparser.Parse(sr);
            pdfDoc.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            reportAutorLivro.File = bytes;
            reportAutorLivro.Name = $"RelatorioAutorLivro_{Guid.NewGuid()}.pdf";
            reportAutorLivro.Type = "application/pdf";

            return reportAutorLivro;
        }
    }
}
