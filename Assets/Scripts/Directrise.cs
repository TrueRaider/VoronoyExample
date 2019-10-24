using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Directrise : MonoBehaviour
{
	public static Transform directriseTransform;

	public List<Point> intersectionPoints = new List<Point>();

	public Parabola[] _allParabolas;
	void Awake ()
	{
		directriseTransform = transform;
	}

	private void Update()
	{
		if(directriseTransform == null)
			directriseTransform = transform;
	}

	private void OnDrawGizmos()
	{
		for (int i = 0; i < _allParabolas.Length; i++)
		{
			for (int j = 0; j < _allParabolas.Length; j++)
			{
				if (i == j)
					continue;

				foreach (var vectorj in _allParabolas[j].OutputCollection)
				{
					foreach (var vectori in _allParabolas[i].OutputCollection)
					{
						if (Vector3.Distance(vectorj, vectori) < 0.3f)
						{
							if (intersectionPoints.Exists(point =>
								i == point.firstParabolaId && j == point.secondParabolaId ||
								i == point.secondParabolaId && j == point.firstParabolaId))
							{
								var tempPoint = intersectionPoints.Find(p =>
									(i == p.firstParabolaId && j == p.secondParabolaId ||
									 i == p.secondParabolaId && j == p.firstParabolaId) && !p._isFirst);
								
								tempPoint.SetPoint(vectori);
							}
							else
							{
								intersectionPoints.Add(new Point(i, j, vectorj, true));
								intersectionPoints.Add(new Point(i, j, vectorj, false));
							}
						}
					}
				}
			}
		}
		
		Gizmos.color = Color.magenta;
		foreach (var point in intersectionPoints)
		{
			Gizmos.DrawSphere(point.point, 1);
			
			
		}
	}

	public struct Point
	{
		public int firstParabolaId;
		public int secondParabolaId;
		public Vector3 point;
		public bool _isFirst;

		public Point(int parabola1, int parabola2, Vector3 value, bool isFirst)
		{
			firstParabolaId = parabola1;
			secondParabolaId = parabola2;
			point = value;
			_isFirst = isFirst;
		}

		public void SetPoint(Vector3 value)
		{
			point = value;
		}
	}
}
