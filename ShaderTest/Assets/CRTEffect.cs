using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CRTEffect : MonoBehaviour
{
    public Material material;

    void Awake()
    {
        material = new Material(Shader.Find("Custom/cubemapToEquirectangular"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture sr, RenderTexture dt)
    {
       // Graphics.Blit(sr, dt, material);
    }
}