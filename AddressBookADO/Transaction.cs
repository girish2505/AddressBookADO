using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBookADO
{
    class Transaction
    {
        public static string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBookSystem;Integrated Security=True;";
        SqlConnection SqlConnection = new SqlConnection(connection);
        List<AddressBookModel> addrList = new List<AddressBookModel>();
        public int AlterTable()
        {
            int result = 0;
            SqlConnection.Open();
            using (SqlConnection)
            {
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = "Alter table Contact_Person add Date_Value Date";
                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Updated!");
                }
            }
            SqlConnection.Close();
            return result;
        }
        public int UpdateStartDateValue()
        {
            int count = 0;
            SqlConnection.Open();
            using (SqlConnection)
            {
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = "Update Contact_Person Set Date_Value='2019-03-23' where ContactID>2";
                    sqlCommand.ExecuteNonQuery();
                    count++;
                    sqlCommand.CommandText = "Update Contact_Person Set Date_Value='2021-01-05' where ContactID<=2";

                    sqlCommand.ExecuteNonQuery();
                    count++;
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated Sucessfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Updated!");
                }
            }
            SqlConnection.Close();
            return count;
        }
        public int RetrieveData()
        {
            int count = 0;
            SqlConnection.Open();
            using (SqlConnection)
            {
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = @"select FirstName,LastName,Address,City,StateName,ZipCode,PhoneNum,EmailId,Date_Value from Contact_Person
                    where Date_Value between ('2021-01-01') and GetDate()";
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t{5}\t{6}\t{7}", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2], sqlDataReader[3], sqlDataReader[4], sqlDataReader[5], sqlDataReader[6], sqlDataReader[7]);
                            count++;
                        }
                    }
                    sqlDataReader.Close();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Retrieve Data Successfully");
                }
            }
            SqlConnection.Close();
            return count;
        }
        public int InsertIntoTablesUsingTransaction()
        {
            int flag = 0;
            SqlConnection.Open();
            using (SqlConnection)
            {
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = "Insert into Contact_Person values(1,'abc','xyz','ttt Street','Hyderabad','Telangana',543212,1234567890,'abc@gmail.com','2021-05-05')";
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.CommandText = "Insert into Relation_Type values(1,5)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("Inserted Successfully!");
                    flag = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sqlTransaction.Rollback();
                    flag = 0;
                }
            }
            SqlConnection.Close();
            return flag;
        }
        public void AddMultipleDataToList()
        {
            try
            {
                string query = @"select AddressBookName,FirstName,LastName,Address,City,StateName,ZipCode,PhoneNum,EmailId,ContactTypeName from Address_Book
     Full JOIN Contact_Person on Address_Book.AddressBookID = Contact_Person.AddressBook_ID
   Full JOIN Relation_Type on Relation_Type.Contact_ID = Contact_Person.ContactID
    Full JOIN Contact_Type on Relation_Type.ContactType_ID = Contact_Type.ContactTypeID";
                SqlCommand sqlCommand = new SqlCommand(query, this.SqlConnection);
                SqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        DisplayDetails(sqlDataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlConnection.Close();
        }
        public bool ImplementingUsingThread()
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            AddMultipleDataToList();
            stopWatch.Stop();
            Console.WriteLine("Duration with Thread excecution : {0} ", stopWatch.ElapsedMilliseconds);
            int elapsedTime = Convert.ToInt32(stopWatch.ElapsedMilliseconds);
            if (elapsedTime != 0)
            {
                return true;
            }
            return false;
        }
        public void DisplayDetails(SqlDataReader sqlDataReader)
        {
            AddressBookModel addressBook = new AddressBookModel();
            addressBook.firstName = Convert.ToString(sqlDataReader["FirstName"]);
            addressBook.lastName = Convert.ToString(sqlDataReader["LastName"]);
            addressBook.address = Convert.ToString(sqlDataReader["Address"]);
            addressBook.city = Convert.ToString(sqlDataReader["City"]);
            addressBook.stateName = Convert.ToString(sqlDataReader["StateName"]);
            addressBook.zipCode = Convert.ToString(sqlDataReader["ZipCode"]);
            addressBook.phonenum = Convert.ToInt64(sqlDataReader["phonenum"]);
            addressBook.emailId = Convert.ToString(sqlDataReader["EmailId"]);
            addressBook.addrBookName = Convert.ToString(sqlDataReader["AddressBookName"]);
            addressBook.contactTypeName = Convert.ToString(sqlDataReader["ContactTypeName"]);
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t {5} \t {6} {7}\t {8}\t {9}\t", addressBook.firstName, addressBook.lastName, addressBook.address, addressBook.city, addressBook.stateName, addressBook.zipCode, addressBook.phonenum, addressBook.emailId, addressBook.addrBookName, addressBook.contactTypeName);
                addrList.Add(addressBook);
            });
            thread.Start();
        }
    }
}
