using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.appsettings
{
    public class AppSettingRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> exclude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}
