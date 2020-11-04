using System;
using System.Collections.Generic;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{

    
    public class Controller
    {
        private List<DTO_Measurement> measurementList;

        private Database database;
        private LocalDatabase localDatabase;

        public void DBcontrol(Database _database, LocalDatabase _localDatabase)
        {
            _localDatabase = localDatabase;
            _database = database;
            database=new Database();
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
    }


}
