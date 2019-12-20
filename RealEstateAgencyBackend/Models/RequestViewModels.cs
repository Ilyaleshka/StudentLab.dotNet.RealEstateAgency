using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAgencyBackend.Models
{
    public class RequestViewModels
    {
    }

    public class RentalRequestCreateViewModel
    {
        public String Title { get; set; }

        public String Description { get; set; }

        public Int32 Area { get; set; }

        public String PrefferedAddress { get; set; }

        public Int32 MaxPrice { get; set; }

        public String UserId { get; set; }
    }
}