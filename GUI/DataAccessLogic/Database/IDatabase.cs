using System;
using System.Collections.Generic;
using DTO;

namespace DataAccessLogic
{
    public interface IDatabase
    {
        public List<DTO_Measurement> GetMeasurement(string socSecNB);
        public bool isUserRegistered(String socSecNb, String pw);
        public bool getSocSecNB(string SocSecNB);
    }
}