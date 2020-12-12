using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Data;

using System.Threading.Tasks;
using DTO;


namespace DataAccessLogic
{
    /// <summary>
    /// This class will only work if the computer is connected to VPN AU University
    /// </summary>
    public class Database : IDatabase
    {
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;
        private const string DBlogin = "F20ST2ITS2201810696";

        /// <summary>
        /// Connection to the database
        /// </summary>
        public Database()
        {
            connection = new SqlConnection("Data Source=st-i4dab.uni.au.dk; Initial Catalog=" + DBlogin + "; User ID= " + DBlogin + "; " +
                                           "Password=" + DBlogin + "; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; " +
                                           "ApplicationIntent=ReadWrite; MultiSubnetFailover=false");
        }

        /// <summary>
        /// Get soc sec from database
        /// </summary>
        /// <param name="SocSecNB"></param>
        /// <returns></returns>
        public bool getSocSecNB(string SocSecNB)
        {
            bool result = false;
            command=new SqlCommand("select * from dbo.[ST3PRJ3M] where SocSecNB= '" + SocSecNB+"'", connection);
            connection.Open();
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["SocSecNB"].ToString()==SocSecNB)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }catch{}
            connection.Close();

            return result;
        }

        /// <summary>
        /// This method checks if the user is registered in the database
        /// </summary>
        /// <param name="socSecNb"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool isUserRegistered(string socSecNb, string pw)
        {

            bool result = false;
            command = new SqlCommand("select * from dbo.[User] where SocSecNB = '" + socSecNb + "'", connection);

            connection.Open();
            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())

                {

                    if (reader["SocSecNB"].ToString() == socSecNb && reader["SocSecPW"].ToString() == pw)
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

        /// <summary>
        /// This method returns a measurement, if there is one for the given patient
        /// </summary>
        /// <param name="socSecNb"></param>
        /// <returns></returns>
        public List<DTO_Measurement> GetMeasurement(string socSecNb)
        {
            List<DTO_Measurement> measurementList =new List<DTO_Measurement>();
            //connection = new SqlConnection("Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + DBlogin + ";User ID=" + DBlogin + ";Password=" + DBlogin + ";Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            command = new SqlCommand("select * from dbo.[ST3PRJ3M] where socSecNb='" + socSecNb + "'", connection);
            connection.Open();

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                measurementList.Add(new DTO_Measurement(Convert.ToString(reader["SocSecNB"]),Convert.ToDouble(reader["RawData"]),Convert.ToDateTime(reader["Date"]), Convert.ToBoolean(reader["Highsys"]), Convert.ToBoolean(reader["Lowsys"]), Convert.ToBoolean(reader["Highdia"]), Convert.ToBoolean(reader["Lowdia"]), Convert.ToBoolean(reader["HighMean"]), Convert.ToBoolean(reader["LowMean"]), Convert.ToInt32(reader["Sys"]), Convert.ToInt32(reader["Dia"]), Convert.ToInt32(reader["Mean"]), Convert.ToInt32(reader["Pulse"]), Convert.ToInt32(reader["Batterystatus"])));
            }
            connection.Close();
            return measurementList;
        }
    }
}