using System.Collections.Generic;

namespace BuissnessLogic
{
    public interface IAlarm
    {
        public List<DTO_Alarm> getAlarm() {}
        public void StartAlarm(){}
        public void MuteAlarm(){}
    }
}