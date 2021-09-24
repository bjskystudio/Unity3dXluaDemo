using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class LabelAttribute : PropertyAttribute
    {
        public string Name { get; private set; }

        public LabelAttribute(string name)
        {
            this.Name = name;
        }
    }

}
