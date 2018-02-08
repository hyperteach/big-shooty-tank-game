using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WanderAgent : MonoBehaviour {

	NavMeshAgent agent;
	public Rect bounds; 

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.destination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, agent.destination)<0.2f ||
			agent.pathStatus != NavMeshPathStatus.PathComplete){
			agent.destination = new Vector3(
				Mathf.Lerp(bounds.xMin, bounds.xMax, Random.value),
				0,
				Mathf.Lerp(bounds.yMin, bounds.yMax, Random.value)
			);
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine(
			new Vector3(bounds.xMin, 0, bounds.yMin),
			new Vector3(bounds.xMin, 0, bounds.yMax)
		);
		Gizmos.DrawLine(
			new Vector3(bounds.xMin, 0, bounds.yMax),
			new Vector3(bounds.xMax, 0, bounds.yMax)
		);
		Gizmos.DrawLine(
			new Vector3(bounds.xMin, 0, bounds.yMin),
			new Vector3(bounds.xMax, 0, bounds.yMin)
		);
		Gizmos.DrawLine(
			new Vector3(bounds.xMax, 0, bounds.yMin),
			new Vector3(bounds.xMax, 0, bounds.yMax)
		);
	}
}
