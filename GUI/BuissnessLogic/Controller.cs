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
        private List<DTO_Measurement> measurementList;
        private List<DTO_Measurement> fiveseconddata;
        private List<DTO_Measurement> measurementdata;
        private double data;
        private List<DTO_CalVal> caldata;
        public bool closeListener { get; set; }
        

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


        public void openrecieveports()
        {
            recieveRPi.OpenRecievePorts();
        }

       
        public List<DTO_Measurement> getmdata()
        {
            
                measurementdata = recieveRPi.test();
               
                
            //measurementdata = recieveRPi.ReceiveMeasurment();
            
           
                return measurementdata;
            
           
        }
        public List<DTO_Measurement> getmdata2()
        {

           // measurementdata = recieveRPi.test();


            measurementdata = recieveRPi.ReceiveMeasurment();


            return fiveseconddata;


        }

        public double RecieveDouble()
        {
            data = recieveRPi.Recievedouble();
            return data;
        }

        
        public List<DTO_CalVal> GetCalVal()
        {
            caldata = localDatabase.GetCalVal();
            return caldata;

        }

        public List<DTO_Measurement> GetMeasurement(string socSecNB)
        {
            measurementList = database.GetMeasurement(socSecNB);
            
            return measurementList;
        }

        public bool getSocSecNB(string SocSecNB)
        {
            return localDatabase.getSocSecNB(SocSecNB);
            
        }

        public bool checkLogin(String socSecNb, String pw)
        {
            
            return localDatabase.isUserRegistered(socSecNb, pw);
        }

        public object sendRPiData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
            
            
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

        
        public string Command(string command)
        {
            return sendrpi.Command(command);
        }

      
        

    }


}
