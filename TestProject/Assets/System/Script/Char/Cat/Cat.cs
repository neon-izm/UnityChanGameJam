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

	private NavMeshAgent navMeshAgentCmp; 
	public NavMeshAgent NavMeshAgentCmp {
		get{ return (navMeshAgentCmp!=null)?navMeshAgentCmp : navMeshAgentCmp=GetComponent<NavMeshAgent>();}
	}


	new void Awake () {
		base.Awake();
		stateMachine = new ALT.StateMachine<StateEnum>(
			new Dictionary<StateEnum, ALT.State> () {
				{StateEnum.EMPTY, new ALT.State(null, null, null, null)},
				{StateEnum.IDLE, new ALT.State(null, null, null, null)},
				{StateEnum.SLEEP, new ALT.State(null, null, null, null)},
				{StateEnum.WALK, new ALT.State(ExecuteWalk, null, null, null)},
				{StateEnum.RUN, new ALT.State(null, null, null, null)},
				{StateEnum.JUMP, new ALT.State(null, null, null, null)},
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


	void ExecuteWalk () {
		NavMeshAgentCmp.SetDestination(target.position);
	}
}
