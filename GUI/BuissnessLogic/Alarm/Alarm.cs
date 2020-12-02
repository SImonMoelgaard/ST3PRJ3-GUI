using System.Collections.Generic;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{
    public class Alarm : IAlarm
    {
        IReceiveRPi receiveRPi=new ReceiveRPi();
        ISendRPi sendRPi=new SendRPi();

        public List<DTO_Measurement> AlarmData()
        {
            List<DTO_Measurement> alarmList = new List<DTO_Measurement>();
            List<bool> alarms = new List<bool>();

            while (true)
            {
                alarmList = receiveRPi.ReceiveMeasurment();

                foreach (var alarm in alarmList)
                {
                    alarms.Add(alarm.HighDia);
                    
                }
            }
            
            
            //alarmList.Add(receiveRPi.ReceiveMeasurment());
            return alarmList;
        }


      

        public void StartAlarm()
        {
            throw new System.NotImplementedException();
        }

        public void MuteAlarm()
        {
            sendRPi.Command("Mutealarm");
        }
    }

}