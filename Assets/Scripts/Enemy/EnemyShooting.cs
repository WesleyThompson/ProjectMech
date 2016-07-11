using UnityEngine;
using System.Collections;
using Common;

namespace Enemy
{
    public class EnemyShooting : GameBehavior
    {
        public float fireRate = 1;
        public float damage = 1;
        public float possibleBulletLocationRadius = 1;
        private float lastTimeShot;
        private bool canShoot;
        private GameObject target;
        private Vector3 targetDir;
        private float maxX = 100;
        private float maxY = 100;

        void Awake()
        {
            lastTimeShot = Time.time - fireRate;

        }

        void Update()
        {
            if (target != null && canShoot && Time.time - lastTimeShot > fireRate)
            {
                lastTimeShot = Time.time;
                float xRand = Random.Range(-maxX, maxX);
                float yRand = Random.Range(-maxY, maxY);
                Vector3 centerOfCircle = targetDir;

                print(targetDir);
                Debug.DrawLine(new Vector3(centerOfCircle.x-maxY, centerOfCircle.y + maxY, centerOfCircle.z), new Vector3(centerOfCircle.x+maxX, centerOfCircle.y + maxY, centerOfCircle.z), Color.red);


                //TODO: Player Take damage
            }
        }

        public void SetShoot(bool shoot, GameObject obj, Vector3 direction)
        {
            canShoot = shoot;
            target = obj;
            targetDir = direction;
        }
    }
}
