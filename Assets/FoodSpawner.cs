using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoodSpawner : MonoBehaviour {

	static List<CollectorAgent> _agents;
	public static List<CollectorAgent> agents{
		get {
			if (_agents==null){
				_agents = new List<CollectorAgent>();
			}
			return _agents;
		}
	}
	public Rect bounds;

	public GameObject foodPrefab;
	public float timeToSpawn = 5;
	float cool;

	// Use this for initialization
	void Start () {
		cool = timeToSpawn;
	}
	
	// Update is called once per frame
	void Update () {
		cool -= Time.deltaTime;
		if (cool<=0){
			cool+=timeToSpawn;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(new Vector3(
				Mathf.Lerp(bounds.xMin, bounds.xMax, Random.value),
				0,
				Mathf.Lerp(bounds.yMin, bounds.yMax, Random.value)
			),out hit, 1000, NavMesh.AllAreas)){
				GameObject food = Instantiate(foodPrefab);
				food.transform.position = hit.position;
				float minDist = Mathf.Infinity;
				CollectorAgent closest = null;
				foreach (CollectorAgent agent in agents){
					NavMeshPath path = new NavMeshPath();
					NavMesh.CalculatePath(agent.transform.position, food.transform.position, NavMesh.AllAreas, path);
					float thisDist = pathLength(path);
					if (agent.target==null && thisDist<minDist){
						minDist = thisDist;
						closest = agent;
					}
				}
				if (closest!=null){
					closest.target = food;
				} else {
					Destroy(food);
				}
			}
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

	void OnDrawGizmos(){
		Gizmos.color = Color.magenta;
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
