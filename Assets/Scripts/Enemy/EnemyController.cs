using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject moveTarget;
    private float width;
    private float height;
    private EnemyScan enemyScanScript;
    private EnemyShooting enemyShootingScript;
    
    private float headTurnStartTime;
    private float smoothTime = 0.2f;
    private Transform topTransform;
    private bool keepShooting;
    private bool topIsFocused;
    private bool firstTimeFocused;
    private float startRotation;
    private float endRotation;
    private float startTime;
    private float scanTime = 1;
    

    RaycastHit lHit;
    RaycastHit rHit;
    RaycastHit cHit;

    Vector3 enemyToPlayer;
    float enemyToPlayerAngle;
    float xOffset;
    float zOffset;

    Vector3 enemyPositionL;
    Vector3 enemyPositionR;
    Vector3 enemyPositionC;

    Vector3 enemyDirectionL;
    Vector3 enemyDirectionR;
    Vector3 enemyDirectionC;

    bool didHitL;
    bool didHitR;
    bool didHitC;
    Vector3 topToPlayerLocal;
    Vector3 enemyTopToPlayer;

    void Start () {
        agent = GetComponent<NavMeshAgent>();//transform.FindChild("NavMesh").
        enemyScanScript = GetComponent<EnemyScan>();
        enemyShootingScript = GetComponent<EnemyShooting>();
        width = 6f;
        height = 5;
        topTransform = transform.FindChild("Top"); 
        keepShooting = false;
        topIsFocused = false;
        firstTimeFocused = true;
        // agent.updateRotation = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateRaycast();

        //If can see entire target or target hasn't broken sight
        if (didHitL && didHitR && lHit.transform.parent.name == "PlayerTemp" && rHit.transform.parent.name == "PlayerTemp" || keepShooting)
        {
            enemyScanScript.StopScanning();
            agent.SetDestination(transform.position);

            if(topIsFocused)
            {
                topTransform.LookAt(moveTarget.transform);
                enemyShootingScript.SetShoot(true, moveTarget);
            } else
            {
                if(firstTimeFocused)
                {
                    startRotation = topTransform.localEulerAngles.y;
                    Vector3 topDir = new Vector3((topTransform.position - moveTarget.transform.position).x, 0, (topTransform.position - moveTarget.transform.position).z);
                    topToPlayerLocal = transform.InverseTransformDirection(enemyTopToPlayer);
                    endRotation = Mathf.Atan(topToPlayerLocal.x/topToPlayerLocal.z) * Mathf.Rad2Deg;

                    startTime = Time.time;
                    firstTimeFocused = false;
                } else
                {
                    float tRot = Mathf.LerpAngle(startRotation, endRotation, (Time.time - startTime) / scanTime);
                    topTransform.localEulerAngles = new Vector3(topTransform.localEulerAngles.x, tRot, topTransform.localEulerAngles.z);
                    if(tRot == endRotation)
                    {
                        topIsFocused = true;
                    }
                }
            }

            if (didHitC && cHit.transform.parent.name != "PlayerTemp")
            {
                keepShooting = false;
            } else
            {
                keepShooting = true;
            }
        } else
        {
            agent.SetDestination(moveTarget.transform.position);
            enemyScanScript.StartScanning();
            enemyShootingScript.SetShoot(false, null);
            firstTimeFocused = true;
            topIsFocused = false;
        }
    }

    void UpdateRaycast()
    {
        enemyPositionC = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        enemyToPlayer = enemyPositionC - moveTarget.transform.position;
        enemyTopToPlayer = new Vector3(topTransform.position.x, topTransform.position.y, topTransform.position.z) - moveTarget.transform.position;

        enemyToPlayerAngle = Mathf.Atan2(enemyToPlayer.x, enemyToPlayer.z);
        xOffset = width * Mathf.Cos(enemyToPlayerAngle);
        zOffset = width * Mathf.Sin(enemyToPlayerAngle);

        enemyPositionL = new Vector3(transform.position.x - xOffset, transform.position.y + height, transform.position.z + zOffset);
        enemyPositionR = new Vector3(transform.position.x + xOffset, transform.position.y + height, transform.position.z - zOffset);

        enemyDirectionL = enemyPositionL - moveTarget.transform.position;
        enemyDirectionR = enemyPositionR - moveTarget.transform.position;
        enemyDirectionC = topTransform.rotation * Vector3.forward * 20;

        enemyDirectionL *= -1;
        enemyDirectionR *= -1;
        enemyToPlayer *= -1;
        enemyTopToPlayer *= -1;

        Debug.DrawRay(enemyPositionL, enemyDirectionL, Color.blue);
        Debug.DrawRay(enemyPositionR, enemyDirectionR, Color.blue);
        Debug.DrawRay(enemyPositionC, enemyDirectionC, Color.red);
        //Debug.DrawRay(enemyPositionC, enemyToPlayer, Color.green);
        Debug.DrawRay(topTransform.position, topToPlayerLocal, Color.gray);
        Debug.DrawRay(topTransform.position, enemyTopToPlayer, Color.gray);

        didHitL = Physics.Raycast(enemyPositionL, enemyDirectionL, out lHit);
        didHitR = Physics.Raycast(enemyPositionR, enemyDirectionR, out rHit);
        didHitC = Physics.Raycast(enemyPositionC, enemyDirectionC, out cHit);
    }
}
