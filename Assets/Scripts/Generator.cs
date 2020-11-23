using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	MeshCollider meshCollider;
	Mesh mesh;

	Vector3 midPoint;
	float thickness;
	float width;
	float spacing;

	public Ship ship;

	private List<Generator> chunks;

	// Use this for initialization
	void Start () {
		chunks = new List<Generator>(100);

		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshCollider = gameObject.AddComponent<MeshCollider>();

		meshRenderer.material = (Material) Resources.Load("TunnelMat");

		midPoint = new Vector3(0f, 0f, 0f);
		thickness = Camera.main.orthographicSize + 50f;
		width = 15f;
		spacing = 0.5f;

		Camera.main.transform.Translate(new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0f, 0f));

		List<Vector3> verts = new List<Vector3>(100);
		List<Vector2> uvs = new List<Vector2>(100);
		List<int> tris = new List<int>(100);

		System.Random rand = new System.Random();

		int index = 0;
		int numParts = 1000;

		float lastup = midPoint.y + width;
		float lastdown = midPoint.y - width;

		for (int i = 0; i < numParts; i++)
		{
			float botOffset = (Mathf.PerlinNoise((midPoint.x+1000f)*0.5f, midPoint.y*0.1f+500f) - 0.5f) * 5f;
			float topOffset = (Mathf.PerlinNoise((midPoint.x+300f)*0.5f, midPoint.y*0.1f+500f) - 0.5f) * 5f;

			verts.Add(new Vector3(midPoint.x, lastup, 0f));
			verts.Add(new Vector3(midPoint.x, midPoint.y + width + thickness, 0f));
			verts.Add(new Vector3(midPoint.x + spacing, midPoint.y + width + thickness, 0f));
			verts.Add(new Vector3(midPoint.x + spacing, midPoint.y + width + topOffset, 0f));
			
			verts.Add(new Vector3(midPoint.x, midPoint.y - width - thickness, 0f));
			verts.Add(new Vector3(midPoint.x, lastdown, 0f));
			verts.Add(new Vector3(midPoint.x + spacing, midPoint.y - width + botOffset, 0f));
			verts.Add(new Vector3(midPoint.x + spacing, midPoint.y - width - thickness, 0f));

			lastup = midPoint.y + width + topOffset;
			lastdown = midPoint.y - width + botOffset;
			
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
			midPoint.y += (Mathf.PerlinNoise(midPoint.x*0.1f, midPoint.y*0.1f) - 0.5f) * 2f;
		}

		Mesh mesh = new Mesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();

		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.Optimize();

		meshFilter.sharedMesh = mesh;
		meshCollider.sharedMesh = mesh;
	}
}
