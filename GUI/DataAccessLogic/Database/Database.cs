using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Data;

using System.Threading.Tasks;
using DTO;


namespace DataAccessLogic
{
    public class Database : IDatabase
    {
        private SqlConnection connection;

        private SqlDataReader reader;

        private SqlCommand command;

        private const String DBlogin = "F20ST2ITS2201810696";


        public Database()
        {
            connection = new SqlConnection("Data Source=st-i4dab.uni.au.dk; Initial Catalog=" + DBlogin + "; User ID= " + DBlogin + "; " +
                                           "Password=" + DBlogin + "; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; " +
                                           "ApplicationIntent=ReadWrite; MultiSubnetFailover=false");
        }

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

        public void SavePatientData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
            throw new NotImplementedException();
        }


        public void SaveMeasurement(string socSecNb, double rawData, DateTime date, int sysData, int diaData, int alarmData, int pulse,
            int powerData)
        {
            throw new NotImplementedException();
        }

        public bool isUserRegistered(String socSecNb, String pw)
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

        public List<DTO_Measurement> GetMeasurement(string socSecNb)
        {
            List<DTO_Measurement> measurementList =new List<DTO_Measurement>();
            //connection = new SqlConnection("Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + DBlogin + ";User ID=" + DBlogin + ";Password=" + DBlogin + ";Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            command = new SqlCommand("select * from dbo.[ST3PRJ3M] where socSecNb='" + socSecNb + "'", connection);
            connection.Open();

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                measurementList.Add(new DTO_Measurement(Convert.ToString(reader["SocSecNB"]),Convert.ToDouble(reader["RawData"]),Convert.ToDateTime(reader["Date"]), Convert.ToBoolean(reader["Highsys"]), Convert.ToBoolean(reader["Lowsys"]), Convert.ToBoolean(reader["Highdia"]), Convert.ToBoolean(reader["Lowdia"]), Convert.ToBoolean(reader["HighMean"]), Convert.ToBoolean(reader["LowMean"]), Convert.ToInt32(reader["Sys"]), Convert.ToInt32(reader["Dia"]), Convert.ToInt32(reader["Mean"]), Convert.ToInt32(reader["Pulse"]), Convert.ToInt32(reader["BatteryStatus"])));
            }
            connection.Close();
            return measurementList;
        }

        public void SaveMeasurement()
        {
            
            
        }

       
    }
}