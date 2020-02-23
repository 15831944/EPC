using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;

using System.Collections;
using Newtonsoft.Json;


namespace DocSystem.Logic.Domain
{
    public partial class S_DOC_FileAttr
    {
        [NotMapped]
        [JsonIgnore]
        public bool IsFullRow
        {
            get
            {
                var result = false;
                if (this.InputType == ControlType.ComboboxFullRow.ToString() ||
                    this.InputType == ControlType.TextAreaFullRow.ToString() ||
                    this.InputType == ControlType.TextBoxFullRow.ToString() ||
                    this.InputType == ControlType.SingleFileFullRow.ToString() ||
                    this.InputType == ControlType.MultiFileFullRow.ToString() ||
                    this.InputType == ControlType.ButtonEditFullRow.ToString())
                    result = true;
                return result;
            }
        }

        public void MoveUp()
        {
            var preAttr = this.S_DOC_File.S_DOC_FileAttr.Where(d => d.AttrSort < this.AttrSort).OrderByDescending(d => d.AttrSort).FirstOrDefault();
            if (preAttr == null) return;
            int sort = this.AttrSort;
            this.AttrSort = preAttr.AttrSort;
            preAttr.AttrSort = sort;
        }

        public void MoveDown()
        {
            var aftAttr = this.S_DOC_File.S_DOC_FileAttr.Where(d => d.AttrSort > this.AttrSort).OrderBy(d => d.AttrSort).FirstOrDefault();
            if (aftAttr == null) return;
            int sort = this.AttrSort;
            this.AttrSort = aftAttr.AttrSort;
            aftAttr.AttrSort = sort;
        }
    }

    [NotMapped]
    public class S_DOC_FileAttrDefault : S_DOC_FileAttr
    {
        public bool IsEdit { get; set; }
        public string DBItemNullStr { get; set; }
        public S_DOC_FileAttrDefault()
        {
            this.InputType = "";
            this.AttrType = DocSystem.Logic.Domain.AttrType.System.ToString();
            this.IsEnum = TrueOrFalse.False.ToString();
            this.EnumKey = "";
            this.ValidateType = DocSystem.Logic.Domain.ValidateType.None.ToString();
            this.Required = TrueOrFalse.False.ToString();
            this.Disabled = TrueOrFalse.True.ToString();
            this.Visible = TrueOrFalse.False.ToString();
            this.VType = "";
            this.IsEdit = false;
            this.DBItemNullStr = " NULL";
        }
    }
}
