using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// What is the reason to put create helpers folder for this class?
namespace RealEstateAgencyBackend.BLL.Services.Helpers
{
	public class FiltrationService
	{
		private static Dictionary<string, FiltrationDelegate> _filtrationDelegates = new Dictionary<string, FiltrationDelegate>();

		public delegate bool FiltrationDelegate(RentalAnnouncement announcement,string param);
		/*
		 				String maxCost = HttpContext.Current.Request.QueryString["maxCost"];
				String minCost = HttpContext.Current.Request.QueryString["minCost"];
				String maxArea = HttpContext.Current.Request.QueryString["maxArea"];
				String minArea = HttpContext.Current.Request.QueryString["minArea"];
				String page = HttpContext.Current.Request.QueryString["minArea"];
				String pageSize = HttpContext.Current.Request.QueryString["pageSize"];*/

		static FiltrationService()
		{
		}

		public static Predicate<RentalAnnouncement> GetFiltrationFunction(IEnumerable<String> keys)
		{
			List<FiltrationDelegate> filtrationDelegates = new List<FiltrationDelegate>();
			foreach (var key in keys)
			{
				if (_filtrationDelegates.ContainsKey(key))
				{
					filtrationDelegates.Add(_filtrationDelegates[key]);
				}
			}
			return null;
		}


	}
}
