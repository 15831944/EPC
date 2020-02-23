using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DocSystem.Logic
{
    public class MiniButtonEdit : BaseControl
    {
        string htmlTemplate = "<input {0} class=\"mini-buttonedit\"  />";

        public MiniButtonEdit()
        {
            this.AllowInput = true;
            this.Required = false;
        }

        public MiniButtonEdit(string name)
            : this()
        {
            this.Name = name;
            this.TextName = name + "Name";
        }

        string _style = " width: 100% ";
        public override string Style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
            }
        }

        public string VType
        {
            get
            {
                if (this.Attributes.ContainsKey("vtype") && !Tool.IsNullOrEmpty(this.Attributes["vtype"]))
                    return this.Attributes["vtype"].ToString();
                else return "";
            }
            set { this.Attributes["vtype"] = value; }
        }

        public string TextName
        {
            get
            {
                if (this.Attributes.ContainsKey("textName") && !Tool.IsNullOrEmpty(this.Attributes["textName"]))
                    return this.Attributes["textName"].ToString();
                else return "";
            }
            set { this.Attributes["textName"] = value; }
        }
        
        public bool AllowInput
        {
            get
            {
                if (this.Attributes.ContainsKey("allowinput") && !Tool.IsNullOrEmpty(this.Attributes["allowinput"]))
                    return Convert.ToBoolean(this.Attributes["allowinput"].ToString());
                else return false;
            }
            set { this.Attributes["allowinput"] = value; }
        }
        
        public bool Required
        {
            get
            {
                if (this.Attributes.ContainsKey("required") && !Tool.IsNullOrEmpty(this.Attributes["required"]))
                    return Convert.ToBoolean(this.Attributes["required"].ToString());
                else return false;
            }
            set
            {
                if (value)
                    this.Attributes["required"] = value;
                else
                {
                    if (this.Attributes.ContainsKey("required"))
                        this.Attributes.Remove("required");
                }
            }
        }

        public override string Render()
        {
            if (String.IsNullOrEmpty(this.Name))
                throw new Formula.Exceptions.BusinessException("必须指定 弹出选择 控件的 name 属性");           
            string attr = this.getAttr();
            if (this.Required)
                attr += " required=\"true\" ";
            //if (String.IsNullOrEmpty(Url)) throw new Formula.Exceptions.BusinessException("未指定枚举的查询地址，无法绘制combobox控件");
            if (String.IsNullOrEmpty(this.TextName)) throw new Formula.Exceptions.BusinessException("未指定TextName字段，无法绘制弹出选择控件");
            return String.Format(htmlTemplate, attr);
        }
    }
}
