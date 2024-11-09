using UnityEngine;

namespace MushyAndCoffe
{
    public class PlacementUI : MonoBehaviour
    {
        public void ButtonOnClick(GameObject panel) => panel.SetActive(!panel.activeSelf);

    }
}
