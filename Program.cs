using System.Collections;
using System.IO;
using System.Text;

namespace diskBenchmark;

class Program
{
    static void Main(string[] args)
    {
        var timer = new System.Diagnostics.Stopwatch();

        var options = new Hashtable
        {
            { 1, 100 * 1024 },
            { 2, 1L * 1024 * 1024 * 1024 },
            { 3, 2L * 1024 * 1024 * 1024 },
            { 4, 3L * 1024 * 1024 * 1024 },
            { 5, 4L * 1024 * 1024 * 1024 },
            { 6, 5L * 1024 * 1024 * 1024 },
            { 7, 10L * 1024 * 1024 * 1024 },
            { 8, 50L * 1024 * 1024 * 1024 },
        };

        int input;
        bool ok = false;
        do
        {
            Console.WriteLine("Select size to benchmark:");

            for (int i = 1; i <= options.Count; i++)
            {
                var optionString = options[i]?.ToString();
                if (optionString == null) return;

                var option = long.Parse(optionString);
                var output = optionString.Length switch
                {
                    < 10 => option / 1024 + " KB",
                    >= 10 => option / 1024 / 1024 / 1024 + " GB"
                };

                Console.WriteLine("Option " + i + ": " + output);
            }

            Console.Write("Write your option: ");

            ok = int.TryParse(Console.ReadLine(), out input);
            if (!ok) Console.WriteLine("You can only input a number!\n");

        } while (!ok);

        timer.Start();

        var fileSizeInput = options[input]?.ToString();
        if (fileSizeInput == null) return;
        var fileSize = long.Parse(fileSizeInput);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "benchmark.txt");
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            fs.SetLength(fileSize);
            fs.Close();
            timer.Stop();
        }

        Console.WriteLine($"Time taken: {timer.ElapsedMilliseconds} ms");
    }
}