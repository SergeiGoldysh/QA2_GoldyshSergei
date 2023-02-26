using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA2_GoldyshSergei.Interface
{
    public interface Iaction
    {
        public void Add();
        public void Edit();
        public void Delete();
        public void ShowClientsOrders();
        public void Return();
        public void Exit();


    }
}
