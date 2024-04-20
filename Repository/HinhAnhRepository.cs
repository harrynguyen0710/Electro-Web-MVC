using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repository
{
    public class HinhAnhRepository :  IHinhAnh
    {
        private readonly IWebHostEnvironment _webHost;

        public HinhAnhRepository(IWebHostEnvironment webHost)
        {   
            _webHost = webHost;
        }
        public string GetProfilePhotoFileName(IFormFile file, string folder)
        {
            string? fileName = null;
            if (file != null)
            {
                string uploadDir = Path.Combine(_webHost.WebRootPath, folder);
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return fileName;
        }
      
    }
}



