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

        public ImageDto Save(String base64Image, String appFolderPath, String folderPath)
        {
            String localPath = SaveImage(base64Image, appFolderPath, folderPath);

            PostImage postImage = new PostImage
            {
                ImagePath = localPath
            };

            PostImage createdPostImage = _repository.Create(postImage);
			_dal.Save();
			ImageDto imageDto = _mapper.Map<ImageDto>(createdPostImage);
            return imageDto;
        }


		public ImageDto Create(ImageDto image)
		{
			PostImage postImage = _mapper.Map<PostImage>(image);
			PostImage createdPostImage = _repository.Create(postImage);
			_dal.Save();
			ImageDto imageDto = _mapper.Map<ImageDto>(createdPostImage);
			return imageDto;
		}

		private String SaveImage(String base64String, String appFolderPath, String folderPath)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            String relativeFilePath = String.Empty;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image img = Image.FromStream(ms);
				String fileName = DateTime.Now.Ticks + ".jpg";
				String filePath = Path.Combine(appFolderPath, folderPath, fileName); 
				relativeFilePath = Path.Combine(folderPath, fileName);
				img.Save(filePath, ImageFormat.Jpeg);
            }

            return relativeFilePath.Replace("\\", "/");
        }

        public ImageDto Find(int id)
        {
            PostImage postImage = _repository.Find(id);
            ImageDto imageDto = _mapper.Map<ImageDto>(postImage);
            return imageDto;
        }

        public IEnumerable<ImageDto> GetAll()
        {
			List<ImageDto> ImagesDto = _mapper.Map<IEnumerable<PostImage>, List<ImageDto>>(_repository.GetAll());
			return ImagesDto;

		}

        public ImageDto Remove(ImageDto postImage)
        {
			var temp = _repository.Remove(postImage.Id);
			_dal.Save();

			ImageDto ImageDto = _mapper.Map<ImageDto>(temp);
			return ImageDto;
		}

        public ImageDto Update(ImageDto image)
        {
			PostImage postImage = _mapper.Map<PostImage>(image);
			_repository.Update(postImage);
			_dal.Save();

			return _mapper.Map<ImageDto>(postImage);
		}
    }
}
