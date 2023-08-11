using BitMiracle.Docotic.Pdf;
using ExpertPdf;
using PDFtoImage;
using SkiaSharp;
using System.Drawing;
using System.Drawing.Imaging;

namespace Demo_PDFtoPNG
{
    public class Converter
    {
        private const double Scale = 4.0;

        public byte[] pdfToByteArray(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public string pdfToBase64String(string filePath)
        {
            // Read the PDF file into a byte array
            byte[] pdfBytes = File.ReadAllBytes(filePath);

            // Convert the byte array to a Base64 string
            string base64String = Convert.ToBase64String(pdfBytes);

            Console.WriteLine("PDF converted to Base64:");
            return base64String;
        }

        public byte[] BmpToPng(MemoryStream stream)
        {
            using var bitmap = Bitmap.FromStream(stream);
            bitmap.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }

        public void PDFtoImageConverter(string pdfFilePath)
        {
            // Create streams for the PDF and image
            var outputPath = @"D:\image\PdfToPng\output.png";
            FileStream pdfFileStream = new FileStream(pdfFilePath, FileMode.Open);
            MemoryStream memoryStream = new MemoryStream();
            pdfFileStream.CopyTo(memoryStream);
            //400 15 sec
            using (MemoryStream imageStream = new MemoryStream())
            {
                // Call SavePng method to convert PDF and save as PNG
                PDFtoImage.Conversion.SavePng(
                    outputPath,         // Stream to store the generated image
                    memoryStream,           // Stream containing the input PDF
                    password: null,      // Password for encrypted PDF (if any)
                    page: 0,             // Page number to convert (0 for all pages)
                    dpi: 600,            // DPI resolution for the image
                    width: 10000,         // Width of the output image (null to maintain aspect ratio)
                    height: null,        // Height of the output image (null to maintain aspect ratio)
                    withAnnotations: false, // Include annotations if true
                    withFormFill: true,     // Include form field content if true
                    withAspectRatio: true,  // Maintain aspect ratio if true
                    rotation: PDFtoImage.PdfRotation.Rotate0 // Rotation of the PDF content
                );

            }
        }

        public void IronConverter(string pdfFilePath)
        {
            IronPdf.License.LicenseKey = "IRONSUITE.HNINTHETWAIZAW8919.GMAIL.COM.14327-584FF71318-AKQIJV22BZDKO6-DVR2VNV7CIAV-NJHF35GSGPTE-LREN7LV2BWAB-BU3VMUC4LYSU-EEJ6HDQKBDAX-GEEVGS-T3Q2RKZQ5BWKUA-DEPLOYMENT.TRIAL-TE226P.TRIAL.EXPIRES.09.SEP.2023";
            var outputPath = @"D:\image\Iron\output.png";
            var pdf = IronPdf.PdfDocument.FromFile(pdfFilePath);

            pdf.RasterizeToImageFiles(outputPath, DPI: 600);
        }

        public void DocoticConverter(string pdfFilePath)
        {
            var outputPath = @"D:\image\Docotic\";
            using (var pdf = new BitMiracle.Docotic.Pdf.PdfDocument(pdfFilePath))
            {
                PdfDrawOptions options = PdfDrawOptions.Create();
                options.BackgroundColor = new PdfRgbColor(255, 255, 255);
                options.HorizontalResolution = 600;
                options.VerticalResolution = 600;

                for (int i = 0; i < pdf.PageCount; ++i)
                    pdf.Pages[i].Save($"{outputPath}page_{i}.png", options);
            }
        }
    }
}
