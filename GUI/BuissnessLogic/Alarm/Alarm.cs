using System.Collections.Generic;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{
    public class Alarm : IAlarm
    {
        IReceiveRPi receiveRPi=new ReceiveRPi();
        public List<DTO_Measurement> AlarmData()
        {
            List<DTO_Measurement> alarmList =new List<DTO_Measurement>();

            //alarmList.Add(receiveRPi.ReceiveMeasurment());
            return alarmList;
        }


      

        public void StartAlarm()
        {
            throw new System.NotImplementedException();
        }

        public void MuteAlarm()
        {
            throw new System.NotImplementedException();
        }
    }

}