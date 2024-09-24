using UnityEngine;

namespace MushyAndCoffe.Interfaces
{
    public interface IActivableUI
    {
        public abstract GameObject ActivableInterface { get; set; }
        
        public void SetInterfaceVisivility(bool state);
    }
}
