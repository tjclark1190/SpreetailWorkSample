using System;
using System.Collections.Generic;
using System.Text;

namespace SpreetailWorkSample.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute: Attribute
    {
        private string name;
        public CommandAttribute(string name)
        {
            this.name = name;           
        }
    }
}
