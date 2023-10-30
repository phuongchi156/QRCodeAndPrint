using System.ComponentModel.DataAnnotations;


namespace QRCodePj.Models;

public class QRCodeModel
{
    [Display(Name = "Enter QRCode Text")]
    public string? Table { get; set; }
    //public DateTime? CreateTime { get; set; }
}