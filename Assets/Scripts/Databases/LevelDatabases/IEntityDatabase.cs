using Assets.Scripts.Databases.dto.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Databases.LevelDatabases
{
    internal interface IEntityDatabase<T> where T : EntityModel
    {
        List<T> Entities { get; }
    }
}
