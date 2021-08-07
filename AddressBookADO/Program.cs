using System;

namespace AddressBookADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Address Book ADO!");
            AddressBookRepo addrBookRepo = new AddressBookRepo();
            Console.WriteLine("1.Connecting to DB And Retrieve the data from sql server");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    addrBookRepo.RetrieveData();
                    break;
            }
        }
    }
}
