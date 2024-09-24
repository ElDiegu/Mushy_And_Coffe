using UnityEngine;
using MushyAndCoffe.Interfaces;
using MushyAndCoffe.Managers;

namespace MushyAndCoffe.Player
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        private readonly RaycastHit[] interactables = new RaycastHit[4];
        [SerializeField] private IInteractable selectedInteractable;
        [SerializeField] private IActivableUI selectedActivableUI;
        
        private void Update()
        {
            var input = InputManager.Instance.GetInput();
            
            FindInteractables();
            if (input.Interact) InteractWithSelected();
            if (input.OpenInterface) OpenSelectedInterface();
        }
        
        private void FindInteractables() 
        {
            int hits = Physics.SphereCastNonAlloc(transform.position, PlayerParameters.Instance.interactionRadius, transform.forward, interactables,
                PlayerParameters.Instance.interactionDistance, PlayerParameters.Instance.interactionLayerMask);
            
            for (int i = 0; i < hits; i++) 
            {
                if (interactables[i].collider.gameObject.GetComponent<IInteractable>() == selectedInteractable) return;
            }
            
            if (hits <= 0) 
            {
                selectedInteractable = null;
                selectedActivableUI = null;
            } 
            else {
                selectedInteractable = interactables[0].collider.gameObject.GetComponent<IInteractable>();
                selectedActivableUI = interactables[0].collider.gameObject.GetComponent<IActivableUI>();
            }
        }
  
        private void OpenSelectedInterface() 
        {
            if (selectedActivableUI == null) return;
            
            selectedActivableUI.SetInterfaceVisivility(!selectedActivableUI.ActivableInterface.activeSelf);
        }      
        
        private void InteractWithSelected()
        {
            if (selectedInteractable == null) return;
            
            selectedInteractable.Interact(this.gameObject);
        }

#if UNITY_EDITOR		
        private void OnDrawGizmos()
        {			
            if(!Application.isPlaying) return;
            
            Gizmos.color = Color.blue;
            
            Gizmos.DrawSphere(transform.position + transform.forward * PlayerParameters.Instance.interactionDistance, 
                PlayerParameters.Instance.interactionRadius);
        }
#endif
    }
}
