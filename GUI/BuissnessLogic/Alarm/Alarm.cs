using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{
    public class Alarm : IAlarm
    {

        readonly ISendRPi sendRPi = new SendRPi();

        /// <summary>
        /// Command to mute alarm
        /// </summary>
        public void MuteAlarm()
        {
            sendRPi.Command("Mutealarm");
        }
    }

}