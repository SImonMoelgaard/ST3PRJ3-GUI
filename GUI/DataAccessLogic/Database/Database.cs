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
        private SqlConnection connection;

        private SqlDataReader reader;

        private SqlCommand command;

        private const String DBlogin = "F20ST2ITS2201810696";
        public void DBConnection()
        {
            connection = new SqlConnection("Data Source=st-i4dab.uni.au.dk; Initial Catalog=" + DBlogin + "; User ID= " + DBlogin + "; " +
                                           "Password=" + DBlogin + "; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; " +
                                           "ApplicationIntent=ReadWrite; MultiSubnetFailover=false");
        }



        public void GetMeasurement()
        {
            throw new System.NotImplementedException();
        }

        public void SaveMeasurement()
        {
            throw new System.NotImplementedException();
        }

       
    }
}