using System.Diagnostics;
namespace SortLibrary;
public static partial class UnstableSortExtension
{
    public static void MergeSort2(this List<int> arr)
    {
        MergeSortHelper2(arr, 0, arr.Count());   
    }

    public static void MergeSortHelper2(List<int> arr, int lb, int rb)
    {
        if(lb >= rb - 1) return;    
        int mid = lb + (rb - lb) / 2;
        MergeSortHelper2(arr, 0, mid);
        MergeSortHelper2(arr, mid, rb);
        Merge2(arr, lb, mid, mid, rb);
    }

    public static void Merge2(List<int> arr, int l1, int r1, int l2, int r2)
    {
        int i1 = l1, i2 = l2, index = 0, n = arr.Count();
        
        int[] tmp = new int[r1 - l1 + r2 - l2];

        while(i1 < r1 && i2 < r2)
        {
            if(arr[i1] < arr[i2])
            {
                tmp[index++] = arr[i1++];
            }
            else
            {
                tmp[index++] = arr[i2++];
            }
        }

        if(i1 == r1)    while(i2 < r2) tmp[index++] = arr[i2++];
        else            while(i1 < r1) tmp[index++] = arr[i1++];
        for(int i = 0, j = l1; i < tmp.Length && j < r2; i++, j++)
        {
            arr[j] = tmp[i];
        }
        tmp = null;
    }

    public static void ShellSort(this List<int> arr)
    {
        ShellsortHelper(arr);
    }

    private static void ShellsortHelper(List<int> arr)
    {
        int n = arr.Count();
        int gap = n / 2 - 1, index, tmp;
        while(gap >= 1)
        {
            // tmp = gap;
            for(int i = gap; i < n; i += gap)
            {
                index = i;
                // Console.Write($"{i}");
                while(index >= gap)
                {
                    if(arr[index] < arr[index - gap]) 
                    {
                        Swap(arr, index, index - gap);
                        // tmp = arr[index];
                        // arr[index] = arr[index - gap];
                        // arr[index - gap] = tmp;
                    }
                    index -= gap;
                }
            }
            // Console.WriteLine($"gap:{gap}");
            gap /= 2;
        }
    }
}
