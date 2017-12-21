using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Damage{
	None,
	Weak,
	Medium,
	Heavy,
}

public class Brick : MonoBehaviour {

	public bool weakened;
	public Damage damageType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.GetComponent<Rigidbody> ().isKinematic == true) {
			switch (damageType) {
			case Damage.Weak:
				this.gameObject.GetComponent<Rigidbody> ().AddForce(transform.forward * 10000);
				break;
			case Damage.Medium:
				this.gameObject.GetComponent<Rigidbody> ().AddForce(transform.forward * 15000);
				break;
			case Damage.Heavy:
				this.gameObject.GetComponent<Rigidbody> ().AddForce(transform.forward * 20000);
				break;
			}
		}
	}

	public void IncreaseDamageType(){
		switch (damageType) {
		case Damage.Weak:
			damageType = Damage.Medium;
			break;
		case Damage.Medium:
			damageType = Damage.Heavy;
			break;
		case Damage.Heavy:
			damageType = Damage.Heavy;
			break;
		default:
			damageType = Damage.Weak;
			break;
		}
	}
}

