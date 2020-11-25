using System.ComponentModel;
using System.Reflection;
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

        

        public object sendpatientdata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient, double Calval, double Zeroval);
    }
}