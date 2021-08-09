using System;

namespace AddressBookADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Address Book ADO!");
            AddressBookRepo addrBookRepo = new AddressBookRepo();
            AddressBookModel addrBook = new AddressBookModel();
            Console.WriteLine("1.Connecting to DB And Retrieve the data from sql server");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    addrBookRepo.RetrieveData();
                    break;
                case 2:
                    addrBook.firstName = "Anish";
                    addrBook.lastName = "guptha";
                    addrBook.address = "velacherry";
                    addrBook.city = "Chennai";
                    addrBook.stateName = "Tamilnadu";
                    addrBook.zipCode = "600015";
                    addrBook.phonenum = 932415670;
                    addrBook.emailId = "a.g@gmail.com";
                    addrBook.addrBookName = "Cousin";
                    addrBook.relationType = "Family";
                    addrBookRepo.InsertIntoTable(addrBook);
                    break;
            }
        }
    }
}
