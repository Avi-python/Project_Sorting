// See https://aka.ms/new-console-template for more information
using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using MySqlConn;
using System.Diagnostics;
using SortLibrary;
public class MainClass
{
    public static void Main()
    {
        // newRecord(4, 100000);
        string str = "server=127.0.0.1;port=3306;uid=root;pwd=Danielis14;database=sortingdata";
        MySqlConnecter A = new MySqlConnecter(str); 
        List<int> result = A.DoSomething("SELECT * FROM `record4`");
        Console.WriteLine("DataSize:{0}", result.Count());
        Stopwatch s = new Stopwatch();
        // List<int> test = new List<int>(){5, 3, 2, 4, 1, 6};
        s.Start();
        result.QuickSort();
        // Thread.Sleep(2);`
        s.Stop();

        TimeSpan ts = s.Elapsed;



        Console.WriteLine("{0}.{1, 0:D3}{2, 0:D3}", ts.Seconds, ts.Milliseconds, ts.Microseconds);
        // createTable(4);
        // newRecord(4, 100000);
        
        
    }

    public static void newRecord(int recordNum, int quality)
    { 
        string str = "server=127.0.0.1;port=3306;uid=root;pwd=Danielis14;database=sortingdata";
        MySqlConnecter A = new MySqlConnecter(str); 
        // A.TryConnect();
        int value = 0;
        Random r = new Random();
        List<int> result;
        for(int i = 0; i < quality; i++)
        {
            // value = r.Next(0, quality);
            value = quality - i;
            result = A.DoSomething(string.Format($"INSERT INTO `special_case{recordNum}`(`Number`) VALUES({value})"));    
        }
    }

    public static void createTable(int num)
    {
        string conStr = "server=127.0.0.1;port=3306;uid=root;pwd=Danielis14;database=sortingdata";
        MySqlConnecter A = new MySqlConnecter(conStr);
        A.DoSomething(string.Format($"CREATE TABLE `special_case{num}` ( `Index` INT NOT NULL AUTO_INCREMENT, `Number` INT NOT NULL, PRIMARY KEY (`Index`));"));
    }

}