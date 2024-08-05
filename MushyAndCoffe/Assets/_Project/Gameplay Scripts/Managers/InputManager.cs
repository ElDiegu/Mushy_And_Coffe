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
            DebugManager.StaticDebug(MessageTypes.Input, InputStruct.ToString());
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
        public PlayerInput PlayerInput { get; set; }
        public Vector2 Movement { get; set; }
        public bool Interact { get; set; }
        public bool Dash { get; set; }
        
        public InputStruct(PlayerInput playerInput) 
        {
            PlayerInput = playerInput;
            Movement = PlayerInput.actions["Movement"].ReadValue<Vector2>();
            Interact = PlayerInput.actions["Interaction"].triggered;
            Dash = PlayerInput.actions["Dash"].triggered;
        }
        
        public override string ToString() 
        {
            return $"Movement {Movement} | Interaction {Interact} | Dash {Dash}";
        }
    }
}
