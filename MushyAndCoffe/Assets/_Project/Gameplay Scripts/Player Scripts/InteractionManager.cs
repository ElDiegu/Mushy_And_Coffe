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
		
		private void Update()
		{
			FindInteractables();
			InteractWithSelected();
		}
		
		private void FindInteractables() 
		{
			int hits = Physics.SphereCastNonAlloc(transform.position, PlayerParameters.Instance.interactionRadius, transform.forward, interactables,
				PlayerParameters.Instance.interactionDistance, PlayerParameters.Instance.interactionLayerMask);
			
			for (int i = 0; i < hits; i++) 
			{
				if (interactables[i].collider.gameObject.GetComponent<IInteractable>() == selectedInteractable) return;
			}
			
			if (hits <= 0) selectedInteractable = null;
			else selectedInteractable = interactables[0].collider.gameObject.GetComponent<IInteractable>();
		}
		
		private void InteractWithSelected()
		{
			var input = InputManager.Instance.GetInput();
			
			if (input.Interact) 
			{
				selectedInteractable.Interact(this.gameObject);
			}
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
