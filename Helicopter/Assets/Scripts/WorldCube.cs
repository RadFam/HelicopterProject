using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldCube
{
	public Vector3 myCenter;
	public Bounds myBounds;
	public float sideSize;
	public List<Vector3> neighbours;

	public WorldCube(Vector3 center, float side)
	{
		myCenter = center;
		sideSize = side;
		myBounds = new Bounds(myCenter, new Vector3(sideSize, sideSize, sideSize));
		neighbours = new List<Vector3>();
	}

	public bool InCube(Vector3 point)
	{
		return myBounds.Contains(point);
	}

	public void SetNeighbour(WorldCube neib)
	{
		neighbours.Add(neib.myCenter);
	}

	public void SetNeighbour(List<WorldCube> neibs)
	{
		foreach(WorldCube neib in neibs)
		{
			neighbours.Add(neib.myCenter);
		}
	}

	public List<WorldCube> Subdivide()
	{
        if (sideSize / 2.0f >= WorldSubdivision.minSize)
        {
            List<WorldCube> subNodes = new List<WorldCube>();
            for (int i = 0; i < 8; ++i)
            {
                int xDir = i % 2 == 0 ? -1 : 1;
                int yDir = i > 3 ? -1 : 1;
                int zDir = (i < 2 || (i > 3 && i < 6)) ? -1 : 1;
                subNodes.Add(new WorldCube(new Vector3(xDir * sideSize / 2.0f, yDir * sideSize / 2.0f, zDir * sideSize / 2.0f), sideSize / 2.0f));
            }

			return subNodes;
        }
		
		return null;
    }

	public void DrawBorder()
	{
		Gizmos.color = new Color(1.0f, 0, 1.0f);
		Gizmos.DrawWireCube(myBounds.center, myBounds.size);
	}
}
