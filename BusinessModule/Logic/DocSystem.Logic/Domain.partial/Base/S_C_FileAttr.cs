﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DocSystem.Logic.Domain
{
    public partial class S_DOC_FileAttr
    {
        public IControl GetCtontrol(bool isUpVersion = false, bool fromQuery = false)
        {
            IControl control = ControlGenrator.GenrateMiniControl(this.InputType);
            control.Name = this.FileAttrField;
            if (this.InputType.IndexOf(ControlType.ButtonEdit.ToString()) >= 0)
            {
                control.SetAttribute("textName", this.FileAttrField + "Name");
                if (fromQuery)
                {
                    control = ControlGenrator.GenrateMiniControl(ControlType.TextBox.ToString());
                    control.Name = this.FileAttrField + "Name";
                }
                else if (this.MultiSelect == "True")
                    control.SetAttribute("multiSelect", "true");
            }
            if (this.IsEnum == TrueOrFalse.True.ToString())
            {
                string enumKey = this.EnumKey;
                if (enumKey.Split('.').Length > 1)
                    enumKey = enumKey.Split('.')[1];
                control.SetAttribute("data", enumKey);
                if (this.MultiSelect == "True")
                {
                    control.SetAttribute("multiSelect", "true");
                }
                if (!String.IsNullOrEmpty(this.TextFieldName))
                {
                    control.SetAttribute("textName", this.TextFieldName);
                }
            }
            if (!fromQuery)
            {
                if (!String.IsNullOrEmpty(this.VType))
                    control.SetAttribute("vtype", this.VType);
                if (this.Required == TrueOrFalse.True.ToString())
                    control.SetAttribute("required", "true");
                if (this.Disabled == TrueOrFalse.True.ToString())
                    control.SetAttribute("enabled", "false");
            }
            if (isUpVersion)
                control.SetAttribute("enabled", "false");
            return control;
        }
    }
}
