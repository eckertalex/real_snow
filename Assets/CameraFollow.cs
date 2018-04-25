using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	public float distance;
	public float height;
	public float rotationDamping;
	public float heightDamping;
	public float zoomRatio;
	public float defaultFOV;
	
	private float rotation_vector;

	void FixedUpdate() {
		Vector3 local_velocity = target.InverseTransformDirection(target.GetComponent<Rigidbody>().velocity);
		if(local_velocity.z < -0.5f) {
			rotation_vector = target.eulerAngles.y + 100;
		}
		else {
			rotation_vector = target.eulerAngles.y;
		}

		float acceleration = target.GetComponent<Rigidbody>().velocity.magnitude;
		Camera.main.fieldOfView = defaultFOV + acceleration * zoomRatio * Time.deltaTime;
	}

	void LateUpdate() {
		float wantedAngle = rotation_vector;

		float wantedHeight = (target.position.y + height) / 6;
		float myAngle = transform.eulerAngles.y;
		float myHeight = transform.position.y;

		myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
		myHeight = Mathf.LerpAngle(myHeight, wantedHeight, heightDamping * Time.deltaTime);

		Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
		
		transform.position =  target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		Vector3 temp = transform.position;
		temp.y = myHeight;
		transform.position = temp;

		transform.LookAt(target);
	}
}
