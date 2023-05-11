// See https://aka.ms/new-console-template for more informatioㄋ
using System;
using SortLibrary;

List<int> a = new List<int>();

Random R = new Random();

for(int i = 0; i < 10; i++)
{
    a.Add(10 - i);
}

a.ShellSort();
foreach(int i in a)
{
    Console.Write($"{i}, ");
}

