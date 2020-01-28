using AutoMapper;
using Microsoft.AspNet.Identity;
using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.DAL.Repositories;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class RentalAnnouncementService : IRentalAnnouncementService
    {
        private IUnitOfWork _dal;
        private IRentalAnnouncementRepository _repository;
        private IMapper _mapper;

        private UserManager<User> _userManager;

        public RentalAnnouncementService(IUnitOfWork dal, IMapper mapper)
        {
            _dal = dal;
            _repository = _dal.RentalAnnouncementRepository;
            _mapper = mapper;
            _userManager = new UserManager<User>(_dal.UserRepository);
        }

        public RentalAnnouncementDto Create(RentalAnnouncementDto rentalAnnouncementDto)
        {
            RentalAnnouncement rentalAnnouncement = _mapper.Map<RentalAnnouncementDto, RentalAnnouncement>(rentalAnnouncementDto);

            User user = _userManager.FindById(rentalAnnouncementDto.UserId);
            rentalAnnouncement.User = user;

            RentalAnnouncement createdRentalAnnouncement = _repository.Create(rentalAnnouncement);

            foreach (var image in rentalAnnouncementDto.Images)
            {
				PostImage postImage = _mapper.Map<PostImage>(image);
				PostImage createdPostImage = _dal.ImageRepository.Create(postImage);
				createdPostImage.RentalAnnouncement = createdRentalAnnouncement;
			}

            _dal.Save();

            return _mapper.Map<RentalAnnouncement,RentalAnnouncementDto>(createdRentalAnnouncement);
        }

        public RentalAnnouncementDto Find(int id)
        {
            RentalAnnouncement rentalAnnouncement = _repository.Find(id);
            RentalAnnouncementDto rentalAnnouncementDto = _mapper.Map<RentalAnnouncement,RentalAnnouncementDto> (rentalAnnouncement);

            return rentalAnnouncementDto;
        }

        //

        public IEnumerable<RentalAnnouncementDto> GetAll()
        {
            IQueryable<RentalAnnouncement> announcements = _repository.GetAll();

			announcements = announcements.Where(announcement => announcement.Reservations
					.All(reservation => ((!reservation.IsActive) && reservation.IsConfirmed)));				

            List<RentalAnnouncementDto> rentalAnnouncementDtos = _mapper.Map<IEnumerable<RentalAnnouncement>, List<RentalAnnouncementDto>>(announcements);

            return rentalAnnouncementDtos;
        }

		public RentalAnnouncementPageDto GetPageWithFilters(Int32 pageNumber, Int32 pageSize, NameValueCollection filteringParams)
		{
			Int32 maxCost, minCost, maxArea, minArea;
			try
			{
				maxCost = String.IsNullOrEmpty(filteringParams["maxCost"]) ? Int32.MaxValue : Int32.Parse(filteringParams["maxCost"]);
				minCost = String.IsNullOrEmpty(filteringParams["minCost"]) ? 0 : Int32.Parse(filteringParams["minCost"]);
				maxArea = String.IsNullOrEmpty(filteringParams["maxArea"]) ? Int32.MaxValue : Int32.Parse(filteringParams["maxArea"]);
				minArea = String.IsNullOrEmpty(filteringParams["minArea"]) ? 0 : Int32.Parse(filteringParams["minArea"]);
			}
			catch
			{
				throw new ArgumentException("Invalid filter parameter");
			}

			IQueryable<RentalAnnouncement> announcements = _repository.GetAll();

			announcements = announcements.Where(announcement => 
						(announcement.Cost >= minCost && announcement.Cost <= maxCost)
					&& (announcement.Area >= minArea && announcement.Area <= maxArea)
					&& announcement.Reservations.All(reservation => ((!reservation.IsActive) && (reservation.IsConfirmed)) || ((!reservation.IsRejected) && (reservation.IsConfirmed))));

			int skip = (pageNumber - 1) * pageSize;
			Int32 announcementCount = (int)Math.Ceiling(announcements.Count() / (float)pageSize);
			announcements = announcements.OrderByDescending(g => g.Id).Skip(skip).Take(pageSize);


			List<RentalAnnouncementDto> rentalAnnouncementDtos = _mapper.Map<IEnumerable<RentalAnnouncement>, List<RentalAnnouncementDto>>(announcements);

			RentalAnnouncementPageDto rentalAnnouncementPageDto = new RentalAnnouncementPageDto
			{
				CurrentPage = pageNumber,
				PageCount = announcementCount,
				RentalAnnouncements = rentalAnnouncementDtos
			};

			return rentalAnnouncementPageDto;
		}

		public RentalAnnouncementDto Remove(RentalAnnouncementDto rentalAnnouncement)
        {
            var temp =  _repository.Remove(rentalAnnouncement.Id);
            _dal.Save();

            RentalAnnouncementDto rentalAnnouncementDto = _mapper.Map<RentalAnnouncement,RentalAnnouncementDto> (temp);
            return rentalAnnouncementDto;
        }

        public RentalAnnouncementDto Update(RentalAnnouncementDto rentalAnnouncementDto)
        {
            RentalAnnouncement rentalAnnouncement = _mapper.Map<RentalAnnouncementDto,RentalAnnouncement >(rentalAnnouncementDto);
            _repository.Update(rentalAnnouncement);
            _dal.Save();

            return _mapper.Map<RentalAnnouncement,RentalAnnouncementDto>(rentalAnnouncement);
        }
    }
}
