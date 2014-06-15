﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerScript : CustomBehaviour {

	static private PlayerScript instance;
	static public PlayerScript Instance {
		get {return instance;}
	}

	private Animator animatorCmp;
	public Animator AnimatorCmp {
		get { return (animatorCmp!=null)?animatorCmp:animatorCmp=GetComponent<Animator>(); }
	}

	public float leftTime = 120;

	[PrefixLabel("コロッケの数")]
	public int foodNumber = 0;

	[PrefixLabel("体力回復率")]
	public float physicalParRecover = 10;
	[PrefixLabel("最大体力")]
	public float maxPhysical = 100;
	[System.NonSerialized]//[PrefixLabel("現在の体力")]
	public float currentPhysical;

	[PrefixLabel("走るのに必要な体力(秒)")]
	public float physicalParSecRun;
	[PrefixLabel("跳ぶのに必要な体力(回)")]
	public float physicalParSecJump;

	public List<Item> itemList;

	public bool isRunning = false;
	public bool isDamaging = false;
	public bool isExthoust = false;

	new void Awake () {
		Application.LoadLevelAdditive("ScoreScene");
		base.Awake();
		instance = this;
		currentPhysical = maxPhysical;
	}

	void Update ()
	{
		if(ScoreBoardManager.Instance == null) return;

		UnityChanControlScriptWithRgidBody script = GetComponent<UnityChanControlScriptWithRgidBody>();

		script.inputHorizontal = Input.GetAxis("Horizontal");
		script.inputVertical = Input.GetAxis("Vertical");
		if(AnimatorCmp.GetBool("Damage") || isExthoust){
			script.inputHorizontal = 0f; 
			script.inputVertical = 0f; 
		}
		//ものを投げる
		if(Input.GetButtonDown ("Throw")) {
			script.inputThrow = true;
			StartCoroutine( ThrowItem());
		}

		if(Input.GetButtonDown("Jump")) {
			script.inputJump = true;
			currentPhysical -= physicalParSecJump;
		}

		if(Input.GetButton("Run") && !isExthoust) {
			script.inputRun = true;
			currentPhysical = Mathf.Clamp(currentPhysical - physicalParSecRun * Time.deltaTime * script.inputVertical, 0, maxPhysical);
			isRunning = true;
		}else {
			isRunning = false;
		}

		if(currentPhysical == 0 && !isExthoust) {
			isExthoust = true;
			AnimatorCmp.SetBool("Exthoust", true);
		}
		if(currentPhysical >= 10 && isExthoust) {
			isExthoust = false;
			AnimatorCmp.SetBool("Exthoust", false);
		}

		if(!Input.GetButtonDown("Jump") && !Input.GetButton("Run")) {
			currentPhysical = Mathf.Clamp(currentPhysical + physicalParRecover * Time.deltaTime, 0f, maxPhysical);

		}
	}

	void FixedUpdate () {
		if(ScoreBoardManager.Instance == null) return;
		ScoreBoardManager.Instance.fatigue_rate = currentPhysical/maxPhysical;

		leftTime -= Time.deltaTime;
		if(leftTime > 0) {
			ScoreBoardManager.Instance.left_time = leftTime;
		}else {
			TimeUp();
		}
	}

	//選んだアイテムを投げる 
	IEnumerator ThrowItem () {
		yield return new WaitForSeconds(1f);
		Debug.Log("throw");
		UnityChanControlScriptWithRgidBody script = GetComponent<UnityChanControlScriptWithRgidBody>();
		script.inputThrow = false;
	}

	//猫に攻撃された 
	public void Damage () {
		if(isDamaging) return;

		//ダメージ処理 
		AnimatorCmp.SetBool("Damage", true);
		StartCoroutine(DamageAction());
		foodNumber --;
		ScoreBoardManager.Instance.LostCroquette(-1);
		isDamaging = true;
	}

	IEnumerator DamageAction () {
		yield return new WaitForSeconds(1.5f);
		AnimatorCmp.SetBool("Damage", false);
		yield return new WaitForSeconds(3f);
		isDamaging = false;
	}

	public void LockOn (Cat cat) {


	}

	void OnTriggerEnter (Collider c)
	{
		//アイテム入手 
		Item item = c.GetComponent<Item>();
		if(item) GetItem (item);

		//家に到着 
		if(c.tag == "Goal") {
			Clear();
		}

		if(c.tag == "Respawn") {
			Respawn();
		}

		//店に到着 
		if(c.tag == "Shop") {
			//10個買う 
			foodNumber += 10;
			ScoreBoardManager.Instance.GetCroquette(10);
			c.gameObject.SetActive(false);
		}
	}


	void GetItem (Item item) {

		//アイテムに追加 
		itemList.Add(item);
	}
	/*
	#if UNITY_EDITOR 
	void OnGUI () {
	    
		GUILayout.Box(
			"コロッケの数 : " + foodNumber + "\n" +
			"現在の体力 : " + currentPhysical
			, GUILayout.Width(300)
		);
	
	}
	#endif
	*/

	//クリア処理 
	void Clear () {
		Debug.Log("Clear : " + foodNumber);
		Result.LoadResultClear(foodNumber);
	}

	void Respawn () {
		Debug.Log("Respawn");
		Application.LoadLevel(Application.loadedLevel);
	}

	void TimeUp () {
		Result.LoadResultTimeUp();
	}
}
