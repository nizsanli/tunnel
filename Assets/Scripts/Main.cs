using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	private List<TunnelPart> parts;

	public Ship ship;
	public TunnelPart partPrefab;

	TunnelPart part;

	public Transform partsContainer;

	int numAdded = 0;

	// integer seed value to randomize levels
	public int generationSeed;

	// Use this for initialization
	void Start () {
		Camera.main.transform.Translate(new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0f, 0f));

		parts = new List<TunnelPart>(100);

		part = (TunnelPart) Instantiate(partPrefab, Vector3.zero, Quaternion.identity);
		part.init(Vector3.zero, generationSeed);
		parts.Add(part);
		part.transform.parent = partsContainer;
		numAdded++;
	}
	
	// Update is called once per frame
	void Update () {
		float section = Mathf.Floor(ship.transform.position.x / 100f);
		if (section+2 > numAdded)
		{
			Vector3 newStart = part.MidPoint;
			part = (TunnelPart) Instantiate(partPrefab, Vector3.zero, Quaternion.identity);
			part.init(newStart, generationSeed);
			parts.Add(part);
			part.transform.parent = partsContainer;
			numAdded++;
		}

		if (parts.Count > 3)
		{
			Destroy(parts[0].gameObject);
			parts.RemoveAt(0);
		}
		
		bool mouseEvent = false;
		if (Input.GetMouseButton(0))
		{
			mouseEvent = true;
		}
		
		controlShip(mouseEvent);
	}
	
	private void controlShip(bool e)
	{
		if (e == true)
		{
			ship.thrust();
		}
		else
		{
			ship.applyGravity();
		}
		ship.translate();
	}
}
