using UnityEngine;
using System.Collections;

public class GameManager : CustomBehaviour {

	static private GameManager instance;
	static public GameManager Instance {
		get {
			if(Instance == null) {
				instance = new GameObject("GameManager").AddComponent<GameManager>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}


}
