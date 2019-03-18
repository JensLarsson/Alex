using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleDude : MonoBehaviour {

    public bool displayPath;
    public float speed;
    public float walkingRadius;
    public float timeCheck;
    public float doorNoGoRadius;
    Vector3[] path;
    int targetIndex;
    bool onPath = false;
    bool check = false;

    Timer timer = new Timer();
    GameObject[] doors;

    Vector3 target;

    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
        timer.Duration = timeCheck;
    }

    void Update()
    {
        timer.Time += Time.deltaTime;
        if (timer.expired)
        {
            if (!check)
            {
                onPath = false;
                check = true;
            }
            if (!onPath)
            {
                target = doors[Random.Range(0, doors.Length)].transform.position;
                PathRequestManager.RequestPath(transform.position, target, OnPathFound);
            }
        }
        else
        {
            if (!onPath)
            {
                target = NewTarget();
                PathRequestManager.RequestPath(transform.position, target, OnPathFound);
            }
        }
    }

    Vector3 NewTarget()
    {
        Vector3 temp = new Vector3(Random.Range(transform.position.x - walkingRadius, transform.position.x + walkingRadius), Random.Range(transform.position.y - walkingRadius, transform.position.y + walkingRadius));
        int tempInt = 0;
        while (true)
        {
            tempInt = 0;
            for (int i = 0; i < doors.Length; i++)
            {
                if (Vector3.Distance(temp, doors[i].transform.position) < doorNoGoRadius)
                {
                    temp = new Vector3(Random.Range(transform.position.x - walkingRadius, transform.position.x + walkingRadius), Random.Range(transform.position.y - walkingRadius, transform.position.y + walkingRadius));
                    tempInt++;
                }
            }
            if (tempInt == 0)
            {
                break;
            }
        }
        return temp;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            onPath = true;
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = transform.position;
        if (path.Length != 0)
        {
            currentWaypoint = path[0];
			LookTowardsWaypoint (currentWaypoint);
        }
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    onPath = false;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
				LookTowardsWaypoint (currentWaypoint);
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

	void LookTowardsWaypoint(Vector3 targetPosition)
	{
		Vector3 lookDirection = targetPosition - transform.position;
		float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

    public void OnDrawGizmos()
    {
        if (path != null && displayPath)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], new Vector3(0.2f, 0.2f));

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Door")
        {
            gameObject.SetActive(false);
        }
    }
}
