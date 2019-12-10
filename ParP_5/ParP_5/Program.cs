using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ParP_5
{
    class Program
    {
        static bool flag = true;
        static string buff;
        static object locker = new object();
        static string res= @"E:\Github\ParP\ParP_5\ParP_5\Result.txt";
        static void Main(string[] args)
        {
            string keysToFind = @"E:\Github\ParP\ParP_5\ParP_5\KeysToFind.txt";

            string keys = @"E:\Github\ParP\ParP_5\ParP_5\Keys.txt";

            string result = @"E:\Github\ParP\ParP_5\ParP_5\Result.txt";

            string buff = "";

            List<string> keysarr = ReadFromFileToArr(keys).Cast<string>().ToList();

            Console.WriteLine(new string('-', 20));

            string[] keysToFindarr = ReadFromFileToArr(keysToFind);

            Console.WriteLine(new string('-', 20));

            LinearAlh(keysarr, keysToFindarr, buff, result);

            Console.WriteLine(new string('-', 20));
            Data data = new Data();
            data.keysarr = keysarr;
            data.keysToFindarr = keysToFindarr;
            data.resstr = buff;
            data.result = result;
            for (int i = 0; i < 4; i++)
            {
                Thread myThread = new Thread(new ParameterizedThreadStart(ParallelAlh));

                myThread.Start(data);
            }
            Console.ReadKey();
        }

        public static void ParallelAlh(object data)
        {
            Data dat = (Data)data;
            
            
                for (int j = 0; j < dat.keysToFindarr.Length; j++)
                {
                    if (dat.keysarr.Contains(dat.keysToFindarr[j]))
                    {
                    lock (locker)
                    {
                        buff += $"{dat.keysToFindarr[j]} is found at index {dat.keysarr.IndexOf(dat.keysToFindarr[j]) - 1}";
                        buff += Environment.NewLine;
                    }
                       // Console.WriteLine($"{dat.keysToFindarr[j]} is found at index {dat.keysarr.IndexOf(dat.keysToFindarr[j]) - 1}");
                    }
                    else
                    {
                    lock (locker)
                    {
                        buff += $"{dat.keysToFindarr[j]} is NOT FOUND";
                        buff += Environment.NewLine;
                    }
                        //Console.WriteLine($"{dat.keysToFindarr[j]} is NOT FOUND");
                    }

                }
            lock (locker)
            {
                if (flag)
                {
                    flag = false;
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(res, false, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(buff);
                        }

                        //Console.WriteLine(buff);
                        Console.WriteLine("Запись выполнена");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            
        }

        static string[] ReadFromFileToArr(string path)
        {
            string[] textarr;

            string textFromFile = File.ReadAllText(path);
            textarr = textFromFile.Split(Environment.NewLine);
            Console.WriteLine(textFromFile);


            return textarr;
        }
        public static void LinearAlh(List<string> keysarr, string[] keysToFindarr, string resstr, string result)
        {
            for (int j = 0; j < keysToFindarr.Length; j++)
            {
                if (keysarr.Contains(keysToFindarr[j]))
                {
                    resstr += $"{keysToFindarr[j]} is found at index {keysarr.IndexOf(keysToFindarr[j]) - 1}";
                    resstr += Environment.NewLine;
                    Console.WriteLine($"{keysToFindarr[j]} is found at index {keysarr.IndexOf(keysToFindarr[j]) - 1}");
                }
                else
                {
                    resstr += $"{keysToFindarr[j]} is NOT FOUND";
                    resstr += Environment.NewLine;
                    Console.WriteLine($"{keysToFindarr[j]} is NOT FOUND");
                }

            }
            try
            {
                using (StreamWriter sw = new StreamWriter(result, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(resstr);
                }


                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
