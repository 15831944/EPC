using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leaflet.Adaptor.DTO
{    
    /// <summary>
    /// 用户
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Ico { get; set; }
    }
}
