using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgencyBackend.BLL.DTO
{
    public class ImageCreateDto
    {
        public String ImageData { get; set; }
    }

    public class ImageDto
    {
        public int Id { get; set; }
        public String ImagePath { get; set; }
    }
}
