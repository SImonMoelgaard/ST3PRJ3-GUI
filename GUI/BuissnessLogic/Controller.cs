using System;
using System.Collections.Generic;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{

    
    public class Controller
    {
        private List<DTO_Measurement> measurementList;

        
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


        public void getPowerValue(int power)
        {

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

        public bool saveData(int sysHigh, int sysLow, int diaHigh, int diaLow, string cprPatient)
        {
            return localDatabase.SavePatientData(sysHigh, sysLow, diaHigh, diaLow, cprPatient);
        }
    }


}
