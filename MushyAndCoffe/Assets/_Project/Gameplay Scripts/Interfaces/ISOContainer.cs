using UnityEngine;

namespace MushyAndCoffe.Interfaces
{
    public interface ISOContainer
    {
        [field: SerializeField]
        public abstract ScriptableObject InteractableType { get; set; }
    }
}
