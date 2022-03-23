using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Manager
{
    public class UnauthorizedException:Exception
    {
        public UnauthorizedException(string? msg) : base(msg)
        { 
            
        }
    }
}
