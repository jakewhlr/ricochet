using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultipleLines : MonoBehaviour {

	private List<GameObject> lineHolder;

	public string SortingLayerName = "";
	public Color LineColor;
	public Transform parent;
	public float lineThickness = 0f;
	public Material lineMat;

	void Start () {
		lineHolder = new List<GameObject>(); //creating the list in which we will hold the lines
	}

	void Update () {
		if(Input.GetMouseButtonDown(0)) //if mouse button was pressed we create a new line
		{
			GameObject newLine = new GameObject("line"+lineHolder.Count.ToString()); //create a new GameObject for the line, adding all the components and adding it to the lineHolder
			if(parent != null){
				newLine.transform.parent = parent.transform; //add it to the parent gameObject
			}

			DrawLine newLineRef = newLine.AddComponent<DrawLine>(); //Adding the DrawLine component which handles the LineRenderer
			newLineRef.selColor = LineColor;
			newLineRef.thickness = lineThickness;
			if(lineMat == null)
			{
				newLineRef.lineMaterial = new Material(Shader.Find("Sprites/Default"));
			}else{
				newLineRef.lineMaterial = lineMat;
			}
			if(SortingLayerName != ""){
				newLine.GetComponent<Renderer>().sortingLayerName = SortingLayerName; //we set the line renderer to display on the layer we want
			}

			lineHolder.Add(newLine);
		}

		if(Input.GetMouseButtonUp(0))
		{
			List<GameObject> toRem = new List<GameObject>();

			foreach(GameObject l in lineHolder)
			{
				if(l.GetComponent<DrawLine>().colliderPoints.Count < 4) //checking to see if there are any lines shorter than 4 points
				{
					toRem.Add(l);
				}
			}

			while(toRem.Count > 0) // removing lines that are shorter than 4 points
			{
				lineHolder.Remove(toRem[0]);
				Destroy(toRem[0]);
				toRem.RemoveAt(0);
			}
		}
	}

	public void removeAllLines() //call this function when you want to destroy all lines
	{
		while(lineHolder.Count > 0)
		{
			Destroy(lineHolder[0]);
			lineHolder.RemoveAt(0);
		}
	}
}
