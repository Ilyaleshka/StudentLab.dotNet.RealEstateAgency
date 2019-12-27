using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.BLL.Interfaces
{
    public interface IImageService
    {
        IEnumerable<ImageDto> GetAll();

        ImageDto Find(int id);

        ImageDto Remove(ImageDto postImage);

        ImageDto Update(ImageDto postImage);

        ImageDto Create(String base64Image,String folderPath);

    }
}
