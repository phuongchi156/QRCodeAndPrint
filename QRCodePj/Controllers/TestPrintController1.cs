using Microsoft.AspNetCore.Mvc;
using PdfiumViewer;
using System.Drawing.Printing;

namespace QRCodePj.Controllers;

public class TestPrintController1 : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

