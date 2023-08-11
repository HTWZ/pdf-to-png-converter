using IronSoftware.Drawing;
using Microsoft.VisualBasic;
using PDFtoImage;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.NetworkInformation;

namespace Demo_PDFtoPNG
    
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var converter = new Converter();
            var pdfFilePath = "D:/EvokeHub/Demos/Demo_PDFtoPNG/resource/planTwo.pdf";           

            converter.PDFtoImageConverter(pdfFilePath);

            Console.WriteLine("Done");
        }
    }
}