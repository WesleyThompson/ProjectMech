using UnityEngine;
using System.Collections;

public class SmokeDisableEnableTest : MonoBehaviour {
	/**
	 *	Because we use pooling for our projectiles, I wanted to test the effects of rigid body physics when you disable an object.
	 *	It appears that after renabling a disabled object, it
	 * 
	 * 
	 * 
	 * 
	 **/
	private bool active = false;
	private GameObject testObject;
	private Rigidbody rb;
	private string[] functionList = {"TestSetActive", "TestSetActiveAndRigidbody"};
	private bool testDone = false;
	private float forceDelayTime = 3.0F;

	void Start() {
		testObject = GameObject.Find ("testObject");
		if (testObject == null) {
			print ("testObject was not found in the scene, if it is in the scene, make sure it is named testObject");
		} else {
			rb = testObject.GetComponent<Rigidbody> ();
			print ("disabling gravity");
			rb.useGravity = false;

			StartCoroutine (RunTests ());
		}
	}

	IEnumerator RunTests() {
		foreach (string currFunction in functionList) {
			print ("---STARTING TEST: " + currFunction + "---");
			testDone = false;
			StartCoroutine (currFunction, currFunction);
			yield return new WaitUntil (() => testDone);
			print ("---FINISHED TEST: " + currFunction + "---");
		}
	}

	void onOff () {
		testObject.SetActive (active);
		if (!active) {
			print ("off");
		} else {
			print ("on");
		}
		active = !active;
	}

	void onOffRigidbody() {
		testObject.SetActive (active);
		if (!active) {
			rb.isKinematic = true;
			print ("off and rigid body is kinementic; when " + testObject.name + " re-activates, it should NOT be moving");
		} else {
			rb.isKinematic = false;
			print ("on and rigid body is NOT kinemetic");
		}
		active = !active;
	}

	/*---tests---*/
	IEnumerator TestSetActive(string thisFunctionsName) {
		print (thisFunctionsName + ": applying force in " + forceDelayTime + " seconds");
		rb.velocity = testObject.transform.forward;
		yield return new WaitForSeconds (2);
		onOff ();
		yield return new WaitForSeconds (2);
		onOff ();
		yield return new WaitForSeconds (2);

		testDone = true;
	}

	IEnumerator TestSetActiveAndRigidbody(string thisFunctionsName) {
		print (thisFunctionsName + ": applying force in " + forceDelayTime + " seconds");
		rb.velocity = testObject.transform.forward;
		yield return new WaitForSeconds (2);
		onOffRigidbody ();
		yield return new WaitForSeconds (2);
		onOffRigidbody ();
		yield return new WaitForSeconds (2);

		testDone = true;
	}
	/*-----------*/
}
