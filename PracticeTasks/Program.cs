namespace PracticeTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку:");
            string input = Console.ReadLine() ?? string.Empty;

            // Проверяем, что строка содержит только буквы a-z
            string invalidChars = GetInvalidCharacters(input);
            if (invalidChars.Length > 0)
            {
                Console.WriteLine($"Сообщение об ошибке с информацией: неподходящие символы: {invalidChars}");
            }
            else
            {
                string processedString = ProcessString(input);
                Console.WriteLine($"Обработанная строка: {processedString}");

                // Выводим статистику по символам
                string charCountInfo = GetCharacterCountInfo(processedString);
                Console.WriteLine($"Информация о том, сколько раз входил в обработанную строку каждый символ: {charCountInfo}");
            }
        }

        static string ProcessString(string input)
        {
            // Логика из Задачи 1: обработка строки
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

        static string GetInvalidCharacters(string input)
        {
            // Проверка из Задачи 2: только a-z
            var invalidChars = input.Where(c => !char.IsLetter(c) || !char.IsLower(c))
                                   .Distinct()
                                   .ToArray();
            return invalidChars.Length > 0 ? string.Join(", ", invalidChars) : string.Empty;
        }

        static string GetCharacterCountInfo(string input)
        {
            // Подсчитываем количество каждого символа в обработанной строке
            var charCounts = input.GroupBy(c => c)
                                 .Select(g => $"{g.Key}: {g.Count()}")
                                 .ToArray();
            return string.Join(", ", charCounts);
        }
    }
}