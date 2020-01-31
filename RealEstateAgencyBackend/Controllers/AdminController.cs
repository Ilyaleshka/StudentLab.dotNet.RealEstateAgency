
using RealEstateAgencyBackend.Models;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using RealEstateAgencyBackend.BLL;
using RealEstateAgencyBackend.BLL.Interfaces;
using AutoMapper;

namespace RealEstateAgencyBackend.Controllers
{

    public class AdminController : System.Web.Http.ApiController
    {
		//[Authorize]
		private IUserService _userService;
		private IMapper _mapper;

		public AdminController(IUserService service, IMapper mapper)
		{
			_userService = service;
			_mapper = mapper;
		}

		//It's only for debug, dont pay attention
		public IQueryable<UserCreateModel> GetUsers()
        {
			
			var users= _userService.GetAll();
			List<UserCreateModel> userModels = new List<UserCreateModel>();
            foreach (var user in users)
            {
                userModels.Add(new UserCreateModel() {Name = user.UserName, Email = user.Email, LastName = user
                .UserLastName});
            }
            return userModels.AsQueryable();
            
        }

    }
}
