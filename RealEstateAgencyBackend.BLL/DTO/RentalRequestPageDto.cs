using System;
using System.Collections.Generic;

namespace RealEstateAgencyBackend.BLL.DTO
{
	public class RentalRequestPageDto
	{
		public List<RentalRequestDto> RentalRequests { get; set; }
		public Int32 PageCount { get; set; }
		public Int32 CurrentPage { get; set; }
	}
}
