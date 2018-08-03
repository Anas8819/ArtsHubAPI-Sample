using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtsHubAPI.Models
{
    public class UserConversionModel
    {
            public int UserId { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string UserImage { get; set; }

            public virtual tbl_User tbl_User { get; set; }
    }

    public class ItemConversionModel
    {
        public int ItemId { get; set; }
        public int ItemDetailId { get; set; }
        public string ItemImage { get; set; }

        public virtual tbl_Item tbl_Item { get; set; }
    }

    public class AuctionConversionModel
    {
        public int AuctionId { get; set; }
        public int AuctionDetailId { get; set; }
        public string AuctionImage { get; set; }

        public virtual tbl_Auction tbl_Auction { get; set; }
    }
    public class ArtistPostConversionModel
    {
        public int PostId { get; set; }
        public int ArtistPostDetailId { get; set; }
        public string PostImage { get; set; }

        public virtual tbl_ArtistPost tbl_ArtistPost { get; set; }
    }
}