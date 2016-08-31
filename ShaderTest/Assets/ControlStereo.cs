using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ControlStereo : MonoBehaviour {

    public Shader shader;
    public float separation;
	// Use this for initialization
	void Start () {
       // GetComponent<Camera>().SetReplacementShader(shader,"");
    }
	
	// Update is called once per frame
	void Update () {
        Shader.SetGlobalFloat("_EYE_SEPARATION", separation);
    }
}
