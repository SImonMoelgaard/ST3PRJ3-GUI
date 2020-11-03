using System;
using DataAccessLogic;

namespace BuissnessLogic
{

    
    public class Controller
    {
        private Database database;
        private LocalDatabase localDatabase;

        public DBcontrol(Database _database, LocalDatabase _localDatabase)
        {
            _localDatabase = localDatabase;
            _database = database;
            database=new Database();
            localDatabase = new LocalDatabase();

            _database.GetMeasurement();
            return;

        }

        private Alarm alarm;
        private Filter filter;
        private Calibration calibration;
        private Mean mean;

        

        public void getPowerValue(int power){}


    }


}
