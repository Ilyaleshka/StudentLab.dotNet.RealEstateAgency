using System;
using System.Collections.Generic;

namespace RealEstateAgencyBackend.BLL.DTO
{
	public class RentalAnnouncementPageDto
	{
		public List<RentalAnnouncementDto> RentalAnnouncements { get; set; }
		public Int32 PageCount { get; set; }
		public Int32 CurrentPage { get; set; }
	}
}
