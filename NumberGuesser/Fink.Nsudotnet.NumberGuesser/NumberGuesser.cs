using System;

namespace Fink.Nsudotnet.NumberGuesser
{
    class NumberGuesser
    {
        const int START = 0;
        const int END = 100;
        const int HISTORY_SIZE = 1000;
        const char MORE = '>';
        const char LESS = '<';
        const char EQUAL = '=';
        const string EXIT = "q";
        static string[] INSULT = {"ты неудачник" , "тебе ещё учиться и учиться" , "ну как же так", "я так долго не выдержу"};

        static void Main(string[] args)
        {

            Console.WriteLine("Привет!");
            Console.WriteLine("Cкажи, как тебя зовут?");
            string name = Console.ReadLine();
            Random random = new Random();
            int guessNumber = random.Next(END);
            Console.WriteLine("Я загадал число, попробуй угадать.");
            int receivedNumber = -1;
            int start = START;
            int end = END;
            string str = "";
            int attempt = 0;
            DateTime date = DateTime.Now;
            int[] attemptHistory = new int[HISTORY_SIZE];
            char[] charHistory = new char[HISTORY_SIZE];
            while (true)
            {
                Console.WriteLine("Число от {0} до {1}", start, end);
                try
                {
                    str = Console.ReadLine();
                    receivedNumber = Int32.Parse(str);
                    if (receivedNumber < start || receivedNumber >= end)
                    {
                        Console.WriteLine("Давай попробуем ещё раз.");
                        continue;
                    }
                    attemptHistory[attempt] = receivedNumber;
                    if (receivedNumber < guessNumber)
                    {
                        Console.WriteLine("Я загадал число побольше.");
                        start = receivedNumber;
                        charHistory[attempt] = MORE;
                    }
                    else if (receivedNumber > guessNumber)
                    {
                        Console.WriteLine("Я загадал число поменьше.");
                        end = receivedNumber;
                        charHistory[attempt] = LESS;
                    }
                    else
                    {
                        charHistory[attempt] = EQUAL;
                        Console.WriteLine("Ура мы его нашли.");
                        Console.WriteLine("{0}, вы совершили {1} попыток", name, attempt + 1);
                        Console.WriteLine("Вы потратили {0} минут", (DateTime.Now.Minute - date.Minute));
                        Console.WriteLine("Ваша история:");
                        for (int i = 0; i <= attempt; i++)
                        {
                            Console.WriteLine("Попытка {0}: {1}, надо {2}", i+1, attemptHistory[i], charHistory[i]);
                        }
                        break;
                    }
                    attempt++;
                    if (attempt % 4 == 0)
                    {
                        Console.WriteLine("{0}, {1}",name, INSULT[random.Next(INSULT.Length)]);
                    }
                }
                catch (FormatException e)
                {
                    if (EXIT == str)
                    {
                        Console.WriteLine("Извините.");
                        break;
                    }
                    Console.WriteLine("Должно быть введено число.");
                }
                catch (OverflowException e)
                {
                    Console.WriteLine("Ну это явно не то. Давай попробуем ещё раз.");
                }
            }
            Console.ReadKey();
        }
    }
}
