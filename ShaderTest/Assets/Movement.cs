using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = new Vector3(-Input.GetAxis("Vertical"), 0,  Input.GetAxis("Horizontal"));
	}
}
