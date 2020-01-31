using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAgencyBackend.Models
{
	public class RentalAnnouncementPageViewModel
	{
		public List<RentalAnnouncementViewModel> RentalAnnouncements { get; set; }
		public Int32 PageCount { get; set; }
		public Int32 CurrentPage { get; set; }
	}
}