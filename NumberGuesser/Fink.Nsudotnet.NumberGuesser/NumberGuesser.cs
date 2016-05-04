using System;

namespace Fink.Nsudotnet.NumberGuesser
{
    class NumberGuesser
    {
        const int Start = 0;
        const int End = 100;
        const int HistorySize = 1000;
        const char More = '>';
        const char Less = '<';
        const char Equal = '=';
        const string Exit = "q";
        static string[] Insult = {"ты неудачник" , "тебе ещё учиться и учиться" , "ну как же так", "я так долго не выдержу"};

        static void Main(string[] args)
        {

            Console.WriteLine("Привет!");
            Console.WriteLine("Cкажи, как тебя зовут?");
            string name = Console.ReadLine();
            Random random = new Random();
            int guessNumber = random.Next(End);
            Console.WriteLine("Я загадал число, попробуй угадать.");
            int receivedNumber = -1;
            int start = Start;
            int end = End;
            string str = "";
            int attempt = 0;
            DateTime date = DateTime.Now;
            int[] attemptHistory = new int[HistorySize];
            char[] charHistory = new char[HistorySize];
            while (true)
            {
                Console.WriteLine("Число от {0} до {1}", start, end);
                try
                {
                    str = Console.ReadLine();
                    receivedNumber = int.Parse(str);
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
                        charHistory[attempt] = More;
                    }
                    else if (receivedNumber > guessNumber)
                    {
                        Console.WriteLine("Я загадал число поменьше.");
                        end = receivedNumber;
                        charHistory[attempt] = Less;
                    }
                    else
                    {
                        charHistory[attempt] = Equal;
                        Console.WriteLine("Ура мы его нашли.");
                        Console.WriteLine("{0}, вы совершили {1} попыток", name, attempt + 1);
                        TimeSpan time = DateTime.Now.Subtract(date);
                        if (time.Hours != 0)
                        {
                            Console.WriteLine("Вы потратили {0} минут {1} часов", time.Minutes,time.Hours);
                        }
                        else
                        {
                            Console.WriteLine("Вы потратили {0} минут", time.Minutes);
                        }
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
                        Console.WriteLine("{0}, {1}",name, Insult[random.Next(Insult.Length)]);
                    }
                }
                catch (FormatException e)
                {
                    if (Exit == str)
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
