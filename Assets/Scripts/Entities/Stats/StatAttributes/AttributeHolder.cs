using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.StatAttributes
{
    internal class AttributeHolder
    {
        public StatModifierHandler ModifierHolder { get; private set; }
        private Stat[] _attributes;
        public bool Modify<T>(AttributeMask mask) where T : Stat
        {
            bool modified = TryGetAttribute<T>(out var but);
            if (modified) ModifyAttrib(but, mask);
            return modified;
        }
        public void Unmodify<T>(AttributeMask mask) where T : Stat
        {
            if(TryGetAttribute<T>(out var but)) UnmodifyAttrib(but, mask);
        }
        private void ModifyAttrib(Stat attribute, AttributeMask mask)
        {
            attribute.MultiplierMask += mask.MaskMultiplier;
            attribute.BaseMultiplier += mask.BaseMultiplier;
            attribute.BaseValue += mask.BaseValue;
        }
        private void UnmodifyAttrib(Stat attribute, AttributeMask mask)
        {
            attribute.MultiplierMask -= mask.MaskMultiplier;
            attribute.BaseMultiplier -= mask.BaseMultiplier;
            attribute.BaseValue -= mask.BaseValue;
        }
        public float GetValue<T>() where T : Stat => GetAttribute<T>().GetValue();
        public int GetValueInt<T>() where T : Stat => (int)GetAttribute<T>().GetValue();
        public T GetAttribute<T>() where T : Stat
        {
            if(TryGetAttribute<T>(out var attrib))
            {
                return (T)attrib;
            }
            throw new NullReferenceException($"Attribute of type {typeof(T)} not implemented");
        }
        public bool TryGetAttribute<T>(out Stat attribute) where T : Stat
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
        public void OnUpdate() => ModifierHolder.OnUpdate();
        public AttributeHolder(params Stat[] attributes)
        {
            ModifierHolder = new();
            _attributes = attributes;
        }
    }
}
