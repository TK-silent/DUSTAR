using UnityEngine;

[ExecuteInEditMode]
public class CameraLayerCull : MonoBehaviour
{
    public Camera camera;

    [System.Serializable]
    public struct LayerCull
    {
        public string layerName;
        public float cullDistance;
    }

    public LayerCull[] culls;

    void OnValidate()
    {
        if (camera == null)
        {
            camera = GetComponent<Camera>();
        }

        ApplyCullingDistances();
    }

    void ApplyCullingDistances()
    {
        float[] distances = new float[32]; // Unity supports 32 layers

        foreach (var cull in culls)
        {
            int layer = LayerMask.NameToLayer(cull.layerName);
            if (layer != -1) // Check if layer is correctly defined
            {
                distances[layer] = cull.cullDistance;
            }
        }

        camera.layerCullDistances = distances;
    }
}