using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA2_GoldyshSergei.Model
{
    public class Client
    {
        [Key]
        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNum { get; set; }
        public uint OrderAmount { get; set; }
        public DateTime DateAdd { get; set; }
        public List<Order> orders { get; set; } = new();

    }
}
