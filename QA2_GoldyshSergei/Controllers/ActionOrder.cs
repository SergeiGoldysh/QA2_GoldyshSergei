using QA2_GoldyshSergei.Interface;
using QA2_GoldyshSergei.Model;
using QA2_GoldyshSergei.SqlServer;
using System.Diagnostics;


namespace QA2_GoldyshSergei.Controllers
{
    public class ActionOrder : Iaction
    {
        public void Add()
        {
            using (var db = new AppDbContext())
            {
                var orders = new Order();
                DateTime dateAdd = DateTime.Now;
                orders.OrderDate = dateAdd;
                Console.WriteLine("Введите Id клиента");
                int clientID = 0;
                while (!int.TryParse(Console.ReadLine(), out clientID))
                {
                    Console.WriteLine("Введите Id клиента цифрами");
                }
                var client = db.Clients.FirstOrDefault(x => x.Id == clientID);
                if (client == null)
                {
                    Console.WriteLine("Клиент не найден");
                    return;
                }
                orders.Clients = client;
                orders.ClientId = clientID;
                Console.WriteLine("Введите описание заказа");
                string description = Console.ReadLine();
                while (string.IsNullOrEmpty(description) || description.Trim().Length == 0)
                {
                    Console.WriteLine("описание заказа не может быть пустым");
                    description = Console.ReadLine();
                }
                orders.Description = description;
                Console.WriteLine("Введите цену заказа");
                float orderPrice = 0;
                while (!float.TryParse(Console.ReadLine(), out orderPrice))
                {
                    Console.WriteLine("Введите цену заказа цифрами");
                }
                orders.OrderPrice = orderPrice;
                client.OrderAmount = client.OrderAmount + 1;
                db.Orders.Add(orders);

                try
                {
                    db.SaveChanges();
                    Console.WriteLine("Данные записаны в базу данных");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Выйти в главное меню? (Y|N)");
            string getmenu = Console.ReadLine();
            if (getmenu.ToLower() == "y")
            {
                Return();
            }
        }

        public void Delete()
        {
            Console.WriteLine("Введите Id заказа");
            int enternumber = 0;
            while (!int.TryParse(Console.ReadLine(), out enternumber))
            {
                Console.WriteLine("Введите Id заказа цифрами");
            }
            using (var db = new AppDbContext())
            {
                var order = db.Orders.Where(x => x.Id == enternumber).FirstOrDefault();
                if (order != null)
                {
                    try
                    {
                        var client = db.Clients.FirstOrDefault(x => x.Id == order.ClientId);
                        client.OrderAmount = client.OrderAmount - 1;
                        db.Orders.Remove(order);
                        db.SaveChanges();
                        Console.WriteLine("Данные успешно удалены");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Заказ не найден");
                }
            }
            Console.WriteLine("Выйти в главное меню? (Y|N)");
            string getmenu = Console.ReadLine();
            if (getmenu.ToLower() == "y")
            {
                Return();
            }
        }

        public void Edit()
        {


            using (var db = new AppDbContext())
            {
                Order orders = new Order();
                Console.WriteLine("Введите Id заказа");
                int enternumber = 0;
                while (!int.TryParse(Console.ReadLine(), out enternumber))
                {
                    Console.WriteLine("Введите Id заказа цифрами");
                }
                orders = db.Orders.Where(x => x.Id == enternumber).FirstOrDefault();
                if (orders != null)
                {
                    DateTime dateAdd = DateTime.Now;
                    orders.OrderDate = dateAdd;
                    Console.WriteLine("Введите Id клиента которому добавите этот заказ");
                    int clientID = 0;
                    while (!int.TryParse(Console.ReadLine(), out clientID))
                    {
                        Console.WriteLine("Введите Id клиента которому добавите этот заказ ЦИФРАМИ");
                    }

                    var kl = db.Clients.Where(x => x.Id == clientID).FirstOrDefault();

                    if (kl == null)
                    {
                        Console.WriteLine("Клиент не найден");
                        return;
                    }
                    orders.Clients = kl;
                    orders.ClientId = clientID;
                    Console.WriteLine("Введите новое описание заказа");
                    string description = Console.ReadLine();
                    while (string.IsNullOrEmpty(description) || description.Trim().Length == 0)
                    {
                        Console.WriteLine("описание заказа не может быть пустым");
                        description = Console.ReadLine();
                    }
                    orders.Description = description;

                    Console.WriteLine("Введите новую цену заказа");
                    float orderPrice = 0;
                    while (!float.TryParse(Console.ReadLine(), out orderPrice))
                    {
                        Console.WriteLine("Введите новую цену заказа цифрами");
                    }
                    orders.OrderPrice = orderPrice;

                    Console.WriteLine("Заказ выполнен? (Y|N)");
                    string choice = Console.ReadLine();
                    if (choice.ToLower() == "y")
                    {
                        orders.CloseDate = DateTime.Now;
                    }

                    try
                    {
                        db.SaveChanges();
                        Console.WriteLine("Данные записаны в базу данных");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Заказ не найден");
                }
            }
            Console.WriteLine("Выйти в главное меню? (Y|N)");
            string getmenu = Console.ReadLine();
            if (getmenu.ToLower() == "y")
            {
                Return();
            }
        }

        public void Exit()
        {
            Process.GetCurrentProcess().Kill();
        }

        public void Return()
        {
            Console.Clear();
            Menu menu = new Menu();
            menu.GetMenu();
        }

        public void ShowClientsOrders()
        {
            using (var db = new AppDbContext())
            {
                Console.WriteLine("Список клиентов");
                var clients = db.Clients.ToList();
                foreach (var client in clients)
                {
                    Console.WriteLine($"{client.Id} {client.FirstName} {client.SecondName}");
                }

                Console.WriteLine("Введите Id клиента заказы которого хотите посмотреть");
                int numberId = 0;
                while (!int.TryParse(Console.ReadLine(), out numberId))
                {
                    Console.WriteLine("Введите Id клиента заказы которого хотите посмотреть цифрами");
                }
                var clientAndOrders = db.Clients.Where(x => x.Id == numberId).FirstOrDefault();
                if (clientAndOrders == null)
                {
                    Console.WriteLine("Клиент не найден");
                    return;
                }
                Console.WriteLine($"Имя и фамилия: {clientAndOrders.FirstName} {clientAndOrders.SecondName}");

                var OrdersCl = db.Orders.Where(c => c.ClientId == numberId).ToList();

                if (OrdersCl.Count() == 0)
                {
                    Console.WriteLine("У данного клиента нет заказов");
                    return;
                }
                int count = 1;
                foreach (var order in OrdersCl)
                {
                    Console.WriteLine($"{count}) Описание: {order.Description} цена: {order.OrderPrice} дата и время заказа: {order.OrderDate.ToString("dd.MM.yyyy HH:mm:ss")}");
                    count++;
                }
            }
            Console.WriteLine("Выйти в главное меню? (Y|N)");
            string getmenu = Console.ReadLine();
            if (getmenu.ToLower() == "y")
            {
                Return();
            }
        }
    }
}

