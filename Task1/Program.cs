using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Создать класс для моделирования счета в банке. Предусмотреть закрытые поля для номера счета,
            баланса, ФИО владельца.Класс должен быть объявлен как обобщенный. Универсальный параметр T
            должен определять тип номера счета. Разработать методы  для доступа  к данным  –  
            заполнения и  чтения.Создать несколько экземпляров класса, параметризированного различными 
            типам. Заполнить его поля и вывести информацию об экземпляре класса на печать.
            */

            Account<int> accountA = null;
            Account<string> accountB = null;

            // Обработка исключений перехватывает исключения, генерируемые методами класса Account,
            // которые возникают при попытке назначить свойствам класса недопустимые значения
            try
            {
                // Создаём экземпляр класса с числовым типом номера счёта
                accountA = new Account<int>(1023, "Петров", "Александр", "Евгеньевич", (decimal)568173.23);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! " + ex.Message);
            }

            try
            {
                // Создаём экземпляр класса с буквенным типом номера счёта
                accountB = new Account<string>("AS82-98", "Бюльбюль-оглы", "Полад");
                accountB.Balance = 1000000;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! " + ex.Message);
            }

            Console.WriteLine("\nИнформация о счёте с числовым типом номера:");
            if (accountA != null)
            {                
                accountA.PrintAccountInfo();
            }
            else
            {
                Console.WriteLine("Информация о счёте недоступна.");
            }

            Console.WriteLine("\nИнформация о счёте с буквенным типом номера:");
            if (accountB != null)
            {
                accountB.PrintAccountInfo();
            }
            else
            {
                Console.WriteLine("Информация о счёте недоступна.");
            }

            Console.ReadKey();
        }

    }
    class Account<T>
    {
        T number;
        decimal balance = 0;
        string name = "";
        string surname = "";
        string patronymic = "";

        public Account(T number, string surname, string name, string patronymic = "", decimal balanse = 0)
        {
            try // Обрабатываются исключения, которые могут быть сгенерированы методами присвоения свойств
            {
                this.Number = number;
                this.SetClientName(surname, name, patronymic);
                this.Balance = balanse;
            }
            catch (Exception ex)
            {
                // Если перехвачено исключение, оно передаётся в код, вызывавший конструктор
                throw new Exception(ex.Message);
            }
        }
        public T Number
        {
            set
            {
                if (value is string) // Если в качетсве типа счёта использована строка
                {
                    if (value.ToString().Length > 0) // Проверяется, что строка непустая 
                    {
                        this.number = value;
                    }
                    else // Если строка пустая, генеириуется исключение
                    {
                        throw new Exception("Строковый номер счёта должен состоять по крайней мере из одного знака.");
                    }
                }
                else if (value is int) // Если в качетсве типа счёта использовано целое число
                {
                    if (Convert.ToInt32(value) > 0) // Проверяется, что номер - положительное число
                    {
                        this.number = value;
                    }
                    else // Если число неположительное, генеириуется исключение
                    {
                        throw new Exception("Числовой номер счёта должен представлять собой положительное целое число.");
                    }
                }
                else // Если тип счёта не целое число и не строка, присвоение значения не происходит, генеириуется исключение
                {
                    throw new Exception("Поддерживаются только целочисленный и строковый номера счёта.");
                }
            }
            get
            {
                {
                    return number;
                }
            }
        }
        public decimal Balance
        {
            set
            {
                balance = value;
            }
            get
            {
                return balance;
            }
        }
        public void SetClientName(string surname, string name, string patronymic = "")
        {
            // Фамилия и имя клиента считаются валидными, если содержат хотя бы по одной букве.
            // Отчество может быть пустым.
            if (surname.Length > 0 && name.Length > 0)
            {
                this.surname = surname;
                this.name = name;
                this.patronymic = patronymic;
            }
            else // в противном случае значение не присваивается полям, генеириуется исключение
            {
                throw new Exception("Фамилия и имя клиента должны состоять по крайней мере из одной буквы.");
            }
        }
        public string GetClientName()
        {
            // Метод возвращает полное имя клиента в виде строки, содержащей фамилию, имя и (при наличии)
            // отчество, разделённые пробелами.
            // Если фамилия или имя имеют пустые (невалидные) значения, то вместо полного имени
            // метод возвращает null
            string fullName = "";
            if (this.surname.Length > 0 && this.name.Length > 0)
            {
                fullName = this.surname + " " + this.name;
                if (this.patronymic.Length > 0)
                {
                    fullName += " " + this.patronymic;
                }
                return fullName;
            }
            else
            {
                return null;
            }
        }
        public void PrintAccountInfo()
        {
            Console.WriteLine("Номер счёта: {0}", this.Number);
            Console.WriteLine("Владелец: {0}", this.GetClientName());
            Console.WriteLine("Баланс: {0}", this.Balance);
        }
    }
}