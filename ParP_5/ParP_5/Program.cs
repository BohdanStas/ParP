using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParP_5
{
    class Program
    {
        static void Main(string[] args)
        {
            string keysToFind = @"E:\Github\ParP\ParP_5\ParP_5\KeysToFind.txt";
            string keys = @"E:\Github\ParP\ParP_5\ParP_5\Keys.txt";
            string result = @"E:\Github\ParP\ParP_5\ParP_5\Result.txt";
            string resstr = "";
            List<string> keysarr = ReadFromFileToArr(keys).Cast<string>().ToList(); 
            Console.WriteLine(new string('-', 20));
            string[] keysToFindarr = ReadFromFileToArr(keysToFind); 
            Console.WriteLine(new string('-', 20));

           
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
            Console.ReadKey();

        }
        static string[] ReadFromFileToArr(string path)
        {
            string[] textarr;
          
                string textFromFile =  File.ReadAllText(path);
                textarr = textFromFile.Split(Environment.NewLine);       
                Console.WriteLine(textFromFile);
        
            
            return textarr;
        }
    }
}
