using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Channels;
using DTO;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace DataAccessLogic
{
    public class LocalDatabase : ILocalDatabase
    {
        private double A;
        private SendRPi send;
        private bool result;
        public object SaveMeasurement(string socSecNb, double mmhg, DateTime tid, bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean, int sys, int dia, int mean, int pulse, int batterystatus)
        {
            socSecNb = "";
            mmhg = 0;

            string path = @"C:\ST3PRJ3FIL\ " + socSecNb.ToString() + DateTime.Now.ToString("dd-MM-yyyy");
            DTO_Measurement measurement = new DTO_Measurement(socSecNb, mmhg, tid, highSys, lowSys, highDia, lowDia, highMean, lowMean, sys, dia, mean, pulse, batterystatus);


            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, measurement);
                
            }

            return null;
        }
        public object SavePatientData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
            
            DTO_PatientData patientData = new DTO_PatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
            string path = @"C:\ST3PRJ3FIL\ " + CprPatient.ToString() +" "+ DateTime.Now.ToString("dd-MM-yyyy");

            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, patientData);
                
            }

            return null;
            

        }

        
        public object ReadPatientData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
            ISendRPi sendrpi = new SendRPi();

            
            string path = @"C:\ST3PRJ3FIL\ " + CprPatient.ToString() + " " + DateTime.Now.ToString("dd-MM-yyyy");

            List<DTO_PatientData> data = new List<DTO_PatientData>();
            var emergencydata = new DTO_PatientData(0, 0, 0, 0, 0, 0, CprPatient, 0, 0);

            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();


                    emergencydata = JsonConvert.DeserializeObject<DTO_PatientData>(json);

                }
            }
            catch (Exception e)
            {
                return true;
            }



            
            return sendrpi.sendemergencydata(emergencydata.Syshigh, emergencydata.Syslow, emergencydata.Diahigh, emergencydata.Dialow, emergencydata.Lowmean, emergencydata.Highmean, emergencydata.SocSecNB, emergencydata.Calval, emergencydata.Zeroval);
           

        }

        public List<DTO_CalVal> SaveCalVal(List<int> calReference, List<double> calMeasured, double r2, double a, double b, double zv,
            DateTime datetime)
        {




            string path = @"C:\ST3PRJ3FIL\Calibration";
            DTO_CalVal calval = new DTO_CalVal(calReference, calMeasured, r2, a, b, zv, DateAndTime.Now);

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, calval);

            }


            return new List<DTO_CalVal>();
        }

        public List<DTO_CalVal> GetCalVal()
        {
            string path = @"C:\ST3PRJ3FIL\Calibration";
            int  zv=0, b = 0; 
            
            double r2=0;
            double a = 0;
            DateTime date;
            List<int> calReference = null;
            List<double> calMeasured = null;

            List<DTO_CalVal> Caldata = new List<DTO_CalVal>();
            var caldata = new DTO_CalVal(calReference, calMeasured, r2, a, b, zv, DateAndTime.Now);

            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();


                    caldata = JsonConvert.DeserializeObject<DTO_CalVal>(json);

                    Caldata.Add(caldata);

                }
            }
            catch (Exception e)
            {
                return null;
            }

            return Caldata;


        }

        public bool isUserRegistered(String socSecNb, String pw)
        {
            string path = @"C:\ST3PRJ3FIL\Users";
            DTO_UserData userdata = new DTO_UserData(socSecNb, pw);
            List<DTO_UserData> pwlogin = new List<DTO_UserData>();

            try
            {
                using (StreamReader r = new StreamReader(path))
                {

                    string test;
                    string json = r.ReadToEnd();

                    userdata = JsonConvert.DeserializeObject<DTO_UserData>(json);

                   // foreach (var VARIABLE in userdata)
                    {
                        //pwlogin.Add(userdata.Username, userdata.Password);
                        
                    }

                    pwlogin.Add(userdata);
                   

                    


                }
            }
            catch
            {
                
            }
            foreach (var VARIABLE in pwlogin)
            {
                var username = Convert.ToString(userdata.Username);
                var password = Convert.ToString(userdata.Password);

                if (socSecNb.ToString() == username && pw.ToString() == password)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }


            return result;

        }

        public bool getSocSecNB(string SocSecNB)
        {
            string path = @"C:\ST3PRJ3FIL\Users";
           DTO_UserData userdata = new DTO_UserData("", "");
            List<DTO_UserData> userData = new List<DTO_UserData>();
            
           try
           {
               using (StreamReader r = new StreamReader(path))
               {
                   
                   string json = r.ReadToEnd();


                   userdata = JsonConvert.DeserializeObject<DTO_UserData>(json);
                    userData.Add(userdata);


                   foreach (var VARIABLE in userData)
                   {
                       var username = userdata.Username;

                       if (username.ToString() == SocSecNB)
                       {
                           result = true;
                       }
                       else
                       {
                           result = false;
                       }

                    }



                   


                }
           }
           catch
           {
               return false;
           }

           return result;


           

        }
        public object Savelogin()
        {

            DTO_UserData user = new DTO_UserData("1", "2");
            string path = @"C:\ST3PRJ3FIL\Users";

            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, user);

            }

            return user;


        }

    }
}