using GiftShopCORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace GiftShopCORE.Entities
{
    public class Comment:BaseEntity
    {
        public int ProductId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public Product Product { get; set; }
    }
}
