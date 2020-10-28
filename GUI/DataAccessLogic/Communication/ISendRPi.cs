namespace DataAccessLogic
{
    public interface ISendRPi
    { 
        public void Start(){}
        public void Stop(){}
        public void StartCalibration(){}
        public void StartZeroing(){}
        public void MuteRPi(){}
    }
}