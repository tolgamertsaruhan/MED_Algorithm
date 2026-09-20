using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n===== Minimum Edit Distance Algorithm =====");
            Console.WriteLine("1. Find the 5 closest words (Part 1)");
            Console.WriteLine("2. Show MED and steps between two words (Part 2)");
            Console.WriteLine("0. Exit");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                RunPart1();
            }
            else if (choice == "2")
            {
                RunPart2();
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }
    }

    // === PART 1 ===
    static void RunPart1()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        Console.Write("Enter a word: ");
        string input = Console.ReadLine();

        string vocabPath = "vocabulary_tr.txt";
        if (!File.Exists(vocabPath))
        {
            Console.WriteLine($"txt file not found: {vocabPath}");
            return;
        }

        string[] vocabulary = File.ReadAllLines(vocabPath);
        var distances = new List<(string word, int distance)>();

        foreach (var word in vocabulary)
        {
            string w = word.Trim();
            int distance = Levenshtein(input, w);
            distances.Add((w, distance));
        }

        var top5 = distances.OrderBy(x => x.distance).Take(5);

        Console.WriteLine("\nClosest 5 words:");
        foreach (var item in top5)
        {
            Console.WriteLine($"{item.word} (distance: {item.distance})");
        }

        stopwatch.Stop();
        Console.WriteLine($"\n[INFO] Part 1 runtime: {stopwatch.ElapsedMilliseconds} ms");

    }


    static int Levenshtein(string s, string t)
    {

        int[,] dp = new int[s.Length + 1, t.Length + 1];

        for (int i = 0; i <= s.Length; i++) dp[i, 0] = i;
        for (int j = 0; j <= t.Length; j++) dp[0, j] = j;

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 1; j <= t.Length; j++)
            {
                int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;

                dp[i, j] = Math.Min(
                    Math.Min(dp[i - 1, j] + 1,        // deletion
                             dp[i, j - 1] + 1),       // insertion
                    dp[i - 1, j - 1] + cost           // substitution
                );
            }
        }

        return dp[s.Length, t.Length];
    }

    // === PART 2 ===
    static void RunPart2()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        Console.Write("Word 1: ");
        string source = Console.ReadLine();
        Console.Write("Word 2: ");
        string target = Console.ReadLine();

        int m = source.Length;
        int n = target.Length;

        int[,] dp = new int[m + 1, n + 1];

        for (int i = 0; i <= m; i++) dp[i, 0] = i;
        for (int j = 0; j <= n; j++) dp[0, j] = j;

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;
                dp[i, j] = Math.Min(
                    Math.Min(dp[i - 1, j] + 1,        // deletion
                             dp[i, j - 1] + 1),       // insertion
                    dp[i - 1, j - 1] + cost           // substitution
                );
            }
        }

        Console.WriteLine($"\nMinimum Edit Distance: {dp[m, n]}\n");

        List<(int i, int j)> path = new();
        int x = m, y = n;
        List<string> operations = new();

        while (x > 0 || y > 0)
        {
            path.Add((x, y));

            if (x > 0 && y > 0 && dp[x, y] == dp[x - 1, y - 1] + ((source[x - 1] == target[y - 1]) ? 0 : 1))
            {
                if (source[x - 1] != target[y - 1])
                    operations.Add($"Replace {source[x - 1]} → {target[y - 1]}");
                x--; y--;
            }
            else if (x > 0 && dp[x, y] == dp[x - 1, y] + 1)
            {
                operations.Add($"Delete {source[x - 1]}");
                x--;
            }
            else if (y > 0 && dp[x, y] == dp[x, y - 1] + 1)
            {
                operations.Add($"Insert {target[y - 1]}");
                y--;
            }
        }
        path.Add((0, 0));
        path.Reverse();
        operations.Reverse();

        Console.Write("     ");
        for (int j = 0; j <= n; j++)
            Console.Write($"{(j == 0 ? ' ' : target[j - 1]),3}");
        Console.WriteLine();

        for (int i = 0; i <= m; i++)
        {
            Console.Write($"{(i == 0 ? ' ' : source[i - 1]),3} ");
            for (int j = 0; j <= n; j++)
            {
                if (path.Contains((i, j)))
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;

                Console.Write($"{dp[i, j],3}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nSteps:");
        foreach (var op in operations)
            Console.WriteLine("- " + op);

        stopwatch.Stop();
        Console.WriteLine($"\n[INFO] Part 2 runtime: {stopwatch.ElapsedMilliseconds} ms");

    }

}
