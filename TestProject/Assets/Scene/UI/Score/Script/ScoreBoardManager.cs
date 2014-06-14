using UnityEngine;
using System.Collections;

public class ScoreBoardManager : MonoBehaviour {

	public GameObject croquette_object;
	public GameObject croquettenum_object;

	public GameObject stone_object;
	public GameObject stonenum_object;

	public GameObject catnip_object;
	public GameObject catnipnum_object;

	public GameObject fatigue_gauge;

	public GameObject timenum_object;

	public int croquette_num;
	public int stone_num;
	public int catnip_num;

	public float fatigue_rate;
	public float fatigue_gauge_long;
	public float fatigue_gauge_long_temp;

	public float left_time;

	public AnimationClip redEffect;
	public AnimationClip greenEffect;


	void displayCroqScore(){
		croquettenum_object.GetComponent<UILabel> ().text = "" + croquette_num;
		stonenum_object.GetComponent<UILabel> ().text = "" + stone_num;
		catnipnum_object.GetComponent<UILabel> ().text = "" + catnip_num;

		fatigue_gauge.transform.localScale = new Vector3 (fatigue_gauge_long * fatigue_rate, fatigue_gauge.transform.localScale.y, fatigue_gauge.transform.localScale.z);
		fatigue_gauge_long_temp = fatigue_gauge_long * fatigue_rate;

		int time_string_second = (int)left_time % 60;
		int time_string_minute = ((int)left_time / 60) % 100;

		timenum_object.GetComponent<UILabel> ().text = string.Format ("{0:D2}", time_string_minute) + ":" + string.Format ("{0:D2}", time_string_second);
	}

	// Use this for initialization
	void Start () {
		croquette_num = 0;
		catnip_num = 1;
		stone_num = 2;
		fatigue_gauge_long = fatigue_gauge.transform.localScale.x;
		left_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		left_time = Time.time;
		displayCroqScore ();
		croquette_num++;
		fatigue_rate = Mathf.Abs(Mathf.Sin (Time.time));
	}
}
