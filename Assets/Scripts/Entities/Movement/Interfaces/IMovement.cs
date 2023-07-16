using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Movement.Interfaces
{
    internal interface IMovement
    {
        bool IsMoving { get; }
        void MoveToTarget(bool stopIfSafeDistance = true);
        void Stop();
        void SetMoveDirection(Vector2 vector);
    }
}
