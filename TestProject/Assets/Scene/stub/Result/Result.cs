using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {

	static int number;

	static public void LoadResultClear (int n) {
		number = n;
		Application.LoadLevelAdditive("ResultClear");
	}

	static public void LoadResultTimeUp () {
		Application.LoadLevelAdditive("ResultTiemUp");
	}

	public UILabel uiLabel;

	void Awake () {
		uiLabel.text = number.ToString();
	}

	void Back () {
		Application.LoadLevel("TitleScene");
	}

	void Tweet () {
		CustomBehaviour.Tweet(number);
		Application.LoadLevel("TitleScene");
	}
}
