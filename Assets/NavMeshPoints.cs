using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

public static class NavMeshPoints
{
    public static List<Vector3> _validPositions = new List<Vector3>();
    public static List<Vector3> _gridPositions;
    private const int BoxSize = 2;
 
    public static void UpdatePositions(List<NavMeshData> data)
    {
        NavMeshHit hit;
        var validPositions = new List<Vector3>();
        var gridPositions = new List<Vector3>();
 
        var sw = Stopwatch.StartNew();
        foreach (var surface in data)
        {
            var min = surface.sourceBounds.min + surface.position;
            var max = surface.sourceBounds.max + surface.position;
            Debug.LogError(surface.sourceBounds.center);
            Debug.LogError(surface.sourceBounds.size);
            
            var surfaceCenterY = surface.sourceBounds.center.y;
            Debug.LogError(surfaceCenterY);
 
            for (var x = min.x; x <= max.x; x = x + BoxSize)
            {
                for (var z = min.z; z <= max.z; z = z + BoxSize)
                {                  
                    var pos = new Vector3(x, surfaceCenterY, z);
 
                    //if (NavMesh.FindClosestEdge(pos, out hit, -1))
                    
                    if (NavMesh.SamplePosition(pos, out hit, 1f, -1))
                    {
                        validPositions.Add(hit.position);
                    }
                    gridPositions.Add(pos);
                }
            }      
        }
        sw.Stop();
        Debug.Log($"UpdatePositions took {sw.Elapsed.TotalMilliseconds:N4}ms");
        _validPositions = validPositions;
        _gridPositions = gridPositions;
    }
 
    public static Vector3 RandomPosition(Vector3 origin, float radius)
    {
        var randDirection = UnityEngine.Random.insideUnitSphere * radius;
        randDirection += origin;
        NavMeshHit navHit;
        var t1 = Stopwatch.StartNew();
        NavMesh.SamplePosition(randDirection, out navHit, radius, -1);
        t1.Stop();
        Debug.Log($"RandomPosition took {t1.Elapsed.TotalMilliseconds:N4}ms");    
        return navHit.position;
    }
 
    public static Vector3 RandomPosition()
    {
        var t1 = Stopwatch.StartNew();
        var position = _validPositions.ElementAtOrDefault(UnityEngine.Random.Range(0, _validPositions.Count));
        t1.Stop();
        Debug.Log($"RandomPosition2 took {t1.Elapsed.TotalMilliseconds:N4}ms");
        return position;
    }
 
}