using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	float rot;

	public float baseDownVel = .5f;
	float currentDownVel;

	public float baseUpVel = .5f;
	float currentUpVel;

	public float accelAmt = 1.05f;

	public float sideVel;

	// Use this for initialization
	void Start () {
		rot = 0f;

		currentDownVel = baseDownVel;
		currentUpVel = baseUpVel;
	}
	
	public void applyGravity()
	{
		rot -= baseDownVel;
		currentDownVel *= accelAmt;
		currentUpVel = baseUpVel;
	}

	public void thrust()
	{
		rot += baseUpVel;
		currentUpVel *= accelAmt;
		currentDownVel = baseDownVel;
	}

	public void translate()
	{
		transform.Translate(transform.right * sideVel, Space.World);
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

		if (rot > 90f)
		{
			rot = 90f;
		}
		if (rot < -90f)
		{
			rot = -90f;
		}
		transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rot));
	}
}
