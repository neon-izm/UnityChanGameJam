using UnityEngine;
using System.Collections;

public class ScoreBoardManager : MonoBehaviour {

	static private ScoreBoardManager instance;
	static public ScoreBoardManager Instance {
		get {
			return instance;
		}
	}


	public GameObject croquette_object;
	public GameObject croquettenum_object;

	public GameObject catnip_object;
	public GameObject catnipnum_object;

	public GameObject fatigue_gauge;

	public GameObject timenum_object;

	public int croquette_num;
	public int catnip_num;

	public float fatigue_rate;
	public float fatigue_gauge_long;
	public float fatigue_gauge_long_temp;

	public float left_time;

	public AnimationClip redEffect;
	public AnimationClip greenEffect;

	public Animator tmp_animator;

	void ManagementObjectColor(){
		fatigue_gauge.GetComponent<UISprite> ().alpha = 0.2f;
		if (fatigue_rate < 0.1) {
			fatigue_gauge.GetComponent<UISprite> ().color = Color.red;
		} else if (0.1 <= fatigue_rate && fatigue_rate < 0.3) {
			fatigue_gauge.GetComponent<UISprite> ().color = Color.yellow;
		} else {
			fatigue_gauge.GetComponent<UISprite> ().color = Color.green;
		}

		if (left_time <= 10) {
			timenum_object.GetComponent<UILabel> ().color = Color.red;
		} else {
			timenum_object.GetComponent<UILabel> ().color = Color.black;
		}
	}

	void displayCroqScore(){
		if (croquette_num <= 0)
			croquette_num = 0;
		if (catnip_num <= 0)
			catnip_num = 0;

		croquettenum_object.GetComponent<UILabel> ().text = "" + croquette_num;
		catnipnum_object.GetComponent<UILabel> ().text = "" + catnip_num;

		fatigue_gauge.transform.localScale = new Vector3 (fatigue_gauge_long * fatigue_rate, fatigue_gauge.transform.localScale.y, fatigue_gauge.transform.localScale.z);
		fatigue_gauge_long_temp = fatigue_gauge_long * fatigue_rate;

		int time_string_second = (int)left_time % 60;
		int time_string_minute = ((int)left_time / 60) % 100;

		timenum_object.GetComponent<UILabel> ().text = string.Format ("{0:D2}", time_string_minute) + ":" + string.Format ("{0:D2}", time_string_second);

	}

	void GetCroquette(int num){
		if (num <= 0) {
			Debug.LogError ("get croquette num is minus or zero !");
		} else {
			tmp_animator = croquettenum_object.GetComponent<Animator> ();
			croquette_num += num;
			tmp_animator.SetTrigger ("GreenEffect");
		}
	}

	void LostCroquette(int num){
		if (num >= 0) {
			Debug.LogError ("lost croquette num is plus or zero !");
		} else {
			tmp_animator = croquettenum_object.GetComponent<Animator> ();
			croquette_num += num;
			tmp_animator.SetTrigger ("RedEffect");
		}
	}

	void GetCatnip(int num){
		if (num <= 0) {
			Debug.LogError ("get croquette num is minus or zero !");
		} else {
			tmp_animator = catnipnum_object.GetComponent<Animator> ();
			catnip_num += num;
			tmp_animator.SetTrigger ("GreenEffect");
		}
	}

	void LostCatnip(int num){
		if (num >= 0) {
			Debug.LogError ("lost croquette num is plus or zero !");
		} else {
			tmp_animator = catnipnum_object.GetComponent<Animator> ();
			catnip_num += num;
			tmp_animator.SetTrigger ("RedEffect");
		}
	}

	void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		croquette_num = 0;
		catnip_num = 1;
		fatigue_gauge_long = fatigue_gauge.transform.localScale.x;
		left_time = Time.time;

		LostCroquette (-23);
		LostCatnip (-1);
	}
	
	// Update is called once per frame
	void Update () {
		left_time = Time.time;
		displayCroqScore ();
		fatigue_rate = Mathf.Abs(Mathf.Sin (Time.time));
		ManagementObjectColor ();
	}
}
