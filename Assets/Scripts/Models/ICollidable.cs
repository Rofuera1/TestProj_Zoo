using System;
using UnityEngine;

namespace Core
{
    public interface ICollidable
    {
        public Action<Collision> OnCollided { get; set; }
        public Action<Collider> OnTriggerCollided { get; set; }
    }
}