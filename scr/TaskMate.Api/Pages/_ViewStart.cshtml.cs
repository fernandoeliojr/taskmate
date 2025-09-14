using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TaskMate.Api.Pages
{
    public class _ViewStart : PageModel
    {
        private readonly ILogger<_ViewStart> _logger;

        public _ViewStart(ILogger<_ViewStart> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}