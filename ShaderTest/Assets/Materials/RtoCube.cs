using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RtoCube : MonoBehaviour {

    public Cubemap cube;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Camera>().RenderToCubemap(cube);
	}
}
