using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[] arr = new int[] { 3, 1, 5, 6, 8, 2, 4, 11, 0 };
            long t1 = DateTime.Now.Ticks;
            QuickSortRecursion(0, arr.Length - 1, arr);
            //QuickSortWithoutRecursion (0, arr.Length - 1, arr);  
            long t2 = DateTime.Now.Ticks;
            //输出  
            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("DelaTime : " + (t2 - t1));
        }
        //分治法分开的部分的算法  
        private static int Partition(int low, int high, int[] arr)
        {
            int left = low;
            int right = high;
            int pivot = arr[low];                              //用于对比的关键点  
            //实现比pivot大的数放后面，比pivot小的数放前面，复杂度为n  
            while (left < right)
            {
                while (left < right && arr[right] >= pivot)
                {
                    right--;
                }
                arr[left] = arr[right];                       //比pivot大的数扔前面  
                while (left < right && arr[left] <= pivot)
                {
                    left++;
                }
                arr[right] = arr[left];                       //pivot小的数扔后面  
            }
            arr[left] = pivot;                                 //对关键点移动后的索引位置赋值  
            return left;                                            //此时左右标志位索引一致，返回任一即可  
        }
        //快速排序调用递归的方法  
        private static void QuickSortRecursion(int low, int high, int[] arr)
        {
            int pivot;
            if (low < high)
            {
                pivot = Partition(low, high, arr);         //获取新的关键点的索引位置  
                QuickSortRecursion(low, pivot - 1, arr);
                QuickSortRecursion(pivot + 1, high, arr);
            }
        }
        //非递归快速排序,使用栈的思路，将每次需要  
        private static void QuickSortWithoutRecursion(int low, int high, int[] arr)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(low);                                   //现在栈中放入数组的首尾位置  
            stack.Push(high);
            while (stack.Count != 0)
            {                          //当栈为空，说明所有元素已经遍历完，数组已经排好  
                int q = stack.Pop();
                int p = stack.Pop();
                int pivot = Partition(p, q, arr);              //每次从栈中取出一对首尾值并获取此部分数组的关键值  
                if (p < pivot - 1)
                {
                    stack.Push(p);
                    stack.Push(pivot - 1);
                }
                if (pivot + 1 < q)
                {                             //两个判断表示当关键值符合大于首小于尾的条件时将其  
                    stack.Push(pivot + 1);                     //和首尾放入栈以便下次排序  
                    stack.Push(q);
                }
            }
        }  
    }
}
