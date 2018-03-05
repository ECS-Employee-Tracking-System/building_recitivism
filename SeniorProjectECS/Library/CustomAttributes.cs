using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Library
{
    public class DefaultAction : Attribute { }
    public class AdminOnly : Attribute { }
    public class ViewOnly : Attribute { }
}
