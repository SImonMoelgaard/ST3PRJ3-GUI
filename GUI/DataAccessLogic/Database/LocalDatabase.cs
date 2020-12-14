using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using DTO;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace DataAccessLogic
{
    public class LocalDatabase : ILocalDatabase
    {
        private bool result;
        
        /// <summary>
        /// Finder den seneste redigerede fil og retunerer denne i en string.
        /// </summary>
        /// <param name="latestfile"></param>
        /// <returns></returns>
        public string latestfile(string latestfile)
        {
            string path = @"C:\ST3PRJ3FIL\ ";
            var files = new DirectoryInfo(path).GetFiles("*.*");
            latestfile = "";

            DateTime lastupdated = DateTime.MinValue;

            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime>lastupdated)
                {
                    lastupdated = file.LastWriteTime;
                    latestfile = file.Name;

                }


            }

            return latestfile;



        }
        
        /// <summary>
        /// Gemmer målingen undervejs. Her gemmer den for hvert nye målepunkt.
        /// </summary>
        /// <param name="socSecNb"></param>
        /// <param name="mmhg"></param>
        /// <param name="tid"></param>
        /// <param name="highSys"></param>
        /// <param name="lowSys"></param>
        /// <param name="highDia"></param>
        /// <param name="lowDia"></param>
        /// <param name="highMean"></param>
        /// <param name="lowMean"></param>
        /// <param name="sys"></param>
        /// <param name="dia"></param>
        /// <param name="mean"></param>
        /// <param name="pulse"></param>
        /// <param name="batterystatus"></param>
        /// <returns></returns>
        public object SaveMeasurement(string socSecNb, double mmhg, DateTime tid, bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean, int sys, int dia, int mean, int pulse, int batterystatus)
        {
            socSecNb = "";
            mmhg = 0;

            var filename = latestfile("");
            string path = @"C:\ST3PRJ3FIL\"+filename;

            //string path = @"C:\ST3PRJ3FIL\ " + soc.ToString() + DateTime.Now.ToString("dd-MM-yyyy");
            DTO_Measurement measurement = new DTO_Measurement(socSecNb, mmhg, tid, highSys, lowSys, highDia, lowDia, highMean, lowMean, sys, dia, mean, pulse, batterystatus);


            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, measurement);
                
            }

            return null;
        }
        
        /// <summary>
        /// Gemmer alle grænseværdier, den seneste kalibrering og Zeroval i databasen.
        /// </summary>
        /// <param name="SysHigh"></param>
        /// <param name="SysLow"></param>
        /// <param name="DiaHigh"></param>
        /// <param name="DiaLow"></param>
        /// <param name="Meanlow"></param>
        /// <param name="Meanhigh"></param>
        /// <param name="CprPatient"></param>
        /// <param name="Calval"></param>
        /// <param name="Zeroval"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Denne metode læser fra databasen og returnere disse data, så de kan sendes til Raspberry Pi'en.
        /// </summary>
        /// <param name="SysHigh"></param>
        /// <param name="SysLow"></param>
        /// <param name="DiaHigh"></param>
        /// <param name="DiaLow"></param>
        /// <param name="Meanlow"></param>
        /// <param name="Meanhigh"></param>
        /// <param name="CprPatient"></param>
        /// <param name="Calval"></param>
        /// <param name="Zeroval"></param>
        /// <returns></returns>
        public object ReadPatientData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
            ISendRPi sendrpi = new SendRPi();
            

            var filename = latestfile("");
            string path = @"C:\ST3PRJ3FIL\" + filename;

            
            
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
                    return null;
                }
                return sendrpi.sendemergencydata(emergencydata.HighSys, emergencydata.LowSys, emergencydata.HighDia, emergencydata.LowDia, emergencydata.LowMean, emergencydata.HighMean, emergencydata.SocSecNB, emergencydata.CalVal, emergencydata.ZeroVal);
            
            


            
        }

        /// <summary>
        /// Denne metode gemmer kalibrering. Her gemmer den 2 lister med målepunkter og udregnede data udfra disse målepunkter.
        /// </summary>
        /// <param name="calReference"></param>
        /// <param name="calMeasured"></param>
        /// <param name="r2"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="zv"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Denne metode finder den seneste kalibrationsværdi og returnerer denne.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// This method gets the patientdata from the latest file and returns them
        /// </summary>
        /// <returns>
        /// The most recent patientdata
        /// </returns>
        public List<DTO_PatientData> ValuedataGet()
        {

            var valueData = new DTO_PatientData(0, 0, 0, 0, 0, 0, "", 0, 0);
            List<DTO_PatientData> valueDataList = new List<DTO_PatientData>();
            var filename = latestfile("");
            string path = @"C:\ST3PRJ3FIL\" + filename;
            

            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    valueData = JsonConvert.DeserializeObject<DTO_PatientData>(json);
                    valueDataList.Add(valueData);
                }
            }
            catch (Exception e)
            {
                
            }

            return valueDataList;
        }
        /// <summary>
        /// Denne metode tjekker om et givent brugernavn og password matcher med databasen.
        /// </summary>
        /// <param name="socSecNb"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Denne metode henter et personnummer fra databasen.
        /// </summary>
        /// <param name="SocSecNB"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Denne metode kan benyttes til at gemme nye profiler til login.
        /// </summary>
        /// <returns></returns>
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