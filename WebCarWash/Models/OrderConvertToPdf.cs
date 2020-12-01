using InDesignServer;
using System;
using System.IO;
using System.Text;

namespace WebCarWash.Models
{
    public static class OrderConvert
    {
        public static bool ToPdfFile(
            WebCarWash.Domain.Entities.Order currOrder, 
            string fileTemplate,
            string pathtoSave, 
            string fileName)
        {
            bool flReturn = false;
            string pathSave = String.Empty;

            if (!File.Exists(fileTemplate))
                throw new ArgumentNullException("Template not found.");


            // var appS = (InDesignServer.Application)System.Runtime.InteropServices.Marshal.BindToMoniker(@"configuration_12345");
            
            Type oType = Type.GetTypeFromProgID("InDesignServer.Application", true);  //"InDesign.Application"
            if (oType != null)
            {

                try
                {
                    var app = (Application)Activator.CreateInstance(oType);   

                    var doc = (Document)app.Open(fileTemplate);

                    // get the first page
                    Page page = (Page)doc.Pages[1];

                    var count = doc.Pages.Count;
                    var frameCount = page.TextFrames.Count;
                    string newContent = String.Empty;

                    for (int i = 1; i <= page.TextFrames.Count; i++)          
                    {
                        var frame = (TextFrame)page.TextFrames[i];
                        var frName = frame.Name;
                        string _contentStr = frame.Contents;
                        if (_contentStr.Contains("Services"))
                        {
                            StringBuilder sb = new StringBuilder(currOrder.Services.Count);
                            int c = 0;
                            foreach (var service in currOrder.Services)
                            {
                                sb.Append($"{++c} .  {service.Title}     -{service.Cost} $ \r\n");
                            }

                            newContent = sb.ToString();
                        }
                        else if (_contentStr.Contains("OrderId"))
                            newContent = $" № {currOrder.OrderId}      with {currOrder.Services.Count} operations:";
                        else if (_contentStr.Contains("ClientName"))
                            newContent = $" Mr. {currOrder.Client.Name} !";

                        else if (_contentStr.Contains("ServiceDate"))
                            newContent = currOrder.ServiceDate.ToString("g");
                        else if (_contentStr.Contains("sum"))
                            newContent = $" {currOrder.Price}$";
                        else
                            continue;

                        frame.ParentStory.Contents = newContent;
                    }

                    var dir = System.IO.Path.GetDirectoryName(fileTemplate);
                    pathSave = pathtoSave + fileName;

                    if (File.Exists(pathSave))
                    {
                        File.Delete(pathSave);
                    }

                    doc.Export(idExportFormat.idPDFType, pathSave);

                    doc.Close(idSaveOptions.idNo);

                    // app.Quit(idSaveOptions.idNo);

                    flReturn = true;
                }
                catch (Exception _e)
                {
                    string _err = _e.Message;

                }
            }

            return flReturn;
        }

    }
}