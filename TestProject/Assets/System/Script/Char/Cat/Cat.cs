using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Cat : CustomBehaviour {

	public enum StateEnum {
		EMPTY,
		IDLE,
		SLEEP,
		WALK,
		RUN,
		JUMP,
	}

	public StateMachine<StateEnum> stateMachine = new StateMachine<StateEnum>(
		new Dictionary<StateEnum, State> () {
			{StateEnum.IDLE, new State(null, null, null, null)},
			{StateEnum.SLEEP, new State(null, null, null, null)},
			{StateEnum.WALK, new State(null, null, null, null)},
			{StateEnum.RUN, new State(null, null, null, null)},
			{StateEnum.JUMP, new State(null, null, null, null)},
		},
		StateEnum.EMPTY
	);

	[PrefixLabel("初期ステータス")][Tooltip("初期の状態です")]
	public StateEnum defaultState = StateEnum.IDLE;


	void Awake () {
		stateMachine.ChangeState(defaultState);
	}

	void FixedUpdate () {
		stateMachine.UpdateState();
		stateMachine.Execute();
	}





}
