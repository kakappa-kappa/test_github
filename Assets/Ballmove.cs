using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballmove : MonoBehaviour {
    public float s = 10;
	// Update is called once per frame
	void FixedUpdate () {
       
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(x*s, 0, z*s);
	}
}
