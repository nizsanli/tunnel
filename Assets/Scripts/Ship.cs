using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	float gravity;
	float thrustSpeed;
	float speed;

	float rot;

	Vector3 move;

	float downVel;	
	float upVel;

	// Use this for initialization
	void Start () {
		gravity = 0.01f;
		thrustSpeed = 0.04f;
		speed = 0.03f;
		rot = 0f;

		downVel = 1f;
		upVel = 1f;

		move = new Vector3(0f, 0f, 0f);
	}
	
	public void applyGravity()
	{
		//move.y -= gravity;
		upVel = 1f;
		rot -= downVel;
		downVel *= 1.05f;
		if (downVel > 5f)
		{
			downVel = 5f;
		}
	}

	public void thrust()
	{
		//move.y += thrustSpeed;
		downVel = 1f;
		rot += upVel;
		upVel *= 1.1f;
		if (upVel > 5f)
		{
			upVel = 5f;
		}
	}

	public void translate()
	{
		transform.Translate(transform.right * 0.4f, Space.World);
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

		/*
		if (transform.localRotation.z > 90f)
		{
			transform.localRotation = new Vector3(0f, 0f, 90f);
		}

		if (transform.localRotation.z < -90f)
		{
			transform.localRotation = new Vector3(0f, 0f, -90f);
		}
		*/

		//Camera.main.transform.Translate(move, Space.World);
	}
}
