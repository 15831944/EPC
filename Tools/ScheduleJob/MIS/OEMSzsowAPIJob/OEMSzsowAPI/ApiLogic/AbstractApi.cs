using OEMSzsowAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEMSzsowAPI.ApiLogic
{
    public abstract class AbstractApi
    {
        public abstract void Sync();
        
        public abstract void SaveLogic(string businessID);

        public abstract void RemoveLogic(string businessID);
    }
}
