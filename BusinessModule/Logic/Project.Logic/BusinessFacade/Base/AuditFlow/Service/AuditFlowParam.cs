using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula.Helper;
using Project.Logic.Domain;
using System.ComponentModel;
using Config.Logic;

namespace Project.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditFlowStartParam
    {
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AuditFormUrl
        {
            get;
            set;
        }

        List<AuditStep> _defsteps = new List<AuditStep>();
        /// <summary>
        /// 
        /// </summary>
        public List<AuditStep> DefSteps
        {
            get {
                return _defsteps;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string WBSID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AuditFormID
        {
            get;
            set;
        }
        bool execNext = true;
        /// <summary>
        /// 启动后，是否执行到下环节
        /// </summary>
        public bool ExecNext
        {
            get { return execNext; }
            set { execNext = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> DefStepsToCollection()
        {
            var result = new List<Dictionary<string, object>>();
            foreach (var item in this.DefSteps)
                result.Add(item.ToDic());
            return result;
        }

        public void AddStep(AuditStep step)
        {
            //如果没有执行信息，默认加提交
            if (step.Options == null)
                this.AddStep(step, AuditOption.Pass);
            else
            {
                this.AddStep(step,null);
            }
        }

        public void AddStep(AuditStep step, params AuditOption[] options)
        {
            var lastStep=this.DefSteps.OrderByDescending(c=>c.StepIndex).FirstOrDefault();
            if (step.StepIndex == 0)
            {
                if (lastStep == null)
                    step.StepIndex = 1;
                else
                    step.StepIndex = lastStep.StepIndex + 1;
            }
            //step.StepIndex = this.DefSteps.Count;
            if (options != null)
            {
                foreach (var item in options)
                    step.Options.Add(item);
            }
            this.DefSteps.Add(step);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AuditFlowExecuteParam
    {
        /// <summary>
        /// 
        /// </summary>
        public string WBSID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }

        public AuditStep NextStep
        {
            get;
            set;
        }

        public AuditOption ExecuteOption
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AuditStep
    {
        public AuditStep()
        { }

        public AuditStep(string Json)
        {
            var dic = JsonHelper.ToObject(Json);
            InitAuditStep(dic);
        }

        public AuditStep(Dictionary<string, object> dic)
        {
            InitAuditStep(dic);
        }

        private void InitAuditStep(Dictionary<string, object> dic)
        {
            this.UserID = dic.GetValue("UserID");
            this.UserName = dic.GetValue("UserName");
            this.StepKey = (ActivityType)Enum.Parse(typeof(ActivityType), dic.GetValue("StepKey"));
            if (!String.IsNullOrEmpty(dic.GetValue("StepIndex")))
                this.StepIndex = Convert.ToInt32(dic.GetValue("StepIndex"));
            if (!string.IsNullOrEmpty(dic.GetValue("MustStep")))
                this.MustStep = dic.GetValue("MustStep");

            if (!string.IsNullOrEmpty(dic.GetValue("CoSign")))
                this.CoSign = dic.GetValue("CoSign");

            if (!string.IsNullOrEmpty(dic.GetValue("CoSignRole")))
                this.CoSignRole = dic.GetValue("CoSignRole");

            if (!String.IsNullOrEmpty(dic.GetValue("AuditModel")))
            {
                this.AuditModel = (Project.Logic.AuditModel)Enum.Parse(typeof(Project.Logic.AuditModel), dic.GetValue("AuditModel"));
            }
            if (!String.IsNullOrEmpty(dic.GetValue("AuditRole")))
            {
                this.AuditRole = (Project.Logic.AuditRoles)Enum.Parse(typeof(Project.Logic.AuditRoles), dic.GetValue("AuditRole"));
            }
            var options = dic.GetValue("Options").Split(',');
            foreach (var option in options)
                this.Options.Add((AuditOption)Enum.Parse(typeof(AuditOption), option));
        }

        ActivityType stepKey = ActivityType.Design;
        public ActivityType StepKey
        {
            get { return stepKey; }
            set
            {
                stepKey = value;
                var name = EnumBaseHelper.GetEnumDescription(typeof(ActivityType), value.ToString());
                this.StepName = name;
            }
        }

        List<AuditOption> _options = new List<AuditOption>();
        public List<AuditOption> Options
        {
            get
            {
                return _options;
            }
            set
            {
                _options = value;
            }
        }

        string stepName;
        public string StepName
        {
            get { return this.stepName; }
            set { this.stepName = value; }
        }

        string userID = string.Empty;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        string userName = string.Empty;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        int stepIndex = 0;
        public int StepIndex
        {
            get { return stepIndex; }
            set { stepIndex = value; }
        }
        AuditModel auditModel = AuditModel.single;
        /// <summary>
        /// 执行方式，单签还是并签、串签（创建环节时，据此生成GroupID）
        /// </summary>
        public AuditModel AuditModel
        {
            get { return auditModel; }
            set { auditModel = value; }
        }

        AuditRoles auditRole;
        /// <summary>
        /// 参与校审的角色（某校审环节有多个角色并签时，以确认是那个角色）
        /// </summary>
        public AuditRoles AuditRole
        {
            get { return auditRole; }
            set { auditRole = value; }
        }

        string mustStep = "否";
        /// <summary>
        ///  是否必须环节
        /// </summary>
        public string MustStep
        {
            get { return mustStep; }
            set { mustStep = value; }
        }
        string coSign = "否";
        /// <summary>
        /// 是否会签
        /// </summary>
        public string CoSign
        {
            get { return coSign; }
            set { coSign = value; }
        }
        /// <summary>
        /// 会签角色
        /// </summary>
        public string CoSignRole
        {
            get;
            set;
        }
        public Dictionary<string, object> ToDic()
        {
            var dic = new Dictionary<string, object>();
            dic.SetValue("StepKey", StepKey.ToString());
            dic.SetValue("StepName", StepName.ToString());
            dic.SetValue("UserID", UserID);
            dic.SetValue("UserName", UserName);
            dic.SetValue("Options",String.Join<AuditOption>(",",this.Options));
            dic.SetValue("StepIndex", StepIndex);
            dic.SetValue("AuditModel", AuditModel.ToString());
            dic.SetValue("AuditRole", AuditRole.ToString());
            dic.SetValue("MustStep", MustStep);
            dic.SetValue("CoSign", CoSign);
            dic.SetValue("CoSignRole", CoSignRole);
            return dic;
        }
    }

    public class ExeResult
    {
        List<S_W_Activity> activities = new List<S_W_Activity>();
        public List<S_W_Activity> Activities
        {
            get
            {
                return activities;
            }
        }

        bool isCancel = false;
        public bool IsCancel
        {
            get { return isCancel; }
            set { isCancel = value; }
        }

        public bool IsComplete
        {
            get;
            set;
        }
        string execStatus = "Success";
        /// <summary>
        /// 执行状态，Success、Wait
        /// </summary>
        public string ExecStatus
        {
            get { return execStatus; }
            set { execStatus = value; }
        }
    }

    public class AuditConfigParam
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableKey
        {
            get;
            set;
        }
        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnKey
        {
            get;
            set;
        }
        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyKey
        {
            get;
            set;
        }

    }
    
}
