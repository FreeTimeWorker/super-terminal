﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.FeildCheck
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =true)]
    public class FeildCheckAttribute: Attribute
    {
    }
}
