using SE2.LabManager.Data;
using SE2.LabManager.Data.Contracts;
using System;
using System.Threading.Tasks;

namespace SE2.LabManager.Data.TestConsole {
    class Program {
        static void Main(string[] args) {

            //Fill DB with Test Entries
            new DataBase();

            Console.WriteLine();
            Console.Write("Filled Up the Database!");
            Console.ReadLine();

        }

    }
}
