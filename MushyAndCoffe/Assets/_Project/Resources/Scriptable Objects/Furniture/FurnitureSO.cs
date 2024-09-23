using UnityEngine;

namespace MushyAndCoffe.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Furniture", menuName = "Mushy And Coffe/Furniture")]
    public class FurnitureSO : ScriptableObject
    {
        [field: SerializeField]      
        public int ID { get; private set; }

        [field: SerializeField]
        public string Name { get; private set;}

        [field: SerializeField]
        public Vector2 Size { get; private set; }

        [field: SerializeField]
        public GameObject Model { get; set; }
    }
}
