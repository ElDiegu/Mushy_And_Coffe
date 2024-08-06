using UnityEngine;

namespace MushyAndCoffe
{
    public class ApplyShader : MonoBehaviour
    {
        public Shader shaderApplied;

        void Start()
        {
            if (shaderApplied != null)
            {
                MeshRenderer[] renderers = Object.FindObjectsByType<MeshRenderer>(0);
                foreach (MeshRenderer renderer in renderers)
                {
                    foreach (Material material in renderer.materials)
                    {
                        material.shader = shaderApplied;
                    }
                }
            }
        }
    }
}