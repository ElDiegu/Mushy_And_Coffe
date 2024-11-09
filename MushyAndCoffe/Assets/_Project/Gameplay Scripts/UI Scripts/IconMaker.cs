using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

namespace MushyAndCoffe
{
    public class IconMaker : MonoBehaviour
    {
        public Camera iconCamera;
        private string savePath = "Assets/_Project/Art/2D Assets/UI/InventoryIcons";

        public void GetIcon()
        {
            //iconCamera.orthographicSize = item.GetComponent<Renderer>().bounds.extents.y + 0.1f;
            int xRes = iconCamera.pixelWidth;
            int yRes = iconCamera.pixelHeight;

            // Clipping variables
            int clipX = xRes - yRes;
            int clipY = 0;
            RenderTexture rt = new RenderTexture(xRes, yRes, 24);
            //Initialization
            int childCount = transform.childCount;
            for(int i = 0; i < childCount; i++){
                GameObject objToRender = transform.GetChild(i).gameObject;
                objToRender.SetActive(true);
                Texture2D tex = new Texture2D(xRes - clipX, yRes - clipY, TextureFormat.RGBA32, false);
                rt = new RenderTexture(xRes, yRes, 24);
                iconCamera.targetTexture = rt;
                RenderTexture.active = rt;

                //Puts the texture into the icon
                iconCamera.Render();
                tex.ReadPixels(new Rect(clipX / 2, clipY / 2, xRes - clipX, yRes - clipY), 0, 0);
                tex.Apply();

                byte[] bytes = tex.EncodeToPNG();
                string filePath = Path.Combine(savePath, objToRender.name+".png");
                File.WriteAllBytes(filePath, bytes);

                //Reset
                iconCamera.targetTexture = null;
                RenderTexture.active = null;
                objToRender.SetActive(false);
            }
            Destroy(rt);
        }
    }
}
