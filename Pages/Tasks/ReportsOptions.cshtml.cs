using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskMate.Pages.Tasks;

public class ReportsOptions : PageModel
{
    [BindProperty] public string ReportType { get; set; } = "todos";
    [BindProperty] public bool IncludeTitulo { get; set; } = true;
    [BindProperty] public bool IncludeDescricao { get; set; } = true;
    [BindProperty] public bool IncludeStatus { get; set; } = true;
    [BindProperty] public bool IncludeDataCriacao { get; set; } = true;

    public IActionResult OnPost()
    {
        return RedirectToPage("/Tasks/Reports", new
        {
            type = ReportType,
            titulo = IncludeTitulo,
            descricao = IncludeDescricao,
            status = IncludeStatus,
            dataCriacao = IncludeDataCriacao
        });
    }
}
