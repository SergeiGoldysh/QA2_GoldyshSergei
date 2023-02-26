using Microsoft.EntityFrameworkCore;
using QA2_GoldyshSergei.Interface;
using QA2_GoldyshSergei.Model;
using QA2_GoldyshSergei.SqlServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace QA2_GoldyshSergei.Controllers
{
    public class ActionClient : Iaction
    {
        public void Add()
        {
            Client client = new Client();
            Console.WriteLine("Введите ваше имя");
            string firstName = Console.ReadLine();
            while (string.IsNullOrEmpty(firstName) || firstName.Trim().Length == 0)
            {
                Console.WriteLine("имя не может быть пустым");
                firstName = Console.ReadLine();
            }
            client.FirstName = firstName;
            Console.WriteLine("Введите вашу фамилию");
            string secondName = Console.ReadLine();
            while (string.IsNullOrEmpty(secondName) || secondName.Trim().Length == 0)
            {
                Console.WriteLine("фамилия не может быть пустым");
                secondName = Console.ReadLine();
            }
            client.SecondName = secondName;
            Console.WriteLine("Введите ваш номер телефона");
            string phoneNum = Console.ReadLine();
            while (string.IsNullOrEmpty(phoneNum) || phoneNum.Trim().Length == 0)
            {
                Console.WriteLine("поле телефон не может быть пустым");
                phoneNum = Console.ReadLine();
            }
            client.PhoneNum = phoneNum;
            int orderAmount = 0;
            client.OrderAmount = orderAmount;
            DateTime dateAdd = DateTime.Now;
            client.DateAdd = dateAdd;

            using (var db = new AppDbContext())
            {
                db.Clients.Add(client);
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
            Console.WriteLine("Введите Id клиента");
            int enternumber = 0;
            while (!int.TryParse(Console.ReadLine(), out enternumber))
            {
                Console.WriteLine("Введите Id клиента цифрами");
            }
            using (var db = new AppDbContext())
            {
                var client = db.Clients.Where(x => x.Id == enternumber).FirstOrDefault();
                if (client != null)
                {
                    var orders = db.Orders.Count(v => v.ClientId == enternumber);
                    if (orders != 0)
                    {
                        Console.WriteLine("У клиента есть заказы. В случае удаления клиента, все его заказы будут удалены.\n" +
                            " Вы уверены что хотите удалить клиента? (Y|N)");
                        string choice = Console.ReadLine();
                        if (choice.ToLower() == "n")
                        {
                            return;
                        }
                    }

                    try
                    {
                        db.Clients.Remove(client);
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
                    Console.WriteLine("Клиент не найден");
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
                Client client = new Client();
                Console.WriteLine("Введите Id клиента");
                int enternumber = 0;
                while (!int.TryParse(Console.ReadLine(), out enternumber))
                {
                    Console.WriteLine("Введите Id клиента цифрами");
                }
                client = db.Clients.Where(x => x.Id == enternumber).FirstOrDefault();
                if (client != null)
                {
                    Console.WriteLine("Введите новое имя");
                    string firstName = Console.ReadLine();
                    while (string.IsNullOrEmpty(firstName) || firstName.Trim().Length == 0)
                    {
                        Console.WriteLine("имя не может быть пустым");
                        firstName = Console.ReadLine();
                    }
                    client.FirstName = firstName;
                    Console.WriteLine("Введите новую фамилию");
                    string secondName = Console.ReadLine();
                    while (string.IsNullOrEmpty(secondName) || secondName.Trim().Length == 0)
                    {
                        Console.WriteLine("фамилия не может быть пустым");
                        secondName = Console.ReadLine();
                    }
                    client.SecondName = secondName;
                    Console.WriteLine("Введите новый номер телефона");
                    string phoneNum = Console.ReadLine();
                    while (string.IsNullOrEmpty(phoneNum) || phoneNum.Trim().Length == 0)
                    {
                        Console.WriteLine("поле телефон не может быть пустым");
                        phoneNum = Console.ReadLine();
                    }
                    client.PhoneNum = phoneNum;

                    int orderAmount = 0;

                    client.OrderAmount = orderAmount;
                    DateTime dateAdd = DateTime.Now;
                    client.DateAdd = dateAdd;

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
                    Console.WriteLine("Клиент не найден");
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
