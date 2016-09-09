using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localEulerAngles += new Vector3(0,Input.GetAxis("Mouse X")*2f,0);
        GetComponent<Rigidbody>().velocity = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;


    }
}
