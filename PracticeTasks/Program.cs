namespace PracticeTasks
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите строку: ");
            string userInput = Console.ReadLine();
            Console.WriteLine("Выберите алгоритм сортировки:");
            Console.WriteLine("1. Быстрая сортировка (Quicksort)");
            Console.WriteLine("2. Сортировка деревом (Tree sort)");
            int sortChoice;
            while (!int.TryParse(Console.ReadLine(), out sortChoice) || (sortChoice != 1 && sortChoice != 2))
            {
                Console.WriteLine("Пожалуйста, выберите 1 или 2:");
            }

            var result = ProcessString(userInput, sortChoice);
            if (result.error != null)
            {
                Console.WriteLine(result.error);
            }
            else
            {
                Console.WriteLine($"1. Обработанная строка: {result.processedString}");
                Console.WriteLine("2. Количество символов:");
                foreach (var pair in result.charCount.OrderBy(p => p.Key))
                {
                    Console.WriteLine($"   '{pair.Key}': {pair.Value}");
                }
                Console.WriteLine($"3. Самая длинная подстрока между гласными: {result.longestVowelSubstring}");
                Console.WriteLine($"4. Отсортированная строка ({result.sortAlgorithm}): {result.sortedString}");
            }
        }

        static (string processedString,
                Dictionary<char, int> charCount,
                string longestVowelSubstring,
                string sortedString,
                string sortAlgorithm,
                string error)
            ProcessString(string input, int sortChoice)
        {
            // Проверка на допустимые символы (Задание 2)
            var invalidChars = new HashSet<char>();
            foreach (char c in input)
            {
                if (c < 'a' || c > 'z')
                {
                    invalidChars.Add(c);
                }
            }
            if (invalidChars.Count > 0)
            {
                string invalidCharsStr = string.Join(", ", invalidChars.OrderBy(c => c));
                return (null, null, null, null, null, $"Ошибка: в строке найдены неподходящие символы - {invalidCharsStr}");
            }

            // Обработка строки (Задание 1)
            string processedString;
            if (input.Length % 2 == 0)
            {
                int halfLength = input.Length / 2;
                string firstHalf = new string(input.Substring(0, halfLength).Reverse().ToArray());
                string secondHalf = new string(input.Substring(halfLength).Reverse().ToArray());
                processedString = firstHalf + secondHalf;
            }
            else
            {
                string reversed = new string(input.Reverse().ToArray());
                processedString = reversed + input;
            }

            // Подсчёт символов (Задание 3)
            var charFrequencies = new Dictionary<char, int>();
            foreach (char c in processedString)
            {
                charFrequencies[c] = charFrequencies.ContainsKey(c) ? charFrequencies[c] + 1 : 1;
            }

            // Поиск самой длинной подстроки между гласными (Задание 4)
            string vowelSubstring = FindLongestVowelSubstring(processedString);

            // Сортировка строки (Задание 5)
            string sortedResult;
            string algorithmName;
            if (sortChoice == 1)
            {
                sortedResult = QuickSort(processedString);
                algorithmName = "Quicksort";
            }
            else
            {
                sortedResult = TreeSort(processedString);
                algorithmName = "Tree sort";
            }

            return (processedString, charFrequencies, vowelSubstring, sortedResult, algorithmName, null);
        }

        static string FindLongestVowelSubstring(string input)
        {
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'y' };
            string longestMatch = string.Empty;

            for (int start = 0; start < input.Length; start++)
            {
                if (!vowels.Contains(input[start])) continue;

                for (int end = input.Length - 1; end >= start; end--)
                {
                    if (!vowels.Contains(input[end])) continue;

                    string currentSubstring = input.Substring(start, end - start + 1);
                    if (currentSubstring.Length > longestMatch.Length)
                    {
                        longestMatch = currentSubstring;
                    }
                    break;
                }
            }

            return longestMatch.Length > 0 ? longestMatch : "Не найдено";
        }

        // Быстрая сортировка (Quicksort)
        static string QuickSort(string input)
        {
            char[] charArray = input.ToCharArray();
            QuickSortImplementation(charArray, 0, charArray.Length - 1);
            return new string(charArray);
        }

        static void QuickSortImplementation(char[] array, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(array, low, high);
                QuickSortImplementation(array, low, pivotIndex - 1);
                QuickSortImplementation(array, pivotIndex + 1, high);
            }
        }

        static int Partition(char[] array, int low, int high)
        {
            char pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }

            (array[i + 1], array[high]) = (array[high], array[i + 1]);
            return i + 1;
        }

        // Сортировка деревом (Tree Sort)
        static string TreeSort(string input)
        {
            var bst = new BinarySearchTree();
            foreach (char c in input)
            {
                bst.Insert(c);
            }

            var sortedList = bst.GetSortedCharacters();
            return new string(sortedList.ToArray());
        }
    }

    class BinarySearchTree
    {
        class Node
        {
            public char Value;
            public int Count;  // Для учёта повторений
            public Node Left, Right;

            public Node(char value)
            {
                Value = value;
                Count = 1;
                Left = Right = null;
            }
        }

        private Node root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void Insert(char value)
        {
            root = InsertRecursive(root, value);
        }

        private Node InsertRecursive(Node node, char value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            if (value == node.Value)
            {
                node.Count++;
            }
            else if (value < node.Value)
            {
                node.Left = InsertRecursive(node.Left, value);
            }
            else
            {
                node.Right = InsertRecursive(node.Right, value);
            }

            return node;
        }

        public List<char> GetSortedCharacters()
        {
            var result = new List<char>();
            InOrderTraversal(root, result);
            return result;
        }

        private void InOrderTraversal(Node node, List<char> result)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, result);
                for (int i = 0; i < node.Count; i++)
                {
                    result.Add(node.Value);
                }
                InOrderTraversal(node.Right, result);
            }
        }
    }
}