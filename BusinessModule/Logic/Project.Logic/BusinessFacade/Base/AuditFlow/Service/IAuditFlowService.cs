using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Logic.Domain;

namespace Project.Logic
{
    public class AuditFlowServiceGenretor
    {
        public static IAuditFlowService CreateService()
        {
            return new CustomAuditFlowService();
        }
    }

    public interface IAuditFlowService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startParam"></param>
        /// <returns></returns>
        List<S_W_Activity> StartAuditFlow(AuditFlowStartParam startParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exeParam"></param>
        /// <returns></returns>
        ExeResult Execute(S_W_Activity activity, AuditFlowExecuteParam exeParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="exeOption"></param>
        /// <returns></returns>
        ExeResult Execute(S_W_Activity activity, string exeOption, string SelectUserData);
    }
}
