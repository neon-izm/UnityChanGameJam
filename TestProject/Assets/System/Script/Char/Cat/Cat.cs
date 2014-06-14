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
				{StateEnum.IDLE, new ALT.State(ExecuteIdle, DecideIdle, EnterIdle, null)},
				{StateEnum.SLEEP, new ALT.State(ExecuteSleep, DecideSleep, EnterSleep, null)},
				{StateEnum.WALK, new ALT.State(ExecuteWalk, DecideWalk, EnterWalk, null)},
				{StateEnum.RUN, new ALT.State(ExecuteRun, DecideRun, EnterRun, null)},
				{StateEnum.JUMP, new ALT.State(ExecuteJump, DecideJump, EnterJump, null)},
			},
			defaultState
		);
	}

	void Start () {
		target = PlayerScript.Instance.transform;
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
		randTime -= Time.deltaTime;
		if(randTime > 0) return;

		NavMeshAgentCmp.speed = runSpeed;
		NavMeshAgentCmp.SetDestination(target.position);
	}

	void ExecuteJump () {
		NavMeshAgentCmp.SetDestination(target.position);
	}

	float randTime = 0;

	ALT.State DecideIdle () {
		randTime -= Time.deltaTime;
		if(randTime < 0) {
			return stateMachine.GetState(StateEnum.WALK);
		}

		if(CheckPlayerInRange() && randTime < 2) {
			return stateMachine.GetState(StateEnum.RUN);
		}

		return stateMachine.GetState(StateEnum.IDLE);
	}

	ALT.State DecideSleep () {

		return stateMachine.GetState(StateEnum.SLEEP);
	}

	ALT.State DecideWalk () {
		randTime -= Time.deltaTime;
		if(randTime < 0) {
			return stateMachine.GetState(StateEnum.IDLE);
		}
		if(CheckPlayerInRange()) {
			return stateMachine.GetState(StateEnum.RUN);
		}
		return stateMachine.GetState(StateEnum.WALK);
	}

	ALT.State DecideRun () {
		if(CheckGiveUpRange()) {
			return stateMachine.GetState(StateEnum.WALK);
		}
		if(Vector3.Distance(target.position, transform.position) < 2) {
			return stateMachine.GetState(StateEnum.JUMP);
		}
		return stateMachine.GetState(StateEnum.RUN);
	}

	ALT.State DecideJump () {
		if(isJumping == false) {
			return stateMachine.GetState(StateEnum.RUN);
		}
		return stateMachine.GetState(StateEnum.JUMP);
	}

	void EnterIdle () {
		randTime = Random.value * 10f;
		AnimatorCmp.SetTrigger("IDLE");
	}
	void EnterSleep () {
		AnimatorCmp.SetTrigger("SLEEP");
	}
	void EnterWalk () {
		randTime = Random.value * 7f;
		AnimatorCmp.SetTrigger("WALK");
	}
	void EnterRun () {
		randTime = 1f;
		PlayerScript.Instance.LockOn(this);
		AnimatorCmp.SetTrigger("RUN");
	}
	void EnterJump () {
		AnimatorCmp.SetTrigger("JUMP");
		StartCoroutine(JumpFinish());
	}

	bool isJumping = false;
	IEnumerator JumpFinish () {
		isJumping = true;
		yield return new WaitForSeconds(1.5f);
		isJumping = false;
	}

	bool CheckPlayerInRange () {
		Vector3 pos = PlayerScript.Instance.transform.position;

		//耳 
		if(PlayerScript.Instance.isRunning) {
			if(Vector3.Distance(pos, transform.position) < earLength){
				return true;
			}
		}
		//目
		if(Vector3.Distance(pos, transform.position) < eyeLength
			&& Vector3.Angle(pos-transform.position, transform.forward) < 60){
			return true;
		}
		//鼻
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

	float time;
	void OnTriggerStay (Collider c) {
		if(!isJumping) return;
		if(Time.time - time < 2f) return;
		time = Time.time;

		if(c.GetComponent<PlayerScript>()) {
			PlayerScript.Instance.Damage();
		}
	}

}
