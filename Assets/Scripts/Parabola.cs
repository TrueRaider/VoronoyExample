using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Parabola : MonoBehaviour
{

	public float FocusToDirectriseDistance = 2;

	public double stepValues = 10;
	public double biasCount = 4;

	public Vector3 center = new Vector3(1, 1, 1);
	
	private List<Vector3> points = new List<Vector3>();
	private List<Vector3> negativePoints = new List<Vector3>();
	
	public List<Vector3> OutputCollection = new List<Vector3>();
	
	void OnDrawGizmos () 
	{
		Gizmos.color = Color.green;

		center = this.transform.position;

		FocusToDirectriseDistance = (Directrise.directriseTransform.position - transform.position).x;
		
		points = new List<Vector3>();
		negativePoints = new List<Vector3>();
		OutputCollection = new List<Vector3>();
		
		for (int i = 0; i < stepValues; i++)
		{
			float x = i;

			double bias = 1 / biasCount;

			for (int j = 0; j < biasCount; j++)
			{
				if(j != 0)
					x += (float) bias;
				double y = Math.Sqrt(-2 * FocusToDirectriseDistance * x);

				Gizmos.DrawSphere(new Vector3((center.x + x), center.y , ((float) y) + center.z), 0.1f);
				Gizmos.DrawSphere(new Vector3((center.x + x), center.y , (-(float) y) + center.z), 0.1f);
			
				points.Add(new Vector3((center.x + x), center.y , ((float) y) + center.z));
				negativePoints.Add(new Vector3((center.x + x), center.y , (-(float) y) + center.z));
			}
		}

		for (int i = 0; i < points.Count - 1; i++)
		{
			Gizmos.DrawLine(points[i], points[i + 1]);
		}
		
		for (int i = 0; i < negativePoints.Count - 1; i++)
		{
			Gizmos.DrawLine(negativePoints[i], negativePoints[i + 1]);
		}

		OutputCollection = points.Concat(negativePoints).ToList();

		Gizmos.color = Color.red;
		
		Gizmos.DrawLine(new Vector3(Directrise.directriseTransform.position.x, Directrise.directriseTransform.position.y, Directrise.directriseTransform.position.z - 100), new Vector3(Directrise.directriseTransform.position.x, Directrise.directriseTransform.position.y, Directrise.directriseTransform.position.z + 100));
	}
}
