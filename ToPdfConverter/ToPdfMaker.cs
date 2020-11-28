using System;

using System.IO;
using WebCarWash.Models;

namespace ToPdfConverter
{
    public static  class ToPdfMaker
    {
        public static string ConvertToFile(string fileTemplate, Order inpOrder)
        {
            string pathReturn = String.Empty;

            if (!File.Exists(fileTemplate))
                throw new ArgumentNullException("Template not found.");


            if (inpOrder == null)
                throw new ArgumentNullException("Dont data for convert.");

            Type oType = Type.GetTypeFromProgID("InDesign.Application", true);
            if (oType != null)
            {
                //  var app = (InDesign.Application)Activator.CreateInstance(oType);
                try
                {
                    var app = (InDesign.Application)Activator.CreateInstance(oType);

                    // get a reference to the current active document
                    //  var doc =(InDesign.Document) app.Open("car_wash.indd");
                    // get a reference to the current active document
                    // InDesign.Document doc = app.ActiveDocument;

                    InDesign.Document doc = (InDesign.Document)app.Open(fileTemplate);  // @"d:\Irina\Work\Example\ConsolуIndesignEx\bin\Debug\car_wash.indd");

                    //  // get the first page
                    InDesign.Page page = (InDesign.Page)doc.Pages[1]; //1e pagina

                    var count = doc.Pages.Count;
                    var frameCount = page.TextFrames.Count;


                    // get the first textframe
                    InDesign.TextFrame frame = (InDesign.TextFrame)page.TextFrames[1];
                    var oldText = frame.Contents;
                    Console.WriteLine(oldText);
                    frame.Contents = inpOrder.Client.Name;

                    var dir = Path.GetDirectoryName(fileTemplate);
                    pathReturn = Path.Combine(dir, inpOrder.OrderId.ToString())+ ".PDF"; // @"d:\Irina\Work\Example\ConsolуIndesignEx\bin\Debug\car_wash.pdf";

                   
                    doc.Export(InDesign.idExportFormat.idPDFType, pathReturn);

                    // document.textFrames.itemByName("shmullus").contents = "The Doctor";

                    Console.WriteLine("Export to PDF");
                    doc.Close(InDesign.idSaveOptions.idNo);

                }
                catch (Exception _e)
                {
                    string _err = _e.Message;
                    Console.WriteLine(_err);
                }
            }

                return pathReturn;
        }


           

    }
}
