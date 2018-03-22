using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public struct Team {
	public string name;
	public Color color;
	public Vector3 goals;
}

public delegate void BallGameEvent(Team team);

public class BallGame : MonoBehaviour {

	public static event BallGameEvent TeamScored;
	public static event BallGameEvent BallPickedUp;

	List<RugbyAgent> _agents;
	public List<RugbyAgent> agents{
		get {
			if (_agents==null){
				_agents = new List<RugbyAgent>();
			}
			return _agents;
		}
	}


	static BallGame _instance;
	public static BallGame instance {
		get {
			if (_instance == null){
				_instance = FindObjectOfType<BallGame>();
			}
			return _instance;
		}
	}

	public Rect bounds;

	public Ball ball;
	public float timeToSpawn = 5;
	float cool;

	public Team[] teams;

	// Use this for initialization
	void Start () {
		if (_instance!=null && _instance!=this){
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
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
				Ball food = Instantiate(ball);
				food.transform.position = hit.position;
				foreach (Team team in teams){
					List<RugbyAgent> tAgents = new List<RugbyAgent>();
					foreach (RugbyAgent agent in agents) {
						if (teams[agent.teamNumber].name == team.name){
							tAgents.Add(agent);
						}
					}
					float minDist = Mathf.Infinity;
					RugbyAgent closest = null;
					foreach (RugbyAgent agent in tAgents){
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
					}
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
		foreach (Team team in teams){
			Gizmos.color = team.color;
			Gizmos.DrawWireCube(team.goals, Vector3.one);
		}
	}

	public void Score(RugbyAgent ra){
		if (TeamScored!=null){
			TeamScored(teams[ra.teamNumber]);
		}
	}
	public void BallPickup(RugbyAgent ra){
		if (BallPickedUp!=null){
			BallPickedUp(teams[ra.teamNumber]);
		}
	}

}
