using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Core.Models
{
    public class ApprovalRequest
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDetails Product { get; set; }
        public string RequestType { get; set; }
        public string RequestReason { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
