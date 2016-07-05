using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject moveTarget;
    private float width;
    private float height;
    private EnemyScan enemyScanScript;
    private EnemyShooting enemyShootingScript;

    private Vector3 startRotation;
    private Vector3 endRotation;
    private float headTurnStartTime;
    private float smoothTime = 0.2f;
    private Transform topTransform;

    void Start () {
        agent = GetComponent<NavMeshAgent>();//transform.FindChild("NavMesh").
        enemyScanScript = GetComponent<EnemyScan>();
        enemyShootingScript = GetComponent<EnemyShooting>();
        width = 9;
        height = 5;
        topTransform = transform.FindChild("Top");
        // agent.updateRotation = false;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit lHit;
        RaycastHit rHit;
        RaycastHit cHit;
        Vector3 enemyPositionL = new Vector3(transform.position.x - width, transform.position.y + height, transform.position.z);
        Vector3 enemyPositionR = new Vector3(transform.position.x + width, transform.position.y + height, transform.position.z);
        Vector3 enemyPositionC = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);

        Vector3 enemyDirectionL = enemyPositionL - moveTarget.transform.position;
        Vector3 enemyDirectionR = enemyPositionR - moveTarget.transform.position;
        Vector3 enemyDirectionC = transform.FindChild("Top").rotation * Vector3.forward * 20;

        enemyDirectionL *= -1;
        enemyDirectionR *= -1;

        Debug.DrawRay(enemyPositionL, enemyDirectionL, Color.blue);
        Debug.DrawRay(enemyPositionR, enemyDirectionR, Color.blue);
        Debug.DrawRay(enemyPositionC, enemyDirectionC, Color.red);

        bool didHitL = Physics.Raycast(enemyPositionL, enemyDirectionL, out lHit);
        bool didHitR = Physics.Raycast(enemyPositionR, enemyDirectionR, out rHit);
        bool didHitC = Physics.Raycast(enemyPositionC, enemyDirectionC, out cHit);

        //If can see entire player
        if (didHitL && didHitR && didHitC && lHit.transform.parent.name == "PlayerTemp" && rHit.transform.parent.name == "PlayerTemp")
        {
            //If rotating head sees player
            if (cHit.transform.parent.name == "PlayerTemp")
            {
                enemyScanScript.StopScanning();
                enemyShootingScript.SetShoot(true, moveTarget);
            }

            if(!enemyScanScript.IsScanning())
            {
                Vector3 targetDir = enemyPositionC - moveTarget.transform.position;
                print(Mathf.Atan2(targetDir.z, targetDir.x) * 180* Mathf.PI);
                //topTransform.localEulerAngles = new Vector3(topTransform.localEulerAngles.x, 0, topTransform.localEulerAngles.z);
            }
            agent.SetDestination(transform.position);

        } else
        {
            agent.SetDestination(moveTarget.transform.position);
            enemyScanScript.StartScanning();
            enemyShootingScript.SetShoot(false, null);
        }
    }

    private void HeadFollowTarget()
    {
        //float tRot = Mathf.LerpAngle(startRotation.y, endRotation.y, (Time.time - headTurnStartTime) / smoothTime);
    }
}
