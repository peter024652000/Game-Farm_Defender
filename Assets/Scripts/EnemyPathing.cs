using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    //configuration parameter
    PatternConfig patternConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;


	// Use this for initialization
	void Start () {
        waypoints = patternConfig.GetWayPoints();
        transform.position = waypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    private void Move()
    {

        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = patternConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);


            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }

        else
        {

            Destroy(gameObject);
        }
    }

    public void SetWaveConfig(PatternConfig patternConfig) //(this waveConfig here is a local variable.) 
    /* This method allows you to know which path is using from another script(in this case is the wave(one of WaveConfig) chosen in EnemySpawner.cs) 
    automatically instead of putting script manually via serializeField.
    */
    {
        this.patternConfig = patternConfig;//1st waveConfig is from this script, 2nd waveConfig is from this method. 
    }
}
