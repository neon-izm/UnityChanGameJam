using UnityEngine;
using System.Collections;

public class ScoreBoardManager : MonoBehaviour {

	public GameObject no1;
	public GameObject no2;
	public GameObject no3;
	public GameObject no4;
	public GameObject no5;
	public GameObject no6;
	public GameObject no7;
	public GameObject no8;
	public GameObject no9;

	public GameObject tendigit_object;
	public GameObject tendigitnum_object;

	public UISprite[] no_list;

	public UISprite tendigit_sprite;
	public UISprite tendigitnum_sprite;

	public int croquette_num;

	void displayCroqScore(){
		int onedigit_num = croquette_num % 10;
		int tendigit_num = croquette_num / 10;

		//１の桁より大きいオブジェクトを非表示にする。
		for(int i =0;i<9;i++){
			if (i > onedigit_num) {
				no_list [i].renderer.enabled = false;
			} else {
				no_list [i].renderer.enabled = true;
			}
		}

		//10個の固まりが何個あるか表示する
		if (tendigit_num <= 0) {
			tendigit_object.renderer.enabled = false;
			tendigitnum_object.renderer.enabled = false;
		} else {
			tendigit_object.renderer.enabled = true;
			tendigitnum_object.renderer.enabled = true;
			tendigitnum_object.GetComponent<UILabel> ().text = "" + tendigit_num;
		}
	}

	// Use this for initialization
	void Start () {
		croquette_num = 0;
		no_list = new UISprite[] {
			no1.GetComponent<UISprite> (),
			no2.GetComponent<UISprite> (),
			no3.GetComponent<UISprite> (),
			no4.GetComponent<UISprite> (),
			no5.GetComponent<UISprite> (),
			no6.GetComponent<UISprite> (),
			no7.GetComponent<UISprite> (),
			no8.GetComponent<UISprite> (),
			no9.GetComponent<UISprite> ()
		};
	}
	
	// Update is called once per frame
	void Update () {
		displayCroqScore ();
		croquette_num++;
	}
}
