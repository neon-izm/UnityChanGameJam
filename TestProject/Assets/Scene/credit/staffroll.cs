using UnityEngine;
using System.Collections;


public class staffroll : MonoBehaviour {
	
	public float speed = 1;
	
	void Update () {
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}

}
