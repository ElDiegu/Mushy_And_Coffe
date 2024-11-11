using EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MushyAndCoffe.Slot;

namespace MushyAndCoffe
{
    public class InfoChange : MonoBehaviour
    {
        EventBinding<OnPlaceSlotEvent> slotEvent;
        [SerializeField] GameObject title, description, icon;
        private void OnEnable()
        {
            slotEvent = new EventBinding<OnPlaceSlotEvent>(ChangeInfo);
            EventBus<OnPlaceSlotEvent>.Register(slotEvent);
        }

        private void OnDisable()
        {
            EventBus<OnPlaceSlotEvent>.Deregister(slotEvent);
        }

        public void ChangeInfo(OnPlaceSlotEvent slotInfo) {
            if (!icon.activeSelf)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
            }
            //Text
            title.GetComponent<TMP_Text>().text = slotInfo.name;
            description.GetComponent<TMP_Text>().text = slotInfo.description;
            //Image
            icon.GetComponent<Image>().sprite = slotInfo.icon;
        }

    }
}
