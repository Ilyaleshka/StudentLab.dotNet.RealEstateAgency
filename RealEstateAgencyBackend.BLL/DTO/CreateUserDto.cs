using System;

namespace RealEstateAgencyBackend.BLL.DTO
{
	public class CreateUserDto
    {
        public String UserLastName { get; set; }

        public String UserName { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }
    }
}
