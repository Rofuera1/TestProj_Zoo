using System;
using UnityEngine;

namespace Core
{
    public interface ICollidable
    {
        public event Action<Collision> OnCollided;
        public event Action<Collider> OnTriggerCollided;
    }
}