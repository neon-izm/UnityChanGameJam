using UnityEngine;
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

	//	[PrefixLabel("コロッケの数")]
	public int foodNumber {
		get {
			return itemList.Where(i=>i.GetComponent<Croquette>() != null).ToList().Count;
		}
	}

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

	[PrefixLabel("アイテムリスト")]
	public List<Item> itemList;

	new void Awake () {
		base.Awake();
		instance = this;
		currentPhysical = maxPhysical;
	}

	void Update ()
	{
		//ものを投げる
		if(Input.GetButtonDown ("Throw")) {
			ThrowItem();
		}

		if(Input.GetButtonDown("Jump")) {
			currentPhysical -= physicalParSecJump;
		}
		if(Input.GetButton("Run")) {
			currentPhysical -= physicalParSecRun * Time.deltaTime;
		}

		if(!Input.GetButtonDown("Jump") && !Input.GetButton("Run")) {
			currentPhysical = Mathf.Clamp(currentPhysical + physicalParRecover * Time.deltaTime, 0f, 100f);

		}
	}

	void FixedUpdate () {
	}

	//選んだアイテムを投げる 
	void ThrowItem () {
		Debug.Log("throw");

	}

	//猫に攻撃された 
	void Damage () {

	}

	void OnTriggerEnter (Collider c)
	{
		//アイテム入手 
		Item item = c.GetComponent<Item>();
		if(item) GetItem (item);

		//家に到着 


		//店に到着 

	}


	void GetItem (Item item) {

		//アイテムに追加 
		itemList.Add(item);
	}

	#if UNITY_EDITOR 
	void OnGUI () {
	    
		GUILayout.Box(
			"コロッケの数 : " + foodNumber + "\n" +
			"現在の体力 : " + currentPhysical
			, GUILayout.Width(300)
		);
	
	}
	#endif
}
