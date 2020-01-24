using RealEstateAgencyBackend.BLL.Interfaces;
using RealEstateAgencyBackend.Models;
using RealEstateAgencyBackend.DAL.Repositories;
using RealEstateAgencyBackend.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateAgencyBackend.BLL.DTO;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Specialized;

namespace RealEstateAgencyBackend.BLL.Services
{
    public class RentalRequestService : IRentalRequestService
    {
        private IUnitOfWork _dal;
        private IRentalRequestRepository _repository;
        private IMapper _mapper;

        private UserManager<User> _userManager;

        public RentalRequestService(IUnitOfWork dal, IMapper mapper)
        {
            _dal = dal;
            _repository = this._dal.RentalRequestRepository;
            _mapper = mapper;
            _userManager = new UserManager<User>(_dal.UserRepository);
        }

        public RentalRequestDto Create(RentalRequestDto rentalRequestDto)
        {
            RentalRequest rentalRequest = _mapper.Map<RentalRequest>(rentalRequestDto);

            User user = _userManager.FindById(rentalRequestDto.UserId);
            rentalRequest.User = user;

            _repository.Create(rentalRequest);
            _dal.Save();

            return _mapper.Map<RentalRequestDto>(rentalRequest);
        }

        public RentalRequestDto Find(int id)
        {
            RentalRequest rentalRequest = _repository.Find(id);
            RentalRequestDto rentalRequestDto = _mapper.Map<RentalRequestDto>(rentalRequest);

            return rentalRequestDto;
        }

        public IEnumerable<RentalRequestDto> GetAll()
        {
            List<RentalRequestDto> rentalRequestDtos = _mapper.Map<IEnumerable<RentalRequest>, List<RentalRequestDto>>(_repository.GetAll());

            return rentalRequestDtos;
        }

		public RentalRequestPageDto GetPageWithFilters(Int32 pageNumber, Int32 pageSize, NameValueCollection filteringParams)
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

			IQueryable<RentalRequest> requests = _repository.GetAll();

			requests = requests.Where(request =>
						(request.MaxPrice >= minCost && request.MaxPrice <= maxCost)
					&& (request.Area >= minArea && request.Area <= maxArea));

			int skip = (pageNumber - 1) * pageSize;
			Int32 requestCount = (int)Math.Ceiling(requests.Count() / (float)pageSize);
			requests = requests.OrderByDescending(g => g.Id).Skip(skip).Take(pageSize);


			List<RentalRequestDto> rentalRequestDtos = _mapper.Map<IEnumerable<RentalRequest>, List<RentalRequestDto>>(requests);

			RentalRequestPageDto rentalRequestPageDto = new RentalRequestPageDto
			{
				CurrentPage = pageNumber,
				PageCount = requestCount,
				RentalRequests = rentalRequestDtos
			};

			return rentalRequestPageDto;
		}

		public RentalRequestDto Remove(RentalRequestDto rentalRequest)
        {
            var temp = _repository.Remove(rentalRequest.Id);
            _dal.Save();

            RentalRequestDto rentalRequestDto = _mapper.Map<RentalRequestDto>(temp);

            return rentalRequestDto;
        }

        public RentalRequestDto Update(RentalRequestDto rentalRequestDto)
        {
            RentalRequest rentalRequest = _mapper.Map<RentalRequest>(rentalRequestDto);
            _repository.Update(rentalRequest);
            _dal.Save();

            return _mapper.Map<RentalRequestDto>(rentalRequest);
        }

    }
}
