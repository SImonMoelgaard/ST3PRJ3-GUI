using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{

    
    public class Controller
    {
        ISendRPi sendrpi = new SendRPi();
        IReceiveRPi recieveRPi = new ReceiveRPi();
        ILocalDatabase localDatabase = new LocalDatabase();
        IDatabase database = new Database();
        //Read from file
        private ReadFromFile readFromFile = new ReadFromFile();

        private List<DTO_Measurement> measurementList;
        private List<DTO_Measurement> measurementdata;
        private double data;
        private List<DTO_CalVal> caldata;
        
        

       
        

        public void DBcontrol()
        {
            measurementList = new List<DTO_Measurement>();
        }



        public void openrecieveports()
        {
            sendrpi.OpenSendPorts();
            recieveRPi.OpenRecievePorts();
            
        }

       
        public List<DTO_Measurement> GetMeasurementData()
        {
            
            measurementdata = recieveRPi.RecieveDataPoints();


            //measurementdata = recieveRPi.ReceiveMeasurment();
            return measurementdata;
            //for (int i = 0; i < 182; i++)
            //{
            //    Thread.Sleep(3);
            //    List<DTO_Measurement> test = new List<DTO_Measurement>();
            //    test.Add(measurementdata[i]);
            //    return test;
                
            //}

            //return null;


        }
        

        public double RecieveDouble()
        {
            data = recieveRPi.RecieveCalculatedValues();
            return data;
        }

        
        public List<DTO_CalVal> GetCalVal()
        {
            caldata = localDatabase.GetCalVal();
            return caldata;

        }

        public List<DTO_Measurement> GetMeasurementLocal(string socSecNB)
        {
            measurementList = database.GetMeasurement(socSecNB);
            
            return measurementList;
        }

        public bool GetSocSecNb(string SocSecNB)
        {
            return localDatabase.getSocSecNB(SocSecNB);
            
        }

        public bool CheckLogin(String socSecNb, String pw)
        {
            
            return localDatabase.isUserRegistered(socSecNb, pw);
        }

        public object SendRPiData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
            
            
            return sendrpi.sendpatientdata(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
        }

        public List<DTO_Measurement> ReadFromFile()
        {
            return readFromFile.Read();
        }

        public object SendEemergencydata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh,
            string CprPatient,
            double Calval, double Zeroval)
        {
            return localDatabase.ReadPatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
        }

        
        public string Command(string command)
        {
            return sendrpi.Command(command);
        }

      
        

    }


}
