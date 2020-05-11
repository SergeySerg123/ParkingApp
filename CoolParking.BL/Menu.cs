using System;
using System.Collections.Generic;
using System.Text;

namespace CoolParking.BL
{
    class Menu
    {
        private readonly InputValidatorMessages ivm = null;
        private Menu(InputValidatorMessages ivm) {
            ivm = InputValidatorMessages.CreateInstance();
        }

        public static Menu CreateInstance(InputValidatorMessages ivm) => new Menu(ivm);

        public void ShowMenu()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Выберите нужную команду:");
            Console.WriteLine("1 - запустить паркинг");
            Console.WriteLine("2 - остановить работу паркинга");
            Console.WriteLine("3 - добавить транспортное средство (генерируется автоматически)");
            Console.WriteLine("4 - баланс паркинга");
            Console.WriteLine("5 - колличество свободных мест в паркинге");
            Console.WriteLine("6 - список транспортных средств в паркинге");
            Console.WriteLine("7 - пополнить баланс транспортного средства");
            Console.WriteLine("======================================");
        }

        public Tuple<bool, int> Select(string value)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(value);
            }
            catch (Exception e)
            {
                ivm.IsNotNumber();
            }

            if(!(num >= 1 && num <= 7))
            {
                ivm.IsOutFromMenuNumsRange();
                return new Tuple<bool, int>(true, num);
            }

            return new Tuple<bool, int>(false, num);
        }

    }
}
