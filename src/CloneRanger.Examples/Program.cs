using System;

namespace CloneRanger.Examples
{
    public class Program
    {         
        public static void Main(string[] args)
        {
            var examples = new Examples();
            examples.CloneAnObject(new AnyClass());

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
