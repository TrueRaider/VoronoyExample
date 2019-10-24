using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Directrise : MonoBehaviour
{
	public static Transform directriseTransform;

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
							Gizmos.color = Color.magenta;
							Gizmos.DrawSphere(vectorj, 1);
						}
					}
				}
			}
		}
	}
}
