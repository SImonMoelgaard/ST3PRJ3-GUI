using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{
    public class Alarm : IAlarm
    {
        //Producer
        private readonly BlockingCollection<DataContainer> _dataQueue;

        IReceiveRPi receiveRPi=new ReceiveRPi();
        ISendRPi sendRPi=new SendRPi();

        public Alarm(BlockingCollection<DataContainer> dataQueue)
        {
            _dataQueue = dataQueue;
        }

        public void Run()
        {
            List<DTO_Measurement> alarms = new List<DTO_Measurement>();
            while (true)
            {
                alarms = receiveRPi.ReceiveMeasurment();
                bool high = false;
                foreach (var alarm in alarms)
                {
                    high = alarm.HighSys;
                }

                DataContainer reading = new DataContainer();

                reading.SetHighSys(high);
                _dataQueue.Add(reading);
                Thread.Sleep(10);
            }
            _dataQueue.CompleteAdding();
        }
        //Producer slut

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
            //
            
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