using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomBehaviour : MonoBehaviour {

	public const string Title = "猫とUnityちゃん";
	public const string URL = "https://dl.dropboxusercontent.com/u/36130213/build_web/build_web.html";


	protected void Awake () {

	}


	static public void Tweet (int score) {

		string format = "https://twitter.com/intent/tweet?&text={0}";
		string url = string.Format(format, WWW.EscapeURL(Title + "で" + score.ToString() + "個のカレーコロッケを持ち帰ることができました " + URL + " #ucgj") );
		Application.OpenURL(url);
	}
}
