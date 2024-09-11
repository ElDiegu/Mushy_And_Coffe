using UnityEngine;

namespace MushyAndCoffe.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Machine", menuName = "Mushy And Coffe/Machine")]
	public class MachineSO : ScriptableObject
	{
		public int MachineID;
		public string MachineName;
		public bool Grabable;
	}
}
