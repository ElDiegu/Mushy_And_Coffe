using UnityEngine;
using UnityEngine.UI;

namespace MushyAndCoffe
{
    public class TabMenu : MonoBehaviour
    {
        [SerializeField] Sprite tabImage1, tabImage2;
        [SerializeField] GameObject[] tabs;
        public void ChangeTab(int tabIndex)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                if (tabIndex == i)
                {
                    tabs[i].GetComponent<Image>().sprite = tabImage1;
                }
                else
                {
                    tabs[i].GetComponent<Image>().sprite = tabImage2;
                }
            }
        }
    }
}
