using System;

namespace ParagraphCount
{
    public class Program
    {
        static void Main(string[] args)
        {
            var counter = new ParagraphCounter();
            var separators = ParagraphCounter.DefaultSeparator;
            Console.WriteLine($"Default separators: [{separators}] plus empty space");
            Console.WriteLine("To use your own list, enter it below. Or press ENTER to use default.");
            Console.Write("Separators: ");
            var separatorInput = Console.ReadLine();
            if (separatorInput != string.Empty)
            {
                separators = separatorInput;
            }
            else
            {
                Console.Write($"[DEFAULT]{separators}");
            }
            string input;
            do
            {
                Console.WriteLine("Input paragraph. Enter 'EXIT' to exit.");
                Console.Write("Input: ");
                input = Console.ReadLine();
                if (input!="EXIT")
                {
                    var output = counter.GetWordCounts(input, separators.ToCharArray());
                    foreach(var pair in output)
                    {
                        Console.WriteLine($"{pair.Key}:{pair.Value}");
                    }
                }
            } while (input!="EXIT");
        }
    }
}
