using Microsoft.EntityFrameworkCore;
using QA2_GoldyshSergei.SqlServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA2_GoldyshSergei.Controllers
{
    public class Menu
    {
        public void GetMenu()
        {
            bool IsEnterIncorrect = true;
            while (IsEnterIncorrect)
            {
                Console.WriteLine("Выберите таблицу которую хотите посмотреть\n" +
                    "Введите 1 - если таблица клиентов\n" +
                    "Введите 2 - если таблица заказов\n" +
                    "Введите 0 - для выхода");
                if (int.TryParse(Console.ReadLine(), out int enternumber))
                {


                    if (enternumber == 1)
                    {
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Вы находитесь в таблице клиентов\n" +
                            "введите цифру которая соответствует действию:\n" +
                            "1 - Добавить\n" +
                            "2 - Редактировать\n" +
                            "3 - Удалить\n" +
                            "4 - Показать заказы клиента\n" +
                            "5 - Вернуться к выбору\n" +
                            "6 - Выйти");

                        int number = 0;
                        while (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("Введите цифру");
                        }

                        ActionClient actionClient = new ActionClient();
                        switch (number)
                        {
                            case 1:
                                actionClient.Add();
                                break;
                            case 2:
                                actionClient.Edit();
                                break;
                            case 3:
                                actionClient.Delete();
                                break;
                            case 4:
                                actionClient.ShowClientsOrders();
                                break;
                            case 5:
                                actionClient.Return();
                                break;
                            case 6:
                                actionClient.Exit();
                                break;

                        }
                        IsEnterIncorrect = false;
                    }
                    else if (enternumber == 2)
                    {
                        using (var db = new AppDbContext())
                        {
                            var orders = db.Orders.Include(c => c.Clients);
                            Console.WriteLine("Все заказы:");

                            foreach (var order in orders)
                            {
                                Console.WriteLine($"Id заказа: {order.Id} Описание: {order.Description} Цена: {order.OrderPrice} " +
                                    $"| Id клиента: {order.ClientId} Имя: {order.Clients.FirstName} Фамилия: {order.Clients.SecondName} ");
                            }
                        }
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Вы находитесь в таблице заказов\n" +
                            "введите цифру которая соответствует действию:\n" +
                            "1 - Добавить\n" +
                            "2 - Редактировать\n" +
                            "3 - Удалить\n" +
                            "4 - Показать заказы клиента\n" +
                            "5 - Вернуться к выбору\n" +
                            "6 - Выйти");

                        int number = 0;
                        while (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("Введите цифру");
                        }
                        ActionOrder actionOrder = new ActionOrder();
                        switch (number)
                        {
                            case 1:
                                actionOrder.Add();
                                break;
                            case 2:
                                actionOrder.Edit();
                                break;
                            case 3:
                                actionOrder.Delete();
                                break;
                            case 4:
                                actionOrder.ShowClientsOrders();
                                break;
                            case 5:
                                actionOrder.Return();
                                break;
                            case 6:
                                actionOrder.Exit();
                                break;

                        }
                        IsEnterIncorrect = false;

                    }
                    else if (enternumber == 0)
                    {
                        Process.GetCurrentProcess().Kill();
                    }

                }
            }


        }
    }
}
