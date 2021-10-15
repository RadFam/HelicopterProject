using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle
{
	public Bounds bounds;
	public Vector3 center;
}
public class WorldSubdivision
{
	public List<WorldCube> allCubes;
	public List<Obstacle> obstacles;
	public static float minSize = 0.1f;

	public WorldSubdivision(Vector3 center, float initSize,  List<Obstacle> objs)
	{
		allCubes = new List<WorldCube>();
		WorldCube rootCude = new WorldCube(center, initSize);
		allCubes.Add(rootCude);
		obstacles = objs;
	}

	public void Add()
	{

	}
}
