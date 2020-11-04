using System;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{

    
    public class Controller
    {
        private Database database;
        private LocalDatabase localDatabase;

        public void DBcontrol(Database _database, LocalDatabase _localDatabase)
        {
            _localDatabase = localDatabase;
            _database = database;
            database=new Database();
            localDatabase = new LocalDatabase();

            _database.GetMeasurement();
            

        }

        private Alarm alarm;
        private Filter filter;
        private Calibration calibration;
        private Mean mean;

        public void mean(DTO_Measurement)
        {
            
        }
        

        public void getPowerValue(int power){}


    }


}
