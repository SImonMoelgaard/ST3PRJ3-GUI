using System;
using System.Collections.Generic;
using System.Text;

namespace ST3Prj3DomaineCore.Models.DTO
{
    public class DTO_Send
    {
        public int ComNo { get; set; }
        public string sendID { get; set; }

        public override string ToString()
        {
            return string.Format("Rx : (ComNo: {0}, sendId: {1})", ComNo, sendID);
        }
    }
}
