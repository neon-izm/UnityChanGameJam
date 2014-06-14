using UnityEngine;
using System.Collections;

public class Controller : CustomBehaviour{

	static private Controller instance;
	static public Controller Instance {
		get {
			if(Instance == null) {
				instance = new GameObject("Controller").AddComponent<Controller>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

}
