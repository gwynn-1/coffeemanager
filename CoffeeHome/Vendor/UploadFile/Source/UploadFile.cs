using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Vendor.UploadFile.Source
{
    public static class UploadFile
    {
        public static string UploadFileToUploads(string path,string name)
        {

            string returnpath = @"\Asset\SaveAsset\" + DateTime.Now.ToString("MMddyyyy") + @"\"+name+".jpg";
            string newDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Asset\SaveAsset\" + DateTime.Now.ToString("MMddyyyy");
            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }
            newDir += @"\"+name + ".jpg";
            File.Copy(path, newDir,true);
            
            return returnpath;
        }
    }
}
