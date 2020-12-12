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
        readonly ISendRPi sendrpi = new SendRPi();
        readonly IReceiveRPi recieveRPi = new ReceiveRPi();
        readonly ILocalDatabase localDatabase = new LocalDatabase();
        readonly IDatabase database = new Database();
        private List<DTO_Measurement> measurementList;
        private List<DTO_Measurement> measurementdata;
        private double data;
        private List<DTO_CalVal> caldata;
        
        /// <summary>
        /// This method opens the receiving and sending ports
        /// </summary>
        public void openrecieveports()
        {
            sendrpi.OpenSendPorts();
            recieveRPi.OpenRecievePorts();
        }

        /// <summary>
       /// This method gets the measurement data points
       /// </summary>
       /// <returns></returns>
        public List<DTO_Measurement> GetMeasurementData()
        {
            measurementdata = recieveRPi.RecieveDataPoints();

            return measurementdata;
        }
        
        /// <summary>
        /// This method received either zero value og calibration value
        /// </summary>
        /// <returns></returns>
        public double RecieveDouble()
        {
            data = recieveRPi.RecieveCalculatedValues();
            return data;
        }

        /// <summary>
        /// This method gets the calibration value from the local database
        /// </summary>
        /// <returns></returns>
        public List<DTO_CalVal> GetCalVal()
        {
            caldata = localDatabase.GetCalVal();
            return caldata;

        }

        /// <summary>
        /// This method returns an earlier measurement for a given patient
        /// </summary>
        /// <param name="socSecNB"></param>
        /// <returns></returns>
        public List<DTO_Measurement> GetMeasurementLocal(string socSecNB)
        {
            measurementList = database.GetMeasurement(socSecNB);
            
            return measurementList;
        }

        /// <summary>
        /// This method returns a patient number from the database
        /// </summary>
        /// <param name="SocSecNB"></param>
        /// <returns></returns>
        public bool GetSocSecNb(string SocSecNB)
        {
            return localDatabase.getSocSecNB(SocSecNB);
        }

        /// <summary>
        /// This method checks if the user name and password is correct
        /// </summary>
        /// <param name="socSecNb"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool CheckLogin(String socSecNb, String pw)
        {
            return localDatabase.isUserRegistered(socSecNb, pw);
        }

        /// <summary>
        /// This method sends the data to the RPi
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
        public object SendRPiData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
            return sendrpi.sendpatientdata(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
        }


        /// <summary>
        /// This method sends the emergency data to the RPi
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
        public object SendEemergencydata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh,
            string CprPatient,
            double Calval, double Zeroval)
        {
            return localDatabase.ReadPatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
        }

        /// <summary>
        /// This method sends a command to the RPi
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string Command(string command)
        {
            return sendrpi.Command(command);
        }
    }
}
