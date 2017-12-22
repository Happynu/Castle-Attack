using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

	List<GameObject> bricks = new List<GameObject>();
	private bool collapsing;

	// Use this for initialization
	void Start () {
		foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Brick")) {
			bricks.Add (brick);
			brick.GetComponent<Rigidbody> ().isKinematic = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.GetComponent<Brick> () != null) {
					hit.collider.gameObject.GetComponent<Brick> ().IncreaseDamageType ();
					hit.collider.gameObject.GetComponent<Brick> ().IncreaseDamageType ();
					hit.collider.gameObject.GetComponent<Brick> ().IncreaseDamageType ();
					foreach (Collider brickCollider in Physics.OverlapSphere(hit.transform.position, 10)) {
						if (brickCollider.gameObject.GetComponent<Brick> () != null && hit.collider.gameObject != brickCollider.gameObject) {
							brickCollider.gameObject.GetComponent<Brick> ().weakened = true;
						}
					}
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			foreach (GameObject brick in bricks) {
				//if (brick.GetComponent<Brick>().weakened == true) {
					brick.GetComponent<Rigidbody> ().isKinematic = false;
				//}
				StartCoroutine (DestroyBricks ());
			}
		}
	}

	public void Collapse(){
		Collider[] colliders = Physics.OverlapSphere (GameObject.Find ("ExplosionPosition").transform.position, 200f);
		foreach (Collider collider in colliders) {
			Rigidbody body = collider.GetComponent<Rigidbody> ();
			if (body != null) {
				body.AddRelativeForce (transform.forward * 2f, ForceMode.Force);
			}
		}
	}

	IEnumerator DestroyBricks(){
		foreach (GameObject brick in bricks) {
			if (brick != null && brick.GetComponent<Brick> ().damageType == Damage.Heavy) {
				Destroy (brick.gameObject);
				yield return new WaitForSecondsRealtime (.1f);
			}
		}
		Collapse ();
	}
}
