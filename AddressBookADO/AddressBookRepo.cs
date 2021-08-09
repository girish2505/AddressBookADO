using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookADO
{
    public class AddressBookRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBookSystem;Integrated Security=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int RetrieveData()
        {

            AddressBookModel model = new AddressBookModel();

            int count = 0;
            using (sqlConnection)
            {

                string query = @"select * from Address_Book_Table";
                SqlCommand command = new SqlCommand(query, this.sqlConnection);

                sqlConnection.Open();

                SqlDataReader sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        DisplayDetails(sqlDataReader);
                        count++;
                    }

                }
                sqlDataReader.Close();
            }
            return count;
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
            addressBook.phonenum = Convert.ToDouble(sqlDataReader["Phonenum"]);
            addressBook.emailId = Convert.ToString(sqlDataReader["EmailId"]);
            addressBook.addrBookName = Convert.ToString(sqlDataReader["AddressBookName"]);
            addressBook.relationType = Convert.ToString(sqlDataReader["RelationType"]);
            Console.WriteLine("FirstName :{0} LastName :{1} Address :{2} City :{3} State :{4} ZipCode :{5} PhoneNum :{6} EmailId :{7} AddressBookName :{8} RelationType :{9} ", addressBook.firstName, addressBook.lastName, addressBook.address, addressBook.city, addressBook.stateName, addressBook.zipCode, addressBook.phonenum, addressBook.emailId, addressBook.addrBookName, addressBook.relationType);
            Console.WriteLine("\n");
        }
        public int InsertIntoTable(AddressBookModel addressBook)
        {
            int count = 0;
            using (sqlConnection)
            {
                SqlCommand sqlCommand = new SqlCommand("dbo.InsertTable", this.sqlConnection);

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FirstName", addressBook.firstName);
                sqlCommand.Parameters.AddWithValue("@LastName", addressBook.lastName);
                sqlCommand.Parameters.AddWithValue("@Address", addressBook.address);
                sqlCommand.Parameters.AddWithValue("@City", addressBook.city);
                sqlCommand.Parameters.AddWithValue("@StateName", addressBook.stateName);
                sqlCommand.Parameters.AddWithValue("@ZipCode", addressBook.zipCode);
                sqlCommand.Parameters.AddWithValue("@Phonenum", addressBook.phonenum);
                sqlCommand.Parameters.AddWithValue("@EmailId", addressBook.emailId);
                sqlCommand.Parameters.AddWithValue("@AddressBookName", addressBook.addrBookName);
                sqlCommand.Parameters.AddWithValue("@RelationType", addressBook.relationType);

                sqlConnection.Open();

                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    count++;
                    Console.WriteLine("Inserted Successfully");
                }
            }
            return count;
        }
        public int EditExistingContact(AddressBookModel addressBook)
        {
            int count = 0;
            using (sqlConnection)
            { 
                string query = @"update Address_Book_Table set EmailId='girish@gmail.com' where FirstName='girish'";
                SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
                sqlConnection.Open();
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    count++;
                    Console.WriteLine("Updated SuccessFully");
                }
            }
            return count;
        }
        public int RetrieveDataBasedOnStateAndCity(AddressBookModel addressBook)
        {
            int count = 0;
            try
            {
                using (sqlConnection)
                {
                    string query = @"Select FirstName,LastName from Address_Book_Table where City = 'nellore' or StateName = 'andhra'";
                    SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            count++;
                            addressBook.firstName = Convert.ToString(sqlDataReader["FirstName"]);
                            addressBook.lastName = Convert.ToString(sqlDataReader["LastName"]);
                            Console.WriteLine("FirstName :{0}\t LastName:{1}\t ", addressBook.firstName, addressBook.lastName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return count;
        }
      
        public string RetrieveCountGroupByStateAndCity(AddressBookModel addressBook)
        {
            string list = null;
            try
            {
                using (sqlConnection)
                {
                    string query = @"Select Count(*) As Count, StateName, City from Address_Book_Table group by StateName,City";
                    SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {

                            Console.WriteLine("Count :{0}\t StateName:{1}\t City :{2}\t", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2]);
                            list += sqlDataReader[0] + " " + sqlDataReader[1] + " " + sqlDataReader[2] + " ";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return list;
        }
    }
}
