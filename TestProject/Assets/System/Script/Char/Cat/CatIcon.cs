using UnityEngine;
using System.Collections;

public class CatIcon : MonoBehaviour {

	public Renderer icon;

	public bool caution;

	public bool Caution {
		get {
			return caution;
		}
		set {
			caution = value;

			GetComponent<Animator>().SetBool("Run", caution);
		}
	}
}
