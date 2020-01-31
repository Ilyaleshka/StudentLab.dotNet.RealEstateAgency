namespace RealEstateAgencyBackend.Models
{
	public class ReservationViewModel
	{
		public int Id { get; set; }

		public bool IsConfirmed { get; set; }

		public bool IsRejected { get; set; }

		public bool IsActive { get; set; }

		public int RentalAnnouncementId { get; set; }

		public string UserId { get; set; }
	}
}