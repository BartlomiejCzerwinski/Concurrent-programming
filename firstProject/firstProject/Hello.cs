using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstProject
{
    public class Hello
    {
        public string returnHello(bool condition)
        {
            if(condition)
            {
                return "HELLO!";
            }
            else
            {
                return "BYE!";
            }
        }
    }
}
