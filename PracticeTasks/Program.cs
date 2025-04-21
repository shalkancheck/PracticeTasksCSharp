namespace PracticeTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку:");
            string input = Console.ReadLine() ?? string.Empty;

            string result = ProcessString(input);
            Console.WriteLine($"Обработанная строка: {result}");
        }

        static string ProcessString(string input)
        {
            if (input.Length % 2 == 0)
            {
                int halfLength = input.Length / 2;
                string firstHalf = input.Substring(0, halfLength);
                string secondHalf = input.Substring(halfLength);

                char[] firstArray = firstHalf.ToCharArray();
                Array.Reverse(firstArray);
                char[] secondArray = secondHalf.ToCharArray();
                Array.Reverse(secondArray);

                return new string(firstArray) + new string(secondArray);
            }
            else
            {
                char[] array = input.ToCharArray();
                Array.Reverse(array);
                return new string(array) + input;
            }
        }
    }
}