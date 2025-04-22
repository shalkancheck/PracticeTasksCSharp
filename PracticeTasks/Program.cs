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
                return;
            }

            string processedString = ProcessString(input);
            Console.WriteLine($"Обработанная строка: {processedString}");

            // Статистика по символам
            string charCountInfo = GetCharacterCountInfo(processedString);
            Console.WriteLine($"Информация о том, сколько раз входил в обработанную строку каждый символ: {charCountInfo}");

            // Поиск подстроки, начинающейся и заканчивающейся на гласную
            string longestVowelSubstring = GetLongestVowelSubstring(processedString);
            Console.WriteLine($"Самая длинная подстрока начинающаяся и заканчивающаяся на гласную: {longestVowelSubstring}");

            // Выбор алгоритма сортировки
            Console.WriteLine("Выберите алгоритм сортировки (1 - Quicksort, 2 - Tree Sort):");
            string choice = Console.ReadLine();
            string sortedString;

            if (choice == "1")
            {
                sortedString = QuickSort(processedString);
            }
            else if (choice == "2")
            {
                sortedString = TreeSort(processedString);
            }
            else
            {
                Console.WriteLine("Неверный выбор, используется Quicksort по умолчанию.");
                sortedString = QuickSort(processedString);
            }

            Console.WriteLine($"Отсортированная обработанная строка: {sortedString}");
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

        static string GetInvalidCharacters(string input)
        {
            var invalidChars = input.Where(c => !char.IsLetter(c) || !char.IsLower(c))
                                   .Distinct()
                                   .ToArray();
            return invalidChars.Length > 0 ? string.Join(", ", invalidChars) : string.Empty;
        }

        static string GetCharacterCountInfo(string input)
        {
            var charCounts = input.GroupBy(c => c)
                                 .Select(g => $"{g.Key}: {g.Count()}")
                                 .ToArray();
            return string.Join(", ", charCounts);
        }

        static string GetLongestVowelSubstring(string input)
        {
            string longestSubstring = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (!vowels.Contains(input[i])) continue;

                for (int j = input.Length - 1; j >= i; j--)
                {
                    if (!vowels.Contains(input[j])) continue;

                    string substring = input.Substring(i, j - i + 1);
                    if (substring.Length > longestSubstring.Length)
                    {
                        longestSubstring = substring;
                    }
                    break;
                }
            }

            return longestSubstring.Length > 0 ? longestSubstring : "Нет подходящей подстроки";
        }

        // Quicksort
        static string QuickSort(string input)
        {
            char[] array = input.ToCharArray();
            QuickSort(array, 0, array.Length - 1);
            return new string(array);
        }

        static void QuickSort(char[] array, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(array, low, high);
                QuickSort(array, low, pi - 1);
                QuickSort(array, pi + 1, high);
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

        // Tree Sort
        static string TreeSort(string input)
        {
            var tree = new BinarySearchTree();
            foreach (char c in input)
            {
                tree.Insert(c);
            }

            // Получаем отсортированный список с учётом повторений
            var sortedList = tree.GetSortedList();
            return new string(sortedList.ToArray());
        }
    }

    class BinarySearchTree
    {
        class Node
        {
            public char Value;
            public int Count; // Для учёта повторяющихся символов
            public Node Left, Right;

            public Node(char value)
            {
                Value = value;
                Count = 1; // Начальное количество = 1
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
            root = InsertRec(root, value);
        }

        private Node InsertRec(Node root, char value)
        {
            if (root == null)
            {
                return new Node(value);
            }

            if (value == root.Value)
            {
                root.Count++; // Увеличиваем счётчик, если символ уже есть
            }
            else if (value < root.Value)
            {
                root.Left = InsertRec(root.Left, value);
            }
            else
            {
                root.Right = InsertRec(root.Right, value);
            }

            return root;
        }

        public List<char> GetSortedList()
        {
            var result = new List<char>();
            InOrderTraversalRec(root, result);
            return result;
        }

        private void InOrderTraversalRec(Node root, List<char> result)
        {
            if (root != null)
            {
                InOrderTraversalRec(root.Left, result);
                // Добавляем символ столько раз, сколько он встречается
                for (int i = 0; i < root.Count; i++)
                {
                    result.Add(root.Value);
                }
                InOrderTraversalRec(root.Right, result);
            }
        }
    }
}