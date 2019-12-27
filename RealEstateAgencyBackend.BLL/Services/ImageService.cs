using AutoMapper;
using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.DAL.Repositories;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class ImageService : IImageService
    {
        private IUnitOfWork _dal;
        private IImageRepository _repository;
        private IMapper _mapper;

        public ImageService(IUnitOfWork dal, IMapper mapper)
        {
            _dal = dal;
            _repository = _dal.ImageRepository;
            _mapper = mapper;
        }

        public ImageDto Create(String base64Image, String folderPath)
        {
            String localPath = SaveImage(base64Image, folderPath);

            PostImage postImage = new PostImage
            {
                ImagePath = localPath
            };

            PostImage createdPostImage = _repository.Create(postImage);

            ImageDto imageDto = _mapper.Map<ImageDto>(createdPostImage);
            return imageDto;
        }

        private String SaveImage(String base64String,String folderPath)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            String filePath = String.Empty;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image img = Image.FromStream(ms);

                filePath = folderPath;
                filePath += DateTime.Now.Ticks + ".jpg";// + "." + img.RawFormat.ToString();// + extension;// postedFile.FileName;// + DateTime.Now.Ticks + extension;
                img.Save(filePath, ImageFormat.Jpeg);
            }

            return filePath;
        }

        public ImageDto Find(int id)
        {
            PostImage postImage = _repository.Find(id);
            ImageDto imageDto = _mapper.Map<ImageDto>(postImage);
            return imageDto;
        }




        public IEnumerable<ImageDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public ImageDto Remove(ImageDto postImage)
        {
            throw new NotImplementedException();
        }

        public ImageDto Update(ImageDto postImage)
        {
            throw new NotImplementedException();
        }
    }
}
