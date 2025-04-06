using System.Text.RegularExpressions;
using System.Threading;

namespace FileReader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //StreamWriter writer = new StreamWriter(@"C:\C#\FileReader\TextFile1.txt", true);

            //for (int i = 0; i <= 2000; i++)
            //{
            //    writer.WriteLine($"{i}) {Guid.NewGuid().ToString()}");
            //}

            await foreach (var batch in Reader(@"C:\C#\FileReader\TextFile1.txt", 400))
            {
                Console.WriteLine($"Записано {batch.Count} строк");
                
                foreach(var str in batch)
                {
                    Console.WriteLine(str);
                }
            }
        }


        static async IAsyncEnumerable<List<string>> Reader(string filePath, int size = 400)
        {
            using var reader = new StreamReader(filePath);

            List<string> batch = new(size);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line != null)
                    batch.Add(line);

                if (batch.Count >= size)
                {
                    yield return batch;
                    batch = new(size);
                }
            }

            if (batch.Count > 0)
            {
                yield return batch;
            }
        }
    }
}
