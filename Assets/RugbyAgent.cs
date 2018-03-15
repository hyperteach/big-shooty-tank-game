using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class RugbyAgent : MonoBehaviour {

	public int teamNumber;
	NavMeshAgent agent;

	Vector3 idle;

	[HideInInspector]
	public Ball target;

	// Use this for initialization
	void Start () {
		target = null;
		agent = GetComponent<NavMeshAgent>();
		agent.destination = transform.position;
		BallGame.instance.agents.Add(this);
		foreach (Renderer rend in GetComponentsInChildren<Renderer>()){
			rend.material.color = BallGame.instance.teams[teamNumber].color;
		}
		NavMeshHit hit;
		if (NavMesh.SamplePosition(new Vector3(
				Mathf.Lerp(BallGame.instance.bounds.xMin, BallGame.instance.bounds.xMax, Random.value),
				0,
				Mathf.Lerp(BallGame.instance.bounds.yMin, BallGame.instance.bounds.yMax, Random.value)
			), out hit, 1000, NavMesh.AllAreas))
		{
			idle = hit.position;
		}
	}
	void OnDestroy(){
		if (BallGame.instance!=null)
			BallGame.instance.agents.Remove(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (target!=null){
			if (target.carriedBy==null){
				agent.destination = target.transform.position;
				if (Vector3.Distance(transform.position, target.transform.position)<1f){
					target.carriedBy = this;
				}
			} else if (target.carriedBy == this){
				target.transform.position = this.transform.position + Vector3.up*2.5f;
				Vector3 goal = BallGame.instance.teams[teamNumber].goals;
				agent.destination = goal;
				if (Vector3.Distance(goal, transform.position)<1f){
					Destroy(target.gameObject);
					target = null;
					BallGame.instance.Score(this);
				}
			} else {
				agent.destination = idle;
				target = null;
			}
		} else {
			agent.destination = idle;
		}
	}

	float pathLength(NavMeshPath path){
		if (path.corners.Length < 2)
            return 0;
        
        Vector3 previousCorner = path.corners[0];
        float lengthSoFar = 0.0F;
        int i = 1;
        while (i < path.corners.Length) {
            Vector3 currentCorner = path.corners[i];
            lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return lengthSoFar;
	}

}
