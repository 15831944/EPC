using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocSystem.Logic.Domain
{
    public partial class S_DOC_AtlasConfigDetail
    {
        public void MoveUp(string type)
        {
            var preAttr = this.S_DOC_File.S_DOC_AtlasConfigDetail.Where(d => d.Type == type && d.DetailSort < this.DetailSort).OrderByDescending(d => d.DetailSort).FirstOrDefault();
            if (preAttr == null) return;
            int sort = this.DetailSort;
            this.DetailSort = preAttr.DetailSort;
            preAttr.DetailSort = sort;
        }

        public void MoveDown(string type)
        {
            var aftAttr = this.S_DOC_File.S_DOC_AtlasConfigDetail.Where(d => d.Type == type && d.DetailSort > this.DetailSort).OrderBy(d => d.DetailSort).FirstOrDefault();
            if (aftAttr == null) return;
            int sort = this.DetailSort;
            this.DetailSort = aftAttr.DetailSort;
            aftAttr.DetailSort = sort;
        }
    }
}
