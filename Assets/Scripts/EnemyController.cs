using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject moveTarget;
	void Start () {
        agent = GetComponent<NavMeshAgent>();//transform.FindChild("NavMesh").
        // agent.updateRotation = false;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Vector3 playerDirection = transform.position - moveTarget.transform.position;
        //Debug.DrawRay(transform.position, playerDirection, Color.blue);

        playerDirection *= -1;

        if (Physics.Raycast(transform.position, playerDirection, out hit))
        {
            print(hit.transform.parent.name);
        }
        if (hit.transform.parent.name != "PlayerTemp")
        {
            agent.SetDestination(moveTarget.transform.position);
        } else
        {
            agent.SetDestination(transform.position);
        }
    }
}
