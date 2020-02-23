using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class S_A_User
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string SynID { get; set; }
        public DateTime? SynTime { get; set; }
        public string SynData { get; set; }
    }
    public class UserRequestData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string loginid { get; set; }
        public string signature { get; set; }
        public bool enable { get; set; }
    }
    public class DeleteRequestData
    {
        public string id { get; set; }
    }
}
