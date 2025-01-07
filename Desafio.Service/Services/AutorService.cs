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

            html += "<div style=\"font-family: Arial, sans-serif;margin: 0;padding: 0;\">";
            html += "<h2 style=\"font-weight: bold;text-align: center;margin-top: 20px;\">Relatório Autor x Livro</h2>";
            html += "<br />";
            html += "<table style=\"border-collapse: collapse;margin: 20px auto;\">";
            html += "<tr>";
            html += "<td style=\"border: 1px solid black;\">Código</td>";
            html += "<td style=\"border: 1px solid black;\">Autor</td>";
            html += "<td style=\"border: 1px solid black;\">Livros</td>";
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

                html += $"<td style=\"border: 1px solid black;\">{codigo}</td>";
                html += $"<td style=\"border: 1px solid black;\">{autor}</td>";

                html += "<td style=\"border: 1px solid black;\">";
                html += "<table style=\"border-collapse: collapse;margin: 20px auto;\">";

                lstautorLivro.ForEach(x =>
                {
                    html += "<tr>";
                    html += $"<td style=\"border: 1px solid black;\">Título : {x.Livro}</td>";
                    html += "</tr>";

                    html += "<tr>";
                    html += $"<td style=\"border: 1px solid black;\">Assuntos : {x.Assuntos}</td>";
                    html += "</tr>";
                });

                html += "</table>";
                html += "</td>";

                html += "</tr>";
            });

            html += "</table>";
            html += "</div>";


            StringReader sr = new StringReader(html);
            Document pdfDoc = new Document(PageSize.A4, 30, 30, 30, 30);
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
