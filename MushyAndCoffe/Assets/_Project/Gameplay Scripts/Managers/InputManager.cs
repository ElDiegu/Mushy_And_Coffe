using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace MushyAndCoffe.Managers
{
    public class InputManager : PersistentSingleton<InputManager>
    {
        [SerializeField] private PlayerInput playerInput;
        public InputStruct InputStruct { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            if (!playerInput) playerInput = GetComponent<PlayerInput>();
            InputStruct = new InputStruct(playerInput);
        }
        
        private void Update()
        {
            GatherInput();
            DebugManager.Log(MessageTypes.Input, InputStruct.ToString());
        }
        
        private void GatherInput() 
        {
            InputStruct = new InputStruct(playerInput);
        }
        
        public InputStruct GetInput() 
        {
            return InputStruct;
        }
    }
    
    public struct InputStruct
    {
        public PlayerInput PlayerInput { get; private set; }
        public Vector2 Movement { get; private set; }
        public bool Interact { get; private set; }
        public bool Dash { get; private set; }
        public bool LeftClick { get; private set; }
        public bool RotateObject { get; private set; }
        public bool OpenInterface { get; private set; }
        
        public InputStruct(PlayerInput playerInput) 
        {
            PlayerInput = playerInput;
            Movement = PlayerInput.actions["Movement"].ReadValue<Vector2>();
            Interact = PlayerInput.actions["Interaction"].triggered;
            Dash = PlayerInput.actions["Dash"].triggered;
            LeftClick = PlayerInput.actions["LeftClick"].triggered;
            RotateObject = PlayerInput.actions["RotateObject"].triggered;
            OpenInterface = PlayerInput.actions["OpenInterface"].triggered;
        }
        
        public override string ToString() 
        {
            return $"Movement {Movement} | Interaction {Interact} | Dash {Dash}";
        }
    }
}
