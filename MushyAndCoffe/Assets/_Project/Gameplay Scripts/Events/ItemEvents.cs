using MushyAndCoffe.Systems.EventSystem;
using UnityEngine;

namespace MushyAndCoffe.Events
{
    public struct PickUpEvent : IEvent 
    {
    	public GameObject pickedObject;
    }
}
