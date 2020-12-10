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


        public List<DTO_Measurement> AlarmData()
        {
            throw new System.NotImplementedException();
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