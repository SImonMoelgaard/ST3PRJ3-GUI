using System;
using System.Collections.Generic;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{

    
    public class Controller
    {
        private List<DTO_Measurement> measurementList;
        private DTO_Measurement measurementdata;


        ISendRPi sendrpi = new SendRPi();
        IReceiveRPi recieveRPi = new ReceiveRPi();

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


        public DTO_Measurement getmdata(string socSecNb, double rawData, DateTime date, int sysData, int diaData, int alarmData, int pulse, int powerData)
        {
            measurementdata = recieveRPi.ReceiveMeasurment(socSecNb, rawData, date, sysData, diaData, alarmData, pulse, powerData);
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

        public object saveData(int sysHigh, int sysLow, int diaHigh, int diaLow, String cprPatient)
        {
            
            //localDatabase.SavePatientData(sysHigh, sysLow, diaHigh, diaLow, cprPatient);
            return sendrpi.sendpatientdata(sysHigh, sysLow, diaHigh, diaLow, cprPatient);
        }
    }


}
