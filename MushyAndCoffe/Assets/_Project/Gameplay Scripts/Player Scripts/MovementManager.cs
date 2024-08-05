using MushyAndCoffe.Managers;
using UnityEngine;

namespace MushyAndCoffe.Player
{
    public class MovementManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private Transform orientation;

        [Header("Movemen Manager Variables")]
        [SerializeField] private Vector3 playerSpeed;
        float currentSpeed;

        private void Awake()
        {
            if (!mainCamera) mainCamera = Camera.main;
            if (!rb) rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            ComputeMovement();
            Rotate();

            ApplyMovement();
        }

        private void ComputeMovement()
        {
            var input = InputManager.Instance.GetInput();
            var moveDirection = (orientation.forward * input.Movement.y + orientation.right * input.Movement.x).normalized;
            
            
            if (input.Movement.magnitude != 0)
                currentSpeed = Mathf.MoveTowards(currentSpeed, PlayerParameters.Instance.maxSpeed, PlayerParameters.Instance.acceleration * Time.deltaTime);
            else
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, PlayerParameters.Instance.deceleration * Time.deltaTime);
            
            playerSpeed = moveDirection * currentSpeed;
            
            //DebugManager.StaticDebug(MessageTypes.Movement, $"{currentSpeed}");
        }

        private void Rotate() 
        {
            var input = inputManager.GetInput();
            
            Vector3 viewDirection = transform.position - new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
            orientation.forward = viewDirection.normalized;
            
            Vector3 inputDirection = (orientation.forward * input.Movement.y + orientation.right * input.Movement.x).normalized;

            if (input.Movement != Vector2.zero) 
                transform.forward = Vector3.Slerp(transform.forward, inputDirection, PlayerParameters.Instance.turnSpeed * Time.deltaTime);
        }

        private void ApplyMovement()
        {
            rb.linearVelocity = playerSpeed;
        }
    }
}
