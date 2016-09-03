using UnityEngine;
using System.Collections;

public enum FacesBitmask { PositiveX = 0, NegativeX = 1, PositiveY = 2, NegativeY = 4, PositiveZ = 8, NegativeZ = 16}

public class RealtimeCubemap : MonoBehaviour
{
    public int cubemapSize = 128;
    public bool oneFacePerFrame = false;
    private Camera cam;
    public RenderTexture rtex;
    private Texture2D temptex;
    private Cubemap cmap;
    private GameObject go;
    private Material m;
    public Shader shader;
    public int customMask = -1;

    public bool allFaces;
    public bool[] customFaces;

    public Shader stereoShader;
    public float separation;

    [ExecuteInEditMode]
    void Start()
    {
        // render all six faces at startup
        m = new Material(shader);
        //GetComponent<EQFullScreen>().mat = m;
        //UpdateCubemap(63);
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
            if (allFaces)
            {
                UpdateCubemap(63); // all six faces
            }
            else if(customMask>0) // we have a custom mask set
            {
                UpdateCubemap(customMask);
            }
            else
            {
                int faceMask = 1;
                for (int i =0; i < customFaces.Length; i++)
                {
                    if (customFaces[i]) faceMask = faceMask | (1 << i);
                    
                }
                UpdateCubemap(faceMask);

            }
        }
    }

    void UpdateCubemap(int faceMask)
    {
        //Debug.Log("Facemask = " + faceMask);
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
            rtex.antiAliasing = 2;
            rtex.depth = 0;
            //rtex.anisoLevel = 16;
            rtex.useMipMap = true;
            rtex.isCubemap = true;
            rtex.hideFlags = HideFlags.HideAndDontSave;
            //GetComponent<Renderer>().sharedMaterial.SetTexture("_Cube", rtex);
            //GetComponent<EQFullScreen>().mat.SetTexture("_Cube", rtex);
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
            //GetComponent<EQFullScreen>().mat.SetTexture("_Cube", cmap);
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