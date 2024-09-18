# PdfSharp Extensions
Draw Zxing barcode in PDFsharp page
```
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ZXing;
using ZXing.Common;
using Extensions; //引入扩展
 
PdfDocument newDocument = new PdfDocument();  
PdfPage page = newDocument.AddPage();    
XGraphics gfx = XGraphics.FromPdfPage(page);
var barcodeWriter = new ZXing.BarcodeWriterGeneric
{
    Format = BarcodeFormat.CODE_93,  
    Options = new EncodingOptions { Height = 80, Width = 200 }  
};
var bitMatrix = barcodeWriter.Encode("24341873");  
gfx.DrawBitMatrix(bitMatrix, new XPoint(100, 100));   
newDocument.Save("output.pdf");   
newDocument.Close();
```
