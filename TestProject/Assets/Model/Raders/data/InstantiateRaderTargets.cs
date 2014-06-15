using UnityEngine;
using System.Collections;

public class InstantiateRaderTargets : MonoBehaviour {

	public GameObject PlayerMarker;
	public GameObject EnemyMarker;
	public GameObject GoalMarker;
	//public GameObject ShopMarker;
	// Use this for initialization
	void Start () {
	//
		if (PlayerMarker != null) {
			GameObject [] players=GameObject.FindGameObjectsWithTag("Player");
			foreach (var item in players) {
				GameObject lPlayerMarker=(GameObject)Instantiate(PlayerMarker);
				lPlayerMarker.transform.parent=item.transform;
				lPlayerMarker.transform.localPosition=new Vector3(0f,0f,0f);
			}
		}

		if (EnemyMarker != null) {
			GameObject [] enemys=GameObject.FindGameObjectsWithTag("Enemy");
			foreach (var item in enemys) {
				GameObject lEnemyMarker=(GameObject)Instantiate(EnemyMarker);
				lEnemyMarker.transform.parent=item.transform;
				lEnemyMarker.transform.localPosition= new Vector3(0f,0f,0f);
				
			}
		}
		if (GoalMarker != null) {
			GameObject [] goals=GameObject.FindGameObjectsWithTag("Goal");
			foreach (var item in goals) {
				GameObject lGoalMarker=(GameObject)Instantiate(GoalMarker);
				lGoalMarker.transform.parent=item.transform;
				lGoalMarker.transform.localPosition=new Vector3(0f,0f,0f);
			}
		}
		/*
		if (ShopMarker != null) {
			GameObject [] shops=GameObject.FindGameObjectsWithTag("Shop");
			foreach (var item in shops) {
				GameObject lShopMarker=(GameObject)Instantiate(ShopMarker);
				lShopMarker.transform.parent=item.transform;
				lShopMarker.transform.localPosition=new Vector3(0f,0f,0f);
				lShopMarker.transform.localScale=new Vector3(15f,15f,15f);
			}
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
