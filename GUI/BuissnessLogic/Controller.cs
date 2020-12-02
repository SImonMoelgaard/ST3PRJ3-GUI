using System;
using System.Collections.Generic;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{

    
    public class Controller
    {
        private List<DTO_Measurement> measurementList;
        private List<DTO_Measurement> measurementdata;


        ISendRPi sendrpi = new SendRPi();
        IReceiveRPi recieveRPi = new ReceiveRPi();
        ILocalDatabase localDatabase = new LocalDatabase();

        IDatabase database = new Database();

        //Read from file
        private ReadFromFile readFromFile = new ReadFromFile();

        public void DBcontrol()
        {
            
            
           
            
            measurementList = new List<DTO_Measurement>();

            //_database.GetMeasurement();


        }

        private Alarm alarm;
        private Filter filter;
        private Calibration calibration;
        private Mean mean;

        public void Mean()
        {
            
        }


        public List<DTO_Measurement> getmdata()
        {
            measurementdata = recieveRPi.ReceiveMeasurment();
            return measurementdata;
        }

        public List<DTO_Measurement> GetMeasurement(string socSecNB)
        {
            measurementList = database.GetMeasurement(socSecNB);
            return measurementList;
        }

        public bool getSocSecNB(string SocSecNB)
        {
            return database.getSocSecNB(SocSecNB);
            
        }

        public bool checkLogin(String socSecNb, String pw)
        {

            return database.isUserRegistered(socSecNb, pw);
        }

        public object sendRPiData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
            
            //localDatabase.SavePatientData(sysHigh, sysLow, diaHigh, diaLow, cprPatient);
            return sendrpi.sendpatientdata(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
        }

        public List<DTO_Measurement> ReadFromFile()
        {
            return readFromFile.Read();
        }

        public object sendEemergencydata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh,
            string CprPatient,
            double Calval, double Zeroval)
        {
            return localDatabase.ReadPatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
        }



    }


}
