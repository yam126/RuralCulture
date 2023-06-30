using RuralCultureDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class OrganizationTestParameter
    {
        public Organization orgData { get; set; }

        public List<OrganizationTestParameter> orgList { get; set; }
    }
}
