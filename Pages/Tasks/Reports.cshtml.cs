using System.Data;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Data;
using TaskMate.Domain.Entities;
using Document = iText.Layout.Document;

namespace TaskMate.Pages.Tasks;

public class Reports : PageModel
{
    private readonly TaskMateDbContext _context;
    public Reports(TaskMateDbContext context) => _context = context;

    public IList<TaskItem> TaskItems { get; set; } = new List<TaskItem>();


    public IActionResult OnGet(string? type, bool titulo = true, bool descricao = true, bool status = true, bool dataCriacao = false)
    {
        var query = _context.Tasks.AsQueryable();

        string tituloRelatorio = "Relatório de Tarefas - Geral";

        if (type == "concluidas")
        {
            query = query.Where(t => t.Status == "Concluída");
            tituloRelatorio = "Relatório de Tarefas Concluídas";
        }
        else if (type == "afazer")
        {
            query = query.Where(t => t.Status == "A Fazer");
            tituloRelatorio = "Relatório de Tarefas A Fazer";
        }
        else if (type == "emprogresso")
        {
            query = query.Where(t => t.Status == "Em Progresso");
            tituloRelatorio = "Relatório de Tarefas Em Progresso";
        }

        var tasks = query.ToList();

        using var ms = new MemoryStream();
        var writer = new PdfWriter(ms);
        var pdf = new PdfDocument(writer);
        var doc = new Document(pdf);

        doc.Add(new Paragraph(tituloRelatorio).SetFontSize(16));
        doc.Add(new Paragraph($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n"));

        if (tasks.Count == 0)
            doc.Add(new Paragraph("Nenhuma tarefa encontrada."));
        else
        {
            foreach (var task in tasks)
            {
                if (titulo) doc.Add(new Paragraph($"Titulo: {task.Titulo}"));
                if (descricao) doc.Add(new Paragraph($"Descrição: {task.Descricao}"));
                if (status) doc.Add(new Paragraph($"Status: {task.Status}"));
                if (dataCriacao) doc.Add(new Paragraph($"Data de Criação: {task.DataCriacao:dd/MM/yyyy}"));
                doc.Add(new Paragraph("------------------------"));
            }
        }

        doc.Close();

        return File(ms.ToArray(), "application/pdf", $"Relatorio_{type ?? "geral"}.pdf");
    }
}
