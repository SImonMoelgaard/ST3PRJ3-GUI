using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;



namespace DataAccessLogic
{
    public class Database : IDatabase
    {
        public void GetMeasurement()
        {
            throw new System.NotImplementedException();
        }

        public void SaveMeasurement()
        {
            throw new System.NotImplementedException();
        }

        public bool Login(String UserID, String UserPassword)
        {
            bool result = false;
            command = new SqlCommand("select * from dbo.[User] where SocSecNB = '" + UserID+ "'", connection);

            connection.Open();
            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())

                {

                    if (reader["SocSecNB"].ToString() == UserID && reader["SocSecPW"].ToString() == UserPassword)
                    {
                        result = true;

                    }
                    else
                    {
                        result = false;

                    }
                }


            }
            catch
            {

            }
            connection.Close();

            return result;
        }
    }
}