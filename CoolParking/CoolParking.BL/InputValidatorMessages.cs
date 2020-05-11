using System;

namespace CoolParking.BL
{
    class InputValidatorMessages
    {
        private InputValidatorMessages() { }

        public static InputValidatorMessages CreateInstance() => new InputValidatorMessages();

        public void IsNotNumber()
        {
            Console.WriteLine("Ошибка! Вы ввели не число!");
        }

        public void IsOutFromMenuNumsRange()
        {
            Console.WriteLine("Введите число от 1 до 8!");
        }
    }
}
