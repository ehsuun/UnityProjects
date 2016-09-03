using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RealtimeCubemap))]
public class EQFullScreen : MonoBehaviour {

    private Material mat;
    public Shader shader;
    public RenderTexture tex;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (!tex)
        {
            tex = GetComponent<RealtimeCubemap>().rtex;
            if (tex)
            {
                mat = new Material(shader);
                mat.SetTexture("_Cube", tex);
            }
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
       if(mat) Graphics.Blit(source, destination, mat);
    }

}
