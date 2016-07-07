using UnityEngine;
using System.Collections;
using Common;

namespace Enemy
{
    public class EnemyController : GameBehavior
    {
        private NavMeshAgent agent;
        public GameObject moveTarget;
        private float width;
        private float height;
        private EnemyScan enemyScanScript;
        private EnemyShooting enemyShootingScript;

        private float headTurnStartTime;
        private Transform topTransform;
        private bool keepShooting;
        private bool topIsFocused;
        private bool firstTimeFocused;
        private float startRotation;
        private float endRotation;
        private float startTime;
        private float scanTime;
        private float lastSeenBuffer = 2.5f;

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
        float lastTimeSeenPlayer;
        float focusProgress;
        float focusSpeed = 50;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();//transform.FindChild("NavMesh").
            enemyScanScript = GetComponent<EnemyScan>();
            enemyShootingScript = GetComponent<EnemyShooting>();
            width = 6.5f;
            height = 5;
            topTransform = transform.FindChild("Top");
            keepShooting = false;
            topIsFocused = false;
            firstTimeFocused = true;
            lastTimeSeenPlayer = Time.time - lastSeenBuffer;
        }
        
        void Update()
        {
            UpdateRaycast();

            //If can see entire target or target hasn't broken sight
            if (didHitL && didHitR && lHit.transform.parent.name == "PlayerTemp" && rHit.transform.parent.name == "PlayerTemp" || keepShooting)
            {
                //If can see player stop scanning and this
                enemyScanScript.StopScanning();
                agent.SetDestination(transform.position);

                //If target moves and are reacquired in less that last buffer then aim immediately
                if (Time.time - lastTimeSeenPlayer < lastSeenBuffer)
                {
                    topIsFocused = true;
                }

                //If the top is lined up with the target
                if (topIsFocused)
                {
                    topTransform.LookAt(moveTarget.transform);
                    enemyShootingScript.SetShoot(true, moveTarget);
                    lastTimeSeenPlayer = Time.time;
                }
                //Play animation to aim on target
                else
                {
                    //First time this is trying to focus on target
                    if (firstTimeFocused)
                    {
                        startRotation = topTransform.localEulerAngles.y;
                        topToPlayerLocal = transform.InverseTransformDirection(enemyTopToPlayer);
                        endRotation = Mathf.Atan(topToPlayerLocal.x / topToPlayerLocal.z) * Mathf.Rad2Deg;

                        startTime = Time.time;
                        firstTimeFocused = false;
                        focusProgress = 0;
                    }
                    //Runs smoothing
                    else
                    {
                        float simpleStartRot = GetSimplifiedAngle(startRotation);
                        float simpleEndRot = GetSimplifiedAngle(endRotation);

                        //Fixes problem when comparing for example 2 and 355 by making distance 7 instead of 333
                        if(simpleStartRot >= 180)
                        {
                            simpleStartRot = 360 - simpleStartRot;
                        }
                        if (simpleEndRot >= 180)
                        {
                            simpleEndRot = 360 - simpleEndRot;
                        }

                        //Keeps same speed as the scanner
                        float dist = Mathf.Abs(simpleStartRot - endRotation);
                        scanTime = dist * enemyScanScript.GetTimePerScanDegree();
                        float tRot = Mathf.LerpAngle(startRotation, endRotation, (Time.time - startTime)/scanTime);
                        topTransform.localEulerAngles = new Vector3(topTransform.localEulerAngles.x, tRot, topTransform.localEulerAngles.z);
                        if (AngleRoughlyEqual(tRot, endRotation, 0.01f))
                        {
                            topIsFocused = true;
                        }
                    }
                }
                
                //After the target is completely visible then hides, but is still visible, this keeps the focus on the target
                if (didHitC && cHit.transform.parent.name != "PlayerTemp")
                {
                    keepShooting = false;
                }
                else
                {
                    keepShooting = true;
                }
            }
            //If the target is not visible
            else
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
            //Define direction from this to target
            enemyDirectionL = enemyPositionL - moveTarget.transform.position;
            enemyDirectionR = enemyPositionR - moveTarget.transform.position;
            enemyDirectionC = topTransform.rotation * Vector3.forward * 20;
            enemyTopToPlayer = new Vector3(topTransform.position.x, topTransform.position.y, topTransform.position.z) - moveTarget.transform.position;
            enemyToPlayer = enemyPositionC - moveTarget.transform.position;

            //Define offset of raycast so that they come off tangent from this to the target
            enemyToPlayerAngle = Mathf.Atan2(enemyToPlayer.x, enemyToPlayer.z);
            xOffset = width * Mathf.Cos(enemyToPlayerAngle);
            zOffset = width * Mathf.Sin(enemyToPlayerAngle);

            //Define left and right position
            enemyPositionL = new Vector3(transform.position.x - xOffset, transform.position.y + height, transform.position.z + zOffset);
            enemyPositionR = new Vector3(transform.position.x + xOffset, transform.position.y + height, transform.position.z - zOffset);
            enemyPositionC = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);

            //Makes sure direction is going the correct way
            enemyDirectionL *= -1;
            enemyDirectionR *= -1;
            enemyToPlayer *= -1;
            enemyTopToPlayer *= -1;

            //Debug
            Debug.DrawRay(enemyPositionL, enemyDirectionL, Color.blue);
            Debug.DrawRay(enemyPositionR, enemyDirectionR, Color.blue);
            Debug.DrawRay(enemyPositionC, enemyDirectionC, Color.red);
            Debug.DrawRay(enemyPositionC, enemyToPlayer, Color.green);

            //Defines if raycast hit anything and what it hit
            didHitL = Physics.Raycast(enemyPositionL, enemyDirectionL, out lHit);
            didHitR = Physics.Raycast(enemyPositionR, enemyDirectionR, out rHit);
            didHitC = Physics.Raycast(enemyPositionC, enemyDirectionC, out cHit);
        }
    }
}
