using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelPart : MonoBehaviour {

	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	Mesh mesh;

	Vector3 midPoint;
	public Vector3 MidPoint
	{
		get {return midPoint;}
	}

	float thickness;
	float width;
	float spacing;

	public void init(Vector3 start, int seed)
	{
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = (Material) Resources.Load("TunnelMat");

		meshFilter = gameObject.AddComponent<MeshFilter>();

		midPoint = start;
		thickness = Camera.main.orthographicSize + 50f;
		width = 10f;
		spacing = 0.5f;

		List<Vector3> verts = new List<Vector3>(1000);
		List<Vector2> uvs = new List<Vector2>(1000);
		List<int> tris = new List<int>(1000);
		
		System.Random rand = new System.Random(seed);
		
		int index = 0;
		int numParts = 200;

		float randY = 0f;
		
		for (int i = 0; i < numParts; i++)
		{	
			verts.Add(new Vector3(midPoint.x, midPoint.y + width, 0f));
			verts.Add(new Vector3(midPoint.x, midPoint.y + width + thickness, 0f));
			verts.Add(new Vector3(midPoint.x + spacing, midPoint.y + width + thickness, 0f));

			randY = (Mathf.PerlinNoise(midPoint.x*0.1f, midPoint.y*0.1f) - 0.5f) * 3f;
			midPoint.y += randY;

			verts.Add(new Vector3(midPoint.x + spacing, midPoint.y + width, 0f));

			midPoint.y -= randY;

			verts.Add(new Vector3(midPoint.x, midPoint.y - width - thickness, 0f));
			verts.Add(new Vector3(midPoint.x, midPoint.y - width, 0f));

			midPoint.y += randY;

			verts.Add(new Vector3(midPoint.x + spacing, midPoint.y - width, 0f));
			verts.Add(new Vector3(midPoint.x + spacing, midPoint.y - width - thickness, 0f));
			
			uvs.Add(new Vector2(0f, 0f));
			uvs.Add(new Vector2(0f, 0f));
			uvs.Add(new Vector2(0f, 0f));
			uvs.Add(new Vector2(0f, 0f));
			uvs.Add(new Vector2(0f, 0f));
			uvs.Add(new Vector2(0f, 0f));
			uvs.Add(new Vector2(0f, 0f));
			uvs.Add(new Vector2(0f, 0f));
			
			tris.Add(index + 0);
			tris.Add(index + 1);
			tris.Add(index + 2);
			tris.Add(index + 2);
			tris.Add(index + 3);
			tris.Add(index + 0);
			
			index += 4;
			
			tris.Add(index + 0);
			tris.Add(index + 1);
			tris.Add(index + 2);
			tris.Add(index + 2);
			tris.Add(index + 3);
			tris.Add(index + 0);
			
			index += 4;
			
			midPoint.x += spacing;

			/*
			randY = (Mathf.PerlinNoise(midPoint.x*0.1f, midPoint.y*0.1f) - 0.5f) * 2f;
			midPoint.y += randY;
			*/
		}
		
		Mesh mesh = new Mesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.Optimize();
		
		meshFilter.sharedMesh = mesh;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
