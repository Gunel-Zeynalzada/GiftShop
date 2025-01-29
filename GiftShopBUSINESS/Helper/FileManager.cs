using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftShopBUSINESS.Helper
{
    public static class FileManager
    {
        public static string Upload(this IFormFile file, string envpath, string folderName)
        {
            string fileName = file.FileName;
            fileName = Guid.NewGuid().ToString() + fileName;
            string path = envpath + folderName + fileName;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
