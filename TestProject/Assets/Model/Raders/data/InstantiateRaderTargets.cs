using UnityEngine;
using System.Collections;

public class InstantiateRaderTargets : MonoBehaviour {

	public GameObject PlayerMarker;
	public GameObject EnemyMarker;
	public GameObject GoalMarker;
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

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
