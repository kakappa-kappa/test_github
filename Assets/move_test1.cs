using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_test1 : MonoBehaviour {
    public float speed = 10.0f;

    float moveX = 0f;
    float moveZ = 0f;
    CharacterController cc;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ);

        cc.SimpleMove(direction);
	}
}
