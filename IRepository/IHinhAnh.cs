using DACS.Models;

namespace DACS.IRepository
{
    public interface IHinhAnh
    {
        string GetProfilePhotoFileName(IFormFile file, string folder);
    }
}
