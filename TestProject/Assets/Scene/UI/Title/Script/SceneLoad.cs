using UnityEngine;
using System.Collections;

public class SceneLoad : MonoBehaviour {

	public string sceneName;
	public AudioClip clip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoadScene(){
		print ("a");
		StartCoroutine(LoadAsync());
	}

	AsyncOperation async;
	IEnumerator LoadAsync (){
		AudioSource source = gameObject.AddComponent<AudioSource>();
		if(clip != null) {
			source.panLevel = 0;
			source.clip = clip;
			source.Play();
		}
		async = Application.LoadLevelAsync (sceneName);
		async.allowSceneActivation = false;
		while(source.isPlaying){
			yield return 0;
		}
		Debug.Log(async.progress + " " + source.isPlaying);
		async.allowSceneActivation = true;
	}

	void OnGUI ()
	{
		if(async == null) return;
		GUI.color = Color.blue;
		string progressLabel = "";
		for(int i=0; i<async.progress*20; i++){
			progressLabel += ".";
		}
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.skin.label.fontSize = (int)(100f * (float)Screen.height/1080f);
		GUI.Label(new Rect(0, Screen.height*0.5f,Screen.width,50), progressLabel);
	}

}
