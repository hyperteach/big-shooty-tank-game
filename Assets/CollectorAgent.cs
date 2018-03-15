using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CollectorAgent : MonoBehaviour {

	NavMeshAgent agent;

	[HideInInspector]
	public GameObject target;

	// Use this for initialization
	void Start () {
		target = null;
		agent = GetComponent<NavMeshAgent>();
		agent.destination = transform.position;
		FoodSpawner.agents.Add(this);
	}
	void OnDestroy(){
		FoodSpawner.agents.Remove(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (target!=null){
			agent.destination = target.transform.position;
			if (Vector3.Distance(transform.position, target.transform.position)<0.3f){
				Destroy(target);
				target = null;
			}
		}
	}

}
