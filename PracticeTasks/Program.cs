namespace PracticeTasks
{
    class Program
    {
        static readonly char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };

        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку:");
            string input = Console.ReadLine() ?? string.Empty;

            // Проверяем, что строка содержит только буквы a-z
            string invalidChars = GetInvalidCharacters(input);
            if (invalidChars.Length > 0)
            {
                Console.WriteLine($"Сообщение об ошибке с информацией: неподходящие символы: {invalidChars}");
                Console.ReadKey();
            }
            else
            {
                string processedString = ProcessString(input);
                Console.WriteLine($"Обработанная строка: {processedString}");

                // Статистика по символам
                string charCountInfo = GetCharacterCountInfo(processedString);
                Console.WriteLine($"Информация о том, сколько раз входил в обработанную строку каждый символ: {charCountInfo}");

                // Поиск подстроки, начинающейся и заканчивающейся на гласную
                string longestVowelSubstring = GetLongestVowelSubstring(processedString);
                Console.WriteLine($"Самая длинная подстрока начинающаяся и заканчивающаяся на гласную: {longestVowelSubstring}");
                Console.ReadKey();
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
            // Подсчёт символов из Задачи 3
            var charCounts = input.GroupBy(c => c)
                                 .Select(g => $"{g.Key}: {g.Count()}")
                                 .ToArray();
            return string.Join(", ", charCounts);
        }

        static string GetLongestVowelSubstring(string input)
        {
            // Находим наибольшую подстроку, начинающуюся и заканчивающуюся на гласную
            string longestSubstring = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (!vowels.Contains(input[i])) continue; // Начало должно быть гласной

                for (int j = input.Length - 1; j >= i; j--)
                {
                    if (!vowels.Contains(input[j])) continue; // Конец должен быть гласной

                    string substring = input.Substring(i, j - i + 1);
                    if (substring.Length > longestSubstring.Length)
                    {
                        longestSubstring = substring;
                    }
                    break; // Нашли гласную с конца, дальше не проверяем
                }
            }

            return longestSubstring.Length > 0 ? longestSubstring : "Нет подходящей подстроки";
        }
    }
}