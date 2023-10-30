using Microsoft.AspNetCore.Mvc;
using PdfiumViewer;
using QRCodePj.Models;
using QRCoder;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;

using System.Drawing;
using System.Drawing.Printing;

namespace QRCodePj.Controllers;

public class CreateQRCodeController : Controller
{
    [HttpGet]
    public IActionResult CreateQRCode()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateQRCode(QRCodeModel model)
    {
        //model.CreateTime = DateTime.Now;
        //string code = $"{model.CreateTime.ToString()}; {model.Table}"; 
        QRCodeGenerator QrGene = new QRCodeGenerator();
        QRCodeData QrInfor = QrGene.CreateQrCode(model.Table, QRCodeGenerator.ECCLevel.Q);
        QRCode QrCode = new QRCode(QrInfor);
        Bitmap QrBitmap = QrCode.GetGraphic(60);

        byte[] BitmapArray = QrBitmap.BitmapToByteArray();
        string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
        ViewBag.QrCodeUri = QrUri;

        //string printer = "Fuji ApeosPort 3560";
        //string paperName = "A5 (148 x 210mm)";
        //string fileName = "C:/Users/Admin/Downloads/Home Page - QRCodePj.pdf";
        //int copies = 1;

        //BitmapExtension.PrintPDF(printer, paperName, fileName, copies);

        BitmapExtension.SendToPrinter();
        return View();
    }



    //[HttpGet]
    //public async Task<JsonResult> PrintCode()
    //{
    //    var path = Path.Combine(Directory.GetCurrentDirectory(), "@ViewBag.QrCodeUri");
    //    string content = System.IO.File.ReadAllText(path);
    //    await Task.Yield();
    //    return Json(content);
    //}

}
public static class BitmapExtension
{
    public static byte[] BitmapToByteArray(this Bitmap bitmap)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }

    //public static bool PrintPDF(string printer, string paperName, string filename, int copies)
    //{
    //    try
    //    {
    //        // Create the printer settings for our printer
    //        var printerSettings = new PrinterSettings
    //        {
    //            PrinterName = printer,
    //            Copies = (short)copies,
    //        };

    //        // Create our page settings for the paper size selected
    //        var pageSettings = new PageSettings(printerSettings)
    //        {
    //            Margins = new Margins(0, 0, 0, 0),
    //        };
    //        foreach (PaperSize paperSize in printerSettings.PaperSizes)
    //        {
    //            if (paperSize.PaperName == paperName)
    //            {
    //                pageSettings.PaperSize = paperSize;
    //                break;
    //            }
    //        }

    //        // Now print the PDF document
    //        using (var document = PdfDocument.Load(filename))
    //        {
    //            using (var printDocument = document.CreatePrintDocument())
    //            {
    //                printDocument.PrinterSettings = printerSettings;
    //                printDocument.DefaultPageSettings = pageSettings;
    //                printDocument.PrintController = new StandardPrintController();
    //                printDocument.Print();
    //            }
    //        }
    //        return true;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

    //public static void SendToPrinter()
    //{
    //    ProcessStartInfo info = new ProcessStartInfo();
    //    info.Verb = "print";
    //    info.FileName = "C:/Users/Admin/Downloads/Home Page - QRCodePj.pdf";
    //    info.CreateNoWindow = true;
    //    info.WindowStyle = ProcessWindowStyle.Hidden;

    //    Process p = new Process();
    //    p.StartInfo = info;
    //    p.Start();

    //    p.WaitForInputIdle();
    //    System.Threading.Thread.Sleep(3000);
    //    if (false == p.CloseMainWindow())
    //        p.Kill();
    //}

    //public static void SendToPrinter()
    //{
    //    ChromePdfRenderer Renderer = new ChromePdfRenderer();
    //    //PdfDocument pdfDocument = Renderer.RenderUrlAsPdf("D:/test/QRCodePj/QRCodePj/wwwroot/image.png");
    //    IronPdf.PdfDocument pdfDocument = Renderer.RenderHtmlAsPdf("<h1> Hello World </h1>");
    //    using (var printDocument = pdfDocument.GetPrintDocument())
    //    {
    //        //printDocument.PrinterSettings.PrinterName = "Fuji ApeosPort 3560";
    //        printDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF";
    //        printDocument.DefaultPageSettings.PrinterResolution = new PrinterResolution
    //        {
    //            Kind = PrinterResolutionKind.Custom,
    //            X = 200,
    //            Y = 300
    //        };

    //        printDocument.Print();
    //    }
    //}

    public static void SendToPrinter()
    {
        PrinterSettings settings = new PrinterSettings();
        //string PrinterName = settings.PrinterName;
        string PrinterName = "Microsoft Print to PDF";
        //string PrinterName = "Fuji ApeosPort 3560";

        //set paper size
        PaperSize oPS = new PaperSize
        {
            RawKind = (int)PaperKind.A4
        };
        //choose the tray here
        PaperSource oPSource = new PaperSource
        {
            RawKind = (int)PaperSourceKind.Upper
        };
        PrintDocument printDoc = new PrintDocument
        {
            PrinterSettings = settings,
        };
        //set printer name here it's the default printer
        printDoc.PrinterSettings.PrinterName = PrinterName;
        printDoc.DefaultPageSettings.PaperSize = oPS;
        printDoc.DefaultPageSettings.PaperSource = oPSource;

        printDoc.PrintPage += new PrintPageEventHandler((sender, args) =>
        {
            string imageFilePath = "D:\\test\\QRCodePj\\QRCodePj\\wwwroot\\image.png";
            System.Drawing.Image img = System.Drawing.Image.FromFile(imageFilePath);
            Point loc = new Point(100, 100);

            args.Graphics.DrawImage(img, loc);
        });

        printDoc.Print();
        printDoc.Dispose();
    }
}
//public static class PrintBill
//{
//    public static void btnPrint()
//    {
//        PrintDocument pDoc = new PrintDocument();
//        pDoc.Print();

//    }
//}
