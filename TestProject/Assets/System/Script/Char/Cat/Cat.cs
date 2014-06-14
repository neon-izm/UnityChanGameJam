using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
public class Cat : CustomBehaviour {

	public enum StateEnum {
		EMPTY,
		IDLE,
		SLEEP,
		WALK,
		RUN,
		JUMP,
	}
	[System.NonSerialized]
	public ALT.StateMachine<StateEnum> stateMachine;

	[PrefixLabel("ターゲット")][Tooltip("ターゲットを追いかけます (主にUnityChan)")]
	public Transform target;

	[PrefixLabel("初期ステータス")][Tooltip("初期の状態です")]
	public StateEnum defaultState = StateEnum.IDLE;

	[PrefixLabel("歩く速さ")]
	public float walkSpeed;
	[PrefixLabel("走る速さ")]
	public float runSpeed;
	[PrefixLabel("ジャンプの高さ")]
	public float jumpHeight;


	[PrefixLabel("音が聞こえる範囲")]
	public float earLength;
	[PrefixLabel("目が見える範囲")]
	public float eyeLength;
	[PrefixLabel("鼻が効く範囲")]
	public float noseLength;
	[PrefixLabel("あきらめる範囲")]
	public float giveUpLength;

	public StateEnum stateEnum;
	private Vector3 randPos;

	private Animator animatorCmp;
	public Animator AnimatorCmp {
		get { return (animatorCmp!=null)?animatorCmp:animatorCmp=GetComponent<Animator>(); }
	}

	private NavMeshAgent navMeshAgentCmp; 
	public NavMeshAgent NavMeshAgentCmp {
		get{ return (navMeshAgentCmp!=null)?navMeshAgentCmp : navMeshAgentCmp=GetComponent<NavMeshAgent>();}
	}


	new void Awake () {
		base.Awake();
		stateMachine = new ALT.StateMachine<StateEnum>(
			new Dictionary<StateEnum, ALT.State> () {
				{StateEnum.EMPTY, new ALT.State(null, null, null, null)},
				{StateEnum.IDLE, new ALT.State(ExecuteIdle, DecideIdle, null, null)},
				{StateEnum.SLEEP, new ALT.State(ExecuteSleep, DecideSleep, null, null)},
				{StateEnum.WALK, new ALT.State(ExecuteWalk, DecideWalk, null, null)},
				{StateEnum.RUN, new ALT.State(ExecuteRun, DecideRun, null, null)},
				{StateEnum.JUMP, new ALT.State(ExecuteJump, DecideJump, null, null)},
			},
			defaultState
		);
	}

	void Start () {

	}

	void FixedUpdate () {
		stateMachine.UpdateState();
		stateMachine.Execute();


	}

	void ExecuteIdle () {
		//ゴロゴロする 




	}

	void ExecuteSleep () {
	}

	void ExecuteWalk () {
		NavMeshAgentCmp.speed = walkSpeed;
		if(Vector3.Distance(randPos, transform.position) < 2) {
			randPos += Random.onUnitSphere * Random.value * 5;
			randPos.y = 0;
		}

		NavMeshAgentCmp.SetDestination(randPos);
	}

	void ExecuteRun () {
		NavMeshAgentCmp.speed = runSpeed;
		NavMeshAgentCmp.SetDestination(target.position);
	}

	void ExecuteJump () {
		NavMeshAgentCmp.SetDestination(target.position);
	}

	ALT.State DecideIdle () {
		if(CheckPlayerInRange()) {
			return stateMachine.GetState(StateEnum.RUN);
		}
		return stateMachine.GetState(StateEnum.IDLE);
	}

	ALT.State DecideSleep () {

		return stateMachine.GetState(StateEnum.SLEEP);
	}

	ALT.State DecideWalk () {
		if(CheckPlayerInRange()) {
			return stateMachine.GetState(StateEnum.RUN);
		}
		return stateMachine.GetState(StateEnum.WALK);
	}

	ALT.State DecideRun () {
		if(CheckGiveUpRange()) {
			return stateMachine.GetState(StateEnum.WALK);
		}
		if(Vector3.Distance(target.position, transform.position) < 3) {
			//			stateMachine.ChangeState(StateEnum.JUMP);
		}
		return stateMachine.GetState(StateEnum.RUN);
	}

	ALT.State DecideJump () {
		if(CheckJumpFinish()){
			return stateMachine.GetState(StateEnum.RUN);
		}
		return stateMachine.GetState(StateEnum.JUMP);
	}

	bool CheckPlayerInRange () {
		Vector3 pos = PlayerScript.Instance.transform.position;
		if(Vector3.Distance(pos, transform.position) < noseLength){
			return true;
		}
		return false;
	}

	bool CheckGiveUpRange () {
		Vector3 pos = PlayerScript.Instance.transform.position;
		if(Vector3.Distance(pos, transform.position) > giveUpLength){
			return true;
		}
		return false;
	}

	bool CheckJumpFinish () {


		return false;
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, noseLength);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, earLength);
	}

}
