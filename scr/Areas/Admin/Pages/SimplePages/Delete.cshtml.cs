﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioWebApp.Models.SimplePages;
using PortfolioWebApp.Services;

namespace PortfolioWebApp.Areas.Admin.Pages.SimplePages
{
    public class DeleteModel : PageModel
    {
        private readonly SimplePageService _simplePageService;

        public DeleteModel(SimplePageService simplePageService)
        {
            _simplePageService = simplePageService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        [Required]
        [HiddenInput]
        public int DeleteId { get; set; }

        public SimplePage SimplePage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id != null)
            {
                SimplePage = await _simplePageService.GetPageByIdAsync((int)id);
                if(SimplePage != null)
                {
                    DeleteId = (int)id;
                    return Page();
                }
                return LocalRedirect("/Admin/SimplePages");
            }
            return LocalRedirect("/Admin/SimplePages");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (DeleteId != 0)
                {
                    (bool, string) result = await _simplePageService.DeletePageByIdAsync(DeleteId);
                    if (result.Item1)
                    {
                        StatusMessage = result.Item2;
                        return LocalRedirect("/Admin/SimplePages");
                    }
                    else
                    {
                        StatusMessage = "Error: " + result.Item2;
                        return Page();
                    }
                }
            }
            return LocalRedirect("/Admin/SimplePages");
        }
    }
}