using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatAttributes
{
    internal class AttributeHolder
    {
        private Attribute[] _attributes;
        public bool TryGetAttribute<T>(out Attribute attribute) where T : Attribute
        {
            attribute = null;
            foreach (var attrib in _attributes)
            {
                if (attrib is T)
                {
                    attribute = attrib;
                    return true;
                }
            }
            return false;
        }
        public AttributeHolder(params Attribute[] attributes)
        {
            _attributes = attributes;
        }
    }
}
