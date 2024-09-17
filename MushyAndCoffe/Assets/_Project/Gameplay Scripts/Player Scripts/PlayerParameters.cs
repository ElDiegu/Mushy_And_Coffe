using UnityEngine;

namespace MushyAndCoffe.Player
{
    public class PlayerParameters : PersistentSingleton<PlayerParameters>
    {
        [Header("Movement Parameters")]
        public float acceleration = 1f;
        public float deceleration = 2f;
        public float maxSpeed = 5f;
        public float dashStrength = 5f;
        public float turnSpeed = 360f;
  
        [Header("Interaction Parameters")]      
        public float interactionDistance = 1f;
        public float interactionRadius = 1f;
        public LayerMask interactionLayerMask;

    }
}
