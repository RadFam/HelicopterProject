using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelicopterBaseAI : MonoBehaviour 
{
	[SerializeField]
	GameObject addPoint;
	[SerializeField]
	List<Transform> wayPoints;
	List<Vector3> wayPointsCoords;
	[SerializeField]
	Rigidbody myRB;
	[SerializeField]
	EnemyVPControl visualPart;
	[SerializeField]
	float myRBsphere;
	[SerializeField]
	float obstacleRebootTime;
	[SerializeField]
    float cruisingSpeed = 30.0f;
    [SerializeField]
    float rotationSpeed = 1.5f;
    [SerializeField]
    float nextWaypointDist = 0.2f;
	float currWaypointDist;
	float hitDist;
	[SerializeField]
	float sightRange;
	float sightRangeMax;
	float sightRangeMin;
	[SerializeField]
	float critAngle;

	Vector3 pointToMove;
	Vector3 wayVector;
	int pointNum;
	int increment;
	bool addedPoint;
	RaycastHit hit;
	float obstacleCheckTimer;
	List<Vector3> evadeVectors;

	// Use this for initialization
	void Start () 
	{
		wayPointsCoords = new List<Vector3>();
        foreach (Transform tr in wayPoints)
        {
            wayPointsCoords.Add(tr.position);
        }
		pointNum = 0;
		pointToMove = wayPointsCoords[pointNum];
		increment = 1;
		addedPoint = false;
		obstacleCheckTimer = 0.0f;
		evadeVectors = new List<Vector3>();

		sightRangeMax = sightRange;
		sightRangeMin = sightRange / 5.0f;
	}

	bool CheckForObstacle(Vector3 vct)
    {
		bool wasHit = Physics.CapsuleCast(transform.position, transform.position, myRBsphere, vct, out hit, sightRange);
        // Check if pointToMove is closer, than hit point
        if (wasHit)
        {
            hitDist = Vector3.Distance(transform.position, hit.point);
            if (hitDist < currWaypointDist)
            {
                return true;
            }
        }
        return false;
    }

	void ObstacleChecker(Vector3 checkVector) // transform.forward
	{
		//bool ans = CheckForObstacle(wayVector);
		bool ans = CheckForObstacle(checkVector);
		if (!ans)
		{
			return;
		}
		else
		{
			// Start find variants of evasion
			Debug.Log("Find obstacle: " + hit.point);

			float seekRadii = 2.0f * myRBsphere;
			bool canEvade = true;
			while(canEvade)
			{
				// Get eight directions to evade
				Vector3 vct_1 = new Vector3(checkVector.x, checkVector.y, checkVector.z);
				float angle = Mathf.Atan(seekRadii/sightRange);
				vct_1 = Quaternion.AngleAxis(-45, Vector3.Cross(Vector3.up, checkVector)) * vct_1;

				evadeVectors.Clear();
				evadeVectors.Add(vct_1);
				for (int i= 1; i < 8; ++i)
				{
					Vector3 vct_t = new Vector3(vct_1.x, vct_1.y, vct_1.z);
					vct_t = Quaternion.AngleAxis(45*i, checkVector) * vct_t;
					evadeVectors.Add(vct_t);
				}

				// Just For Debug
				Debug.Log("Candidates");
				foreach(Vector3 vct in evadeVectors)
				{
					Vector3 tmpPoint = vct*sightRange + transform.position;
					Debug.Log("Coord: " + tmpPoint);
				}

				foreach(Vector3 vct in evadeVectors)
				{
					if (!CheckForObstacle(vct))
					{
						// Get new point to move
						addedPoint = true;
						pointToMove = vct*sightRange + transform.position;
						wayVector = Vector3.Normalize(pointToMove-transform.position);
						canEvade = false;
						Debug.Log("New point to move: " + pointToMove);
						addPoint.transform.position = pointToMove;
						break;
					}
				}

				seekRadii += myRBsphere/4.0f;
			}
		}
	}

	void CheckForTargetPoint()
	{
		wayVector = Vector3.Normalize(pointToMove - transform.position);
		currWaypointDist = Vector3.Distance(transform.position, pointToMove);

		if (currWaypointDist <= nextWaypointDist)
		{
			Debug.Log("Check to find new wayPoint");
			if (!addedPoint)
			{
				pointNum += increment;
				if (pointNum == wayPointsCoords.Count || pointNum == -1)
				{
					increment *= -1;
					pointNum += increment;
				}
				pointToMove = wayPointsCoords[pointNum];
				Debug.Log("Current pos: " + transform.position + "   fly to: " + pointToMove);
			}
			else
			{
				Debug.Log("Can erase evade point");
				addedPoint = false;
				pointToMove = wayPointsCoords[pointNum];
				Debug.Log("Current pos: " + transform.position + "   fly to: " + pointToMove);
			}
			wayVector = Vector3.Normalize(pointToMove - transform.position);
			sightRange = sightRangeMax;
			CheckForObstacle(wayVector);

		}
	}

	void CheckRotation()
	{
		if (Mathf.Abs(Vector3.Angle(transform.forward, wayVector)) >= critAngle)
		{
			if (sightRange == sightRangeMax)
			{
				obstacleCheckTimer = obstacleRebootTime;
			}
			sightRange = sightRangeMin;
		}
		else
		{
			if (sightRange == sightRangeMin)
			{
				obstacleCheckTimer = obstacleRebootTime;
			}
			sightRange = sightRangeMax;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (obstacleCheckTimer >= obstacleRebootTime)
		{
			ObstacleChecker(transform.forward);
			obstacleCheckTimer = 0.0f;
		}
		gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(pointToMove-transform.position), rotationSpeed * Time.deltaTime);
		CheckForTargetPoint();
		CheckRotation();

        // Вычисляем вектор скорости движения
        myRB.velocity = myRB.transform.forward * cruisingSpeed;
		obstacleCheckTimer += Time.deltaTime;

		visualPart.SetForwardOrientation(new Vector3(-myRB.transform.forward.z, 0, myRB.transform.forward.x));
		
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, myRBsphere);
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * (sightRange + myRBsphere));
	}
}
