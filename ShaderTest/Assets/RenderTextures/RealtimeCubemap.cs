using UnityEngine;
using System.Collections;

public class RealtimeCubemap : MonoBehaviour
{

    public int cubemapSize = 128;
    public bool oneFacePerFrame = false;
    private Camera cam;
    private RenderTexture rtex;
    private Texture2D temptex;
    private Cubemap cmap;
    private GameObject go;
    private Material m;
    public Shader shader;

    public Shader stereoShader;
    public float separation;

    [ExecuteInEditMode]
    void Start()
    {
        // render all six faces at startup
        m = new Material(shader);
        GetComponent<EQFullScreen>().mat = m;
        UpdateCubemap(63);
        temptex = new Texture2D(cubemapSize, cubemapSize);
    }

    void LateUpdate()
    {
        if (oneFacePerFrame)
        {
            int faceToRender = Time.frameCount % 6;
            int faceMask = 1 << faceToRender;
            UpdateCubemap(faceMask);
        }
        else {
            UpdateCubemap(63); // all six faces
        }
    }

    void UpdateCubemap(int faceMask)
    {
        Shader.SetGlobalFloat("_EYE_SEPARATION", separation);
        if (!cam)
        {
            
            go = new GameObject("CubemapCamera");
            go.AddComponent(typeof(Camera));
            go.AddComponent<ControlStereo>();
            go.GetComponent<ControlStereo>().shader = stereoShader;
            go.GetComponent<ControlStereo>().separation = separation;
            //go.AddComponent(typeof(EQFullScreen));

            //go.hideFlags = HideFlags.HideAndDontSave;
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            
            cam = go.GetComponent<Camera>();
            cam.farClipPlane = 500; // don't render very far into cubemap
            cam.nearClipPlane = 0.01f; // don't render very far into cubemap
            cam.enabled = false;
        }

        if (!rtex)
        {
            rtex = new RenderTexture(cubemapSize, cubemapSize, 16);
            rtex.isCubemap = true;
            rtex.hideFlags = HideFlags.HideAndDontSave;
            //GetComponent<Renderer>().sharedMaterial.SetTexture("_Cube", rtex);
            GetComponent<EQFullScreen>().mat.SetTexture("_Cube", rtex);
        }

        cam.transform.position = transform.position;
        cam.RenderToCubemap(rtex, faceMask);
    }


    void UpdateCubemap3(int faceMask)
    {
        Shader.SetGlobalFloat("_EYE_SEPARATION", separation);
        if (!cam)
        {

            go = new GameObject("CubemapCamera");
            go.AddComponent(typeof(Camera));
            //go.AddComponent<ControlStereo>();
            //go.GetComponent<ControlStereo>().shader = stereoShader;
            // go.GetComponent<ControlStereo>().separation = separation;
            //go.AddComponent(typeof(EQFullScreen));

            //go.hideFlags = HideFlags.HideAndDontSave;
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;

            cam = go.GetComponent<Camera>();
            cam.farClipPlane = 100; // don't render very far into cubemap
            cam.enabled = false;
        }

        if (!rtex)
        {
            rtex = new RenderTexture(cubemapSize, cubemapSize, 16);
            rtex.hideFlags = HideFlags.HideAndDontSave;
            cmap = new Cubemap(cubemapSize, TextureFormat.RGBA32, false);
            cmap.hideFlags = HideFlags.HideAndDontSave;
            GetComponent<EQFullScreen>().mat.SetTexture("_Cube", cmap);
        }

        cam.transform.position = transform.position;



        //cam.RenderToCubemap(rtex, faceMask);
        cam.targetTexture = rtex;
        cam.Render();

    }

    void OnDisable()
    {
        DestroyImmediate(cam);
        DestroyImmediate(rtex);
    }
}