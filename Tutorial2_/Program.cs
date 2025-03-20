public class A
{
    void M()
    {
        int number = 10;
        string name = $"Test {number}";
        string path = @"C:\Users\User\Documents\studies\4 sem\APBD\APBD";
        
        int[] numbers = { 1, 2, 3, 4, 5 };
        string[] names = new string[3];
        //numbers.Length
        
        var list = new List<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        var set = new HashSet<int>();
        set.Add(1);
        set.Add(2);
        set.Add(3);
        var dict = new Dictionary<int, string>();
        dict.Add(1, "one");
        dict.Add(2, "two");
        dict.Add(3, "three");
        
        
    }
}