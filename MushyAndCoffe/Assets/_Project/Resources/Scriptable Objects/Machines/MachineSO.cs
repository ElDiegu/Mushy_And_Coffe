using System.Collections.Generic;
using UnityEngine;

namespace MushyAndCoffe.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Machine", menuName = "Mushy And Coffe/Machine")]
    public class MachineSO : ScriptableObject
    {
        [field: SerializeField]      
        public int ID { get; private set; }
        
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public bool Grabable { get; private set; }
        
        [field: SerializeField]
        public List<ScriptableObject> AllowedIngredients { get; private set; }
    }
}
