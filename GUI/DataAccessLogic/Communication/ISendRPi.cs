using System.ComponentModel;
using System.Reflection;
using DTO;

namespace DataAccessLogic
{
    public interface ISendRPi
    {
        public string Command(string command);
       

        public object sendemergencydata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient, double Calval, double Zeroval);

        

        public object sendpatientdata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient, double Calval, double Zeroval);
        public void OpenSendPorts();
    }
}