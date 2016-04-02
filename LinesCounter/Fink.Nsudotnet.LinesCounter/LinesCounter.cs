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
            Boolean commentStarted = false;
            int firstCommentPosition;
            int secondCommentPosition;
            int quotePosition;
            int counter = 0;
            foreach (var item in fileNamesArray)
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(item))
                {
                    while (!file.EndOfStream)
                    {
                        line = file.ReadLine().Trim();
                        quotePosition = line.IndexOf("\"");
                        if (quotePosition != -1)
                        {
                            line = line.Remove(quotePosition + 1, line.LastIndexOf("\"") - quotePosition - 1);
                        }
                        firstCommentPosition = line.IndexOf("//");
                        if (firstCommentPosition != -1)
                        {
                            line = line.Remove(firstCommentPosition);
                        }
                        while (true)
                        {
                            if (commentStarted)
                            {
                                secondCommentPosition = line.IndexOf("*/");
                                if (secondCommentPosition != -1)
                                {
                                    commentStarted = false;
                                    firstCommentPosition = line.IndexOf("/*");
                                    if (firstCommentPosition != -1)
                                    {
                                        line = line.Remove(firstCommentPosition, secondCommentPosition - firstCommentPosition + 2);
                                    }
                                    else
                                    {
                                        line = line.Remove(0, secondCommentPosition + 2);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                secondCommentPosition = line.IndexOf("/*");
                                if (secondCommentPosition != -1)
                                {
                                    commentStarted = true;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        if (line.Length != 0 && !commentStarted)
                        {
                            counter++;
                        }
                    }
                }
            }
            Console.WriteLine(counter);
            Console.ReadKey();
        }
    }
}