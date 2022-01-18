using System.Collections;
using System.Collections.Generic;
using System;

public class KakutyouMethod
{
}

public static class EnumerableHelper
{
    public static void WriteItems(this IEnumerable items) // Šg’£ƒƒ\ƒbƒh
    {
        var e = items.GetEnumerator();
        var i = 0;
        while (e.MoveNext())
        {
            Console.WriteLine($"[{i}]={e.Current}");
            i++;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var ary = new int[] { 10, 20, 30, 40, 50, 60, };
        var list = new List<int>() { 100, 200, 300 };
        var strs = new string[] { "AAA", "BBB", "CCC" };
        ary.WriteItems(); // «‚ÆÀ¿“¯‚¶
        //EnumerableHelper.WriteItems(ary);
        list.WriteItems();
        //EnumerableHelper.WriteItems(list);
        strs.WriteItems();
        //EnumerableHelper.WriteItems(strs);
    }
}