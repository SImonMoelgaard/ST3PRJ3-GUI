﻿using System.Collections.Generic;
using DTO;

namespace DataAccessLogic
{
    public class LocalDatabase : IDatabase
    {
        public List<DTO_Measurement> GetMeasurement(string socSecNB)
        {
            throw new System.NotImplementedException();
        }

        public void SaveMeasurement()
        {
            throw new System.NotImplementedException();
        }

        public bool isUserRegistered(string socSecNb, string pw)
        {
            throw new System.NotImplementedException();
        }

        public bool getSocSecNB(string SocSecNB)
        {
            throw new System.NotImplementedException();
        }
    }
}