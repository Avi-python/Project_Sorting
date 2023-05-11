using System;
using System.Net;
using System.Diagnostics;
namespace SortLibrary;
public static partial class UnstableSortExtension
{
    public class Hnode
    {
        private int _next;
        private int _key;
        public int Next { get { return _next; } set { _next = value; } } // 無法修改的原因是 struct 本身是值類型，所以回傳的會是一個拷貝的值，沒辦法真正改到裡面的值。
        public int Key { get { return _key; } set { _key = value; } }
        public Hnode(int key, int next)
        {
            _key = key;
            _next = next;
        }
    }
    
    public static void HeapSort(this List<int> arr)
    {
        HeapSortHelper(arr, arr.Count());
    }

    public static void HeapSortHelper(List<int> arr, int n)
    {
        for(int i = n / 2; i >= 1; i--)
        {
            HeapAdjust(arr, i, n);
        }

        for(int i = n - 1; i >= 1; i--)
        {
            Swap(arr, 0, i);
            HeapAdjust(arr, 1, i);
        }
    }

    public static void HeapAdjust(List<int> arr, int iRoot, int n)
    {
        int e = arr[iRoot - 1];
        int j;
        for(j = 2 * iRoot; j <= n; j *= 2)
        {
            if(j < n && arr[j - 1] < arr[j + 1 - 1]) j++;
            if(e >= arr[j - 1]) break;
            arr[j / 2 - 1] = arr[j - 1];
        }
        arr[j / 2 - 1] = e;
    }
 
    [Conditional("DEBUG")]
    public static void ShowHelper(List<Hnode> input)    
    {
        int head = input[0].Next;
        while(head != 0)
        {
            Console.Write("({0:00}, {1:00}), ", input[head].Key, input[head].Next);
            head = input[head].Next;
        }
        Console.Write("\n");
    }
    public static void MergeSort(this List<int> arr) 
    {
        // 為了免除在 Merge裡面要開tmp arr，用資料結構課本裡的方式將數據打包就可以做到。
        List<Hnode> helper = new List<Hnode>();
        int n = arr.Count();
        helper.Add(new Hnode(0, 1));
        for(int i = 0; i < n; i++)
        {
            helper.Add(new Hnode(arr[i], i + 2));
        }
        helper[n].Next = 0;
        // ShowHelper(helper);
        int head = MergeSortHelper(helper, 1, n + 1); 
        for(int i = 0; i < n; i++)
        {
            arr[i] = helper[head].Key;
            head = helper[head].Next;
        }
    }

    public static int MergeSortHelper(List<Hnode> helper, int lb, int rb) // 左閉右開
    {
        if(lb >= rb - 1) return lb;
        int mid = lb + (rb - lb) / 2;
        helper[mid - 1].Next = 0;
        helper[rb - 1].Next = 0;
        return Merge(helper, MergeSortHelper(helper, lb, mid),
                          MergeSortHelper(helper, mid, rb));
    }

    public static int Merge(List<Hnode> arr, int start1, int start2)
    {
        int iResult = 0;
        int i1, i2;
        // ShowHelper(arr);//
        for(i1 = start1, i2 = start2; i1 != 0 && i2 != 0;)
        {
            if(arr[i1].Key < arr[i2].Key)
            {
                arr[iResult].Next = i1;
                i1 = arr[i1].Next; iResult = arr[iResult].Next;
            }
            else
            {
                arr[iResult].Next = i2;
                i2 = arr[i2].Next; iResult = arr[iResult].Next;
            }
        }
        
        if(i1 == 0) arr[iResult].Next = i2;
        else        arr[iResult].Next = i1;


        return arr[0].Next;
    }

    public static void QuickSort(this List<int> arr)
    {
        QuickSortHelper(ref arr, 0, arr.Count() - 1);
    }

    private static void QuickSortHelper(ref List<int> arr, int lb, int rb) // 沒有左閉右開
    {
        if(lb < rb)
        {
            int l = lb, r = rb + 1, pivot = arr[lb];
            do
            {
                do l++; while(l <= rb && arr[l] < pivot);
                do r--; while(r >= 0 && arr[r] > pivot);
                if(l < r) arr.Swap(l, r);
            } while(l < r);
            arr.Swap(lb, r);
            QuickSortHelper(ref arr, lb, r - 1);
            QuickSortHelper(ref arr, r + 1, rb);
        }
    }

    public static void QuickSortRandomized(this List<int> arr)
    {
        QuickSortRandomizedHelper(arr, 0, arr.Count() - 1);
    }

    private static Random RandomHelper = new Random();
    private static void QuickSortRandomizedHelper(List<int> arr, int lb, int rb)
    {
        if(lb < rb)
        {
            int s = RandomHelper.Next(lb + 1, arr.Count());

            Swap(arr, lb, s);

            int l = lb, r = rb + 1, pivot = arr[lb];
            do
            {
                do l++; while(l <= rb && arr[l] < pivot);
                do r--; while(r >= 0 && arr[r] > pivot);
                if(l < r) arr.Swap(l, r);
            } while(l < r);
            arr.Swap(lb, r);
            QuickSortRandomizedHelper(arr, lb, r - 1);
            QuickSortRandomizedHelper(arr, r + 1, rb);
        }
    }


    private static void Swap(this List<int> arr, int l, int r)
    {
        int tmp = arr[l];
        arr[l] = arr[r];
        arr[r] = tmp;
    }

    private static void test(this ref int input)
    {
        input = 10;
    }
}
