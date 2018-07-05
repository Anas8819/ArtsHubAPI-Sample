using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtsHubAPI.Models
{
    public class ConversionModel
    {
            public int UserId { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string UserImage { get; set; }

            public virtual tbl_User tbl_User { get; set; }
    }
}