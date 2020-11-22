using DTO;

namespace DataAccessLogic
{
    public interface ISendRPi
    {
        public void Start();
        public void Stop();
        public string StartCalibration();
        public void StartZeroing();
        public void MuteRPi();

        public void sendA();

        public object sendpatientdata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, string CprPatient);
    }
}