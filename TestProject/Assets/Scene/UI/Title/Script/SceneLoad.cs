using UnityEngine;
using System.Collections;

public class SceneLoad : MonoBehaviour {

	public string sceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoadScene(){
		print ("a");
		Application.LoadLevel (sceneName);
	}
}
