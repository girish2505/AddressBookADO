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
            Transaction transaction = new Transaction();
            Console.WriteLine("1.Connecting to DB And Retrieve the data from sql server");
            Console.WriteLine("2.Insert into table");
            Console.WriteLine("3.Edit the existing contact using update query");
            Console.WriteLine("4.Retrieve Data Based on City And State");
            Console.WriteLine("5.Retrieve Count Group By State And City");

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
                case 3:
                    addrBookRepo.EditExistingContact(addrBook);
                    break;
                case 4:
                    addrBookRepo.RetrieveDataBasedOnStateAndCity(addrBook);
                    break;
                case 5:
                    addrBookRepo.RetrieveCountGroupByStateAndCity(addrBook);
                    break;
                case 6:
                    transaction.AlterTable();
                    break;
                case 7:
                    transaction.UpdateStartDateValue();
                    break;
                case 8:
                    transaction.RetrieveData();
                    break;
                case 9:
                    transaction.InsertIntoTablesUsingTransaction();
                    break;
            }
        }
    }
}
