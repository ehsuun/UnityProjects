using UnityEngine;
using System.Collections;

public class EQFullScreen : MonoBehaviour {

    public Material mat;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
       if(mat) Graphics.Blit(source, destination, mat);
    }

}
