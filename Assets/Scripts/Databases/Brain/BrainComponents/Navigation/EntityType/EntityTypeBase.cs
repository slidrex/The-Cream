using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Navigation.EntityType
{
    public abstract class EntityTypeBase
    {
        public abstract bool MatchesEntityType(Entity entityType);
        public abstract bool MatchesTag(Entity entityType);
        public abstract bool MatchesTag(EntityTypeBase entityType);
    }
}
