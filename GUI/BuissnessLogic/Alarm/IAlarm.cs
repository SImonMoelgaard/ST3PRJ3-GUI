using System.Collections.Generic;
using DTO;

namespace BuissnessLogic
{
    public interface IAlarm
    {
        public List<DTO_Measurement> AlarmData()
        {
            return AlarmData();
        }
        public void StartAlarm(){}
        public void MuteAlarm(){}
    }
}