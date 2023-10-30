using Microsoft.AspNetCore.Mvc;
using PdfiumViewer;
using QRCodePj.Models;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using static System.Net.WebRequestMethods;

namespace QRCodePj.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public async Task<JsonResult> PrintCode()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "print-code-template.html");
        string content = System.IO.File.ReadAllText(path);
        var replace = new System.Text.StringBuilder();

        replace.AppendLine(@"<img src='/image.png'/>");
        content = content.Replace("{content}", replace.ToString());
        await Task.Yield();

        StringBuilder sb = new StringBuilder();
        using (System.IO.StreamWriter file = new System.IO.StreamWriter("C://Users//Admin//Downloads//testPrint.pdf"))
        {

            file.WriteLine(sb.ToString());

        }

        return Json(content);
    }
}