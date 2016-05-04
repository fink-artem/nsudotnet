using System;
using System.IO;

namespace Fink.Nsudotnet.LinesCounter
{
    class LinesCounter
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Параметр не найден");
                Console.ReadKey();
                return;
            }
            string[] fileNamesArray = Directory.GetFiles(Directory.GetCurrentDirectory(), args[0], SearchOption.AllDirectories);
            string line;
            int answer = 0;
            int length;
            Boolean comment = false;
            Boolean sym;
            foreach (var item in fileNamesArray)
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(item))
                {
                    while (!file.EndOfStream)
                    {
                        line = file.ReadLine();
                        length  = line.Length;
                        sym = false;
                        for (int i = 0; i < length; i++)
                        {
                            if (comment)
                            {
                                if (line[i] == '*')
                                {
                                    if (i < length - 1 && line[i + 1] == '/')
                                    {
                                        comment = false;
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                if (line[i] == '/')
                                {
                                    if (i < length - 1 && line[i + 1] == '/')
                                    {
                                        break;
                                    }
                                    else if (i < length - 1 && line[i + 1] == '*')
                                    {
                                        comment = true;
                                        i++;
                                    }
                                    else
                                    {
                                        sym = true;
                                    }
                                }
                                else
                                {
                                    sym = true;
                                }
                            }
                        }
                        if (sym)
                        {
                            answer++;
                        }
                    }
                }
            }
            Console.WriteLine(answer);
            Console.ReadKey();
        }
    }
}