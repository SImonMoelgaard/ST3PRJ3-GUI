using System;
using System.Collections.Generic;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{

    
    public class Controller
    {
        private List<DTO_Measurement> measurementList;

        
        ISendRPi sendrpi = new SendRPi();

        private LocalDatabase localDatabase;
        IDatabase database = new Database();

        public void DBcontrol()
        {
            
            
            localDatabase = new LocalDatabase();
            
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


        public List<DTO_Measurement> GetMeasurmentdata(string socSecNb, double rawData, DateTime date, int sysData, int diaData,
            int alarmData, int pulse, int powerData)
        {
            List<DTO_Measurement> measurmentdata = new DTO_Measurement(socSecNb, rawData, date, sysData, diaData, alarmData, pulse, powerData);
            return measurmentdata;
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

        public object saveData(int sysHigh, int sysLow, int diaHigh, int diaLow, String cprPatient)
        {
            
            //localDatabase.SavePatientData(sysHigh, sysLow, diaHigh, diaLow, cprPatient);
            return sendrpi.sendpatientdata(sysHigh, sysLow, diaHigh, diaLow, cprPatient);
        }
    }


}
