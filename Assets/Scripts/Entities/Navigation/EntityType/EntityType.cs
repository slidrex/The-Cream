using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Navigation.EntityType
{
    internal class EntityType<T> : EntityTypeBase where T : Enum
    {
        private T[] _types;
        public bool IsAny { get; private set; }
        public T[] GetTags()
        {
            return _types;
        }
        public EntityType(params T[] tags)
        {
            _types = tags;
        }
        public EntityType<T> Any()
        {
            IsAny = true;
            return new EntityType<T>(Enum.GetValues(typeof(T)).Cast<T>().ToArray());
        }
    }
}
