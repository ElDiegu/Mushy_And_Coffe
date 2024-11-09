using MushyAndCoffe.Player;
using UnityEngine;

namespace MushyAndCoffe
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] private MovementManager movementManager;
        [SerializeField] private Animator animator;

        private void FixedUpdate()
        {
            animator.SetFloat("Speed", movementManager.playerSpeed.magnitude);
        }
    }
}
