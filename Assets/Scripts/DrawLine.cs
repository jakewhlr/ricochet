using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour
{
	private LineRenderer line;
	private EdgeCollider2D edCol;
	private bool isMousePressed;
	private List<Vector3> pointsList;
	private Vector3 mousePos;
	private bool doneLine = false;
	private bool justCreated = true;

	public List<Vector2> colliderPoints;

	public Color selColor;
	public float thickness;
	public Material lineMaterial;
	PhysicsMaterial2D pyMat;

	struct myLine
	{
		public Vector3 StartPoint;
		public Vector3 EndPoint;
	};

	void Awake()
	{
		line = gameObject.AddComponent<LineRenderer>();
		edCol = gameObject.AddComponent<EdgeCollider2D>();
		pyMat = new PhysicsMaterial2D();
		pyMat.bounciness = 1.1f;
		edCol.sharedMaterial = pyMat;
		line.SetVertexCount(0);
		line.SetColors(selColor, selColor);
		line.useWorldSpace = true;
		isMousePressed = false;
		pointsList = new List<Vector3>();
		colliderPoints = new List<Vector2>();
	}

	void Update ()
	{
		if(doneLine) return;

		if(justCreated)
		{
			justCreated = false;
			isMousePressed = true;
			line.SetWidth(thickness,thickness);
			line.material =  lineMaterial;
			line.SetVertexCount(0);
			pointsList.RemoveRange(0,pointsList.Count);
			line.SetColors(selColor, selColor);
		}
		else if(Input.GetMouseButtonUp(0))
		{
			isMousePressed = false;
			doneLine=true;
		}

		if(isMousePressed)
		{
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z=0;

			if (!pointsList.Contains (mousePos))
			{
				pointsList.Add (mousePos);
				colliderPoints.Add(new Vector2(mousePos.x, mousePos.y));
				if(colliderPoints.Count > 2)
				{
					edCol.points = colliderPoints.ToArray();
				}
				line.SetVertexCount (pointsList.Count);
				line.SetPosition (pointsList.Count - 1, (Vector3)pointsList [pointsList.Count - 1]);
			}
		}
	}
}
