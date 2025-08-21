using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models.Helpers
{
    public static class StoreImageHelper
    {
        public static void StoreImage(string imageName, string ImagePath)
        {
            string imagesFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            if (!System.IO.Directory.Exists(imagesFolder)) //create folder
            {
                System.IO.Directory.CreateDirectory(imagesFolder);
            }
            string destPath = System.IO.Path.Combine(imagesFolder, imageName);
            System.IO.File.Copy(ImagePath, destPath, true);
        }
    }
}
