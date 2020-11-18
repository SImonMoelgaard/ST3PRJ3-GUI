using DTO;

namespace DataAccessLogic
{
    public interface IReceiveRPi
    {
        public DTO_Measurement ReceiveMeasurment();

        public DTO_CalVal ReceiveCalibration();


    }
}