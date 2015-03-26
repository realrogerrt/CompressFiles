using System.IO;
using CompressionCore;
using CompressionCore.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CompresssingTestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string notepad = "test.txt";
            string word = "proj.docx";
            string video = "forex.mp4";
            string image = "image.jpg";
            string pdf = "dmv.pdf";
            string american = "american.mp4";

            //File.Delete("compressed.cf");
            //var fileOut = new FileStream("compressed.cf", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //var c = new Compressor("resume.doc");
            //c.RunProccess(fileOut);

            var compressed = new FileStream("compressed.cf", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var uc = new UnCompressor(compressed);
            var originalBuild = new FileStream("newresume.doc", FileMode.Create, FileAccess.ReadWrite);
            uc.RunProccess(originalBuild);
            compressed.Close();
        }
    }
}
