using System;

namespace SuperTerminal.FeildCheck
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class FeildCheckAttribute : Attribute
    {
        public string ErrorMsg { get; set; }
    }
}
