using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Dept_subDept_Module.Services
{
    public class FileUploader
    {
        public string FilePath(IFormFile file)
        {
            var fileLocation = "";
            if (file != null)
            {
                var x = Guid.NewGuid();
                var fileName = x + Path.GetExtension(file.FileName); // Ensure correct file extension
                var filepathStr = "fileupload";

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filepathStr);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filePath = Path.Combine(path, fileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                fileLocation = $"/{filepathStr}/{fileName}"; // Correct URL path format
            }
            else
            {
                fileLocation = "No File Found !!";
            }

            return fileLocation;
        }
    }
}
