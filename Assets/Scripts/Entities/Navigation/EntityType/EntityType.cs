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

        public override bool MatchesTag(Entity entity)
        {
            if (entity.ThisType is EntityType<T> type)
            {
                if (_types.Length == 0) return true;
                foreach (var tag in type.GetTags())
                {
                    foreach (var secondTag in _types)
                    {
                        if (tag.ToString().Equals(secondTag.ToString())) return true;
                    }
                }
            }
            return false;
        }
        public override bool MatchesEntityType(Entity entity)
        {
            if (entity.ThisType is EntityType<T> type)
            {
                return true;
            }
            return false;
        }
    }
}
