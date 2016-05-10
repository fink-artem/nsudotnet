
using System;
using System.IO;
using System.Security.Cryptography;

namespace Fink.Nsudotnet.Enigma
{
    class AlgorithmNotFoundException : Exception{ }


    class Enigma
    {

        static void Encrypt(String inputFileName, String algorithmName, String outputFileName)
        {
            using (FileStream inFileStream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
            {
                using (FileStream outFileStream = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter keyStream = new StreamWriter("file.key.txt"))
                    {
                        SymmetricAlgorithm algorithm = GetAlgorithm(algorithmName);
                        algorithm.GenerateKey();
                        algorithm.GenerateIV();
                        keyStream.WriteLine(Convert.ToBase64String(algorithm.Key));
                        keyStream.WriteLine(Convert.ToBase64String(algorithm.IV));
                        using (CryptoStream cryptoStream = new CryptoStream(outFileStream, algorithm.CreateEncryptor(algorithm.Key, algorithm.IV), CryptoStreamMode.Write))
                        {
                            inFileStream.CopyTo(cryptoStream);
                            cryptoStream.FlushFinalBlock();
                        }
                    }
                }
            }
        }

        static void Dencrypt(String inputFileName, String algorithmName, String keyFile, String outputFileName)
        {
            using (FileStream inFileStream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
            {   
                using (FileStream outFileStream = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamReader keyStream = new StreamReader(keyFile))
                    {
                        SymmetricAlgorithm algorithm = GetAlgorithm(algorithmName);
                        byte[] key = Convert.FromBase64String(keyStream.ReadLine());
                        byte[] IV = Convert.FromBase64String(keyStream.ReadLine());
                        using (CryptoStream cryptoStream = new CryptoStream(outFileStream, algorithm.CreateDecryptor(key, IV), CryptoStreamMode.Write))
                        {
                            inFileStream.CopyTo(cryptoStream);
                            cryptoStream.FlushFinalBlock();
                        }
                    }
                }
            }
        }

        static SymmetricAlgorithm GetAlgorithm(String algorithm)
        {
            switch (algorithm)
            {
                case "rc2":
                    return new RC2CryptoServiceProvider();
                case "aes":
                    return new AesCryptoServiceProvider();
                case "des":
                    return new DESCryptoServiceProvider();
                case "rijndael":
                    return new RijndaelManaged();
                default:
                    throw new AlgorithmNotFoundException();
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Мало параметров");
                Console.ReadKey();
                return;
            }
            try
            {
                if (args[0] == "encrypt")
                {
                    Encrypt(args[1], args[2], args[3]);
                }
                else if (args[0] == "decrypt")
                {
                    if (args.Length < 4)
                    {
                        Console.WriteLine("Мало параметров");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Dencrypt(args[1], args[2], args[3], args[4]);
                    }
                }
                else
                {
                    Console.WriteLine("Параметры некорректны");
                    Console.ReadKey();
                }
            }
            catch (AlgorithmNotFoundException e)
            {
                Console.WriteLine("Алгоритм не найден");
                Console.ReadKey();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Файл не найден");
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("Неверный формат");
                Console.ReadKey();
            }
        }
    }
}
