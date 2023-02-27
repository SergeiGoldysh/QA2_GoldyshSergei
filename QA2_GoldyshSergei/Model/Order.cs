using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA2_GoldyshSergei.Model
{
    public class Order
    {
        [Key]
        public uint Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public float OrderPrice { get; set; }
        public DateTime? CloseDate { get; set; }

        [ForeignKey("Clients")]
        public uint ClientId { get; set; }
        public Client Clients { get; set; }

    }
}
