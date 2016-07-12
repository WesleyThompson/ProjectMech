using UnityEngine;
using System.Collections;
using Common;

namespace Enemy
{
    public class EnemyShooting : GameBehavior
    {
        public bool shouldDebug = false;
        public float fireRate = 1;
        public float damage = 1;
        public float possibleBulletLocationRadius = 1;
        private float lastTimeShot;
        private bool canShoot;
        private GameObject target;
        private Vector3 hitBox;
        private Vector3 shotPos;
        private Vector3 shotDir;
        private float maxX = 3;
        private float maxY = 4;

        private float minX = 2;
        private float minY = 3;

        private float distanceScale = .2f;
        private float minDistance = 30;
        private float maxDistance = 100;
        private float startingSize = 10;
        private float endingSize = 20;
        private float sizeScale;
        private float distanceOffset = -10;

        private float distanceXProportionScale = .5f;
        private float distanceYProportionScale = 1;

        private float currX;
        private float currY;
        private float distanceToTarget;

        private float randomXShot;
        private float randomYShot;

        private float debugOffsetX;
        private float debugOffsetZ;
        private float offsetX;
        private float offsetZ;
        private Transform topTransform;
        void Awake()
        {
            topTransform = transform.FindChild("Top");
            lastTimeShot = Time.time - fireRate;
            debugOffsetX = maxX;
            debugOffsetZ = 0;
            currX = minX;
            currY = minY;
        }

        void Update()
        {
            if (target != null)
            {
                hitBox = target.transform.position;
                distanceToTarget = Vector3.Distance(topTransform.position, target.transform.position);
                print(distanceToTarget);
                if(distanceToTarget < minDistance)
                {
                    distanceToTarget = minDistance;
                } else if(distanceToTarget > maxDistance)
                {
                    distanceToTarget = maxDistance;
                }

                currX = GetProportionalDistance(distanceToTarget, distanceXProportionScale);
                currY = GetProportionalDistance(distanceToTarget, distanceYProportionScale);
                print(currX);
                randomXShot = Random.Range(-currX, currX);
                randomYShot = Random.Range(-currY, currY);

                debugOffsetX = currX * Mathf.Cos(topTransform.eulerAngles.y * Mathf.Deg2Rad);
                debugOffsetZ = currX * Mathf.Sin(topTransform.eulerAngles.y * Mathf.Deg2Rad);

                offsetX = randomXShot * Mathf.Cos(topTransform.eulerAngles.y * Mathf.Deg2Rad);
                offsetZ = -randomXShot * Mathf.Sin(topTransform.eulerAngles.y * Mathf.Deg2Rad);

                if (shouldDebug)
                {
                    //Top
                    Debug.DrawLine(new Vector3(hitBox.x - debugOffsetX, hitBox.y + currY, hitBox.z + debugOffsetZ), new Vector3(hitBox.x + debugOffsetX, hitBox.y + currY, hitBox.z - debugOffsetZ), Color.red);
                    //Right
                    Debug.DrawLine(new Vector3(hitBox.x + debugOffsetX, hitBox.y + currY, hitBox.z - debugOffsetZ), new Vector3(hitBox.x + debugOffsetX, hitBox.y - currY, hitBox.z - debugOffsetZ), Color.red);
                    //Bottom
                    Debug.DrawLine(new Vector3(hitBox.x + debugOffsetX, hitBox.y - currY, hitBox.z - debugOffsetZ), new Vector3(hitBox.x - debugOffsetX, hitBox.y - currY, hitBox.z + debugOffsetZ), Color.red);
                    //Left
                    Debug.DrawLine(new Vector3(hitBox.x - debugOffsetX, hitBox.y - currY, hitBox.z + debugOffsetZ), new Vector3(hitBox.x - debugOffsetX, hitBox.y + currY, hitBox.z + debugOffsetZ), Color.red);
                }

                //If there is a target, focused, and is fire rate is good
                if (target != null && canShoot && Time.time - lastTimeShot > fireRate)
                {
                    lastTimeShot = Time.time;
                    shotPos = new Vector3(hitBox.x + offsetX, hitBox.y + randomYShot, hitBox.z + offsetZ);
                    shotDir = topTransform.position - shotPos;
                    Debug.DrawRay(shotPos, shotDir, Color.cyan, 1);

                    //TODO: Call shoot function
                }
            }
        }

        float GetProportionalDistance(float distToTarg, float propScale)
        {
            distToTarg += distanceOffset;
            distToTarg = distanceScale * distToTarg * propScale;
            return distToTarg;
        }

        void OnDrawGizmosSelected()
        {
            if (shouldDebug)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(shotPos, new Vector3(0.5f, 0.5f, 0.5f));
            }
        }

        public void SetShoot(bool shoot, GameObject obj)
        {
            canShoot = shoot;
            target = obj;
        }
    }
}
