using UnityEngine;
using System.Collections;


public enum CubeCamType
{
    Forward,Back,Up,Down,Left,Right
}

public class CubeRender : MonoBehaviour {
    public CubeCamType type;
    public int renderSize = 128;
    public Material m;

    public Cubemap cube;
    public RenderTexture rt;
    private int faceMask = 0;
    // Use this for initialization
    void Start () {
        //GetComponent<Camera>().enabled = false;
        rt = new RenderTexture(renderSize, renderSize, 0);
        rt.isCubemap = true;
        rt.hideFlags = HideFlags.HideAndDontSave;
        m.SetTexture("_MainTex", rt);
    }
	
	// Update is called once per frame
	void Update () {
        //GetComponent<Camera>().RenderToCubemap(cube);
        for(int j = 0;j<6; j++)
        {
            faceMask = 1 << j;
            GetComponent<Camera>().RenderToCubemap(rt, 63);
        }
        
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //mat is the material containing your shader
        //Graphics.Blit(source, destination, m);
    }
}
