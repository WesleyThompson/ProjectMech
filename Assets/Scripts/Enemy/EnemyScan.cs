using UnityEngine;
using System.Collections;
using Common;

namespace Enemy
{
    public class EnemyScan : GameBehavior
    {
        [Range(0, 90)]
        public float scanRange = 45;
        public float scanTime = 1f;
        private int scanDirection = -1;
        private Vector3 centerRotation;
        private Vector3 startRotation;
        private Vector3 endRotation;
        private float startTime;
        private Transform topTransform;
        private bool shouldScan;

        void Awake()
        {
            topTransform = transform.FindChild("Top");
            centerRotation = topTransform.localEulerAngles;
            topTransform.localEulerAngles = new Vector3(topTransform.localEulerAngles.x, centerRotation.y, topTransform.localEulerAngles.z);
            endRotation = startRotation = topTransform.localEulerAngles;
            startTime = Time.time;
            StartScanning();
        }

        void Update()
        {
            //Should scan when enemy cannot see the target
            if (shouldScan)
            {
                //If the angle is at the end of the cycle it will reverse
                if (AngleRoughlyEqual(topTransform.localEulerAngles.y, endRotation.y, 0.001f))
                {
                    //If it is at the center it determines which way to go
                    if (AngleRoughlyEqual(topTransform.localEulerAngles.y, centerRotation.y, 0.001f))
                    {
                        startRotation = topTransform.localEulerAngles;
                        endRotation = new Vector3(topTransform.localEulerAngles.x, centerRotation.y + scanRange * scanDirection, topTransform.localEulerAngles.z);
                    }
                    //If it is at the ends it changes direction
                    else
                    {
                        scanDirection *= -1;
                        startRotation = topTransform.localEulerAngles;
                        endRotation = new Vector3(topTransform.localEulerAngles.x, centerRotation.y, topTransform.localEulerAngles.z);
                    }
                    startTime = Time.time;
                }
                //Always lerping back and forth
                float tRot = Mathf.LerpAngle(startRotation.y, endRotation.y, (Time.time - startTime) / scanTime);
                topTransform.localEulerAngles = new Vector3(topTransform.localEulerAngles.x, tRot, topTransform.localEulerAngles.z);
            }
        }

        public void StopScanning()
        {
            shouldScan = false;
        }

        public void StartScanning()
        {
            shouldScan = true;
        }

        public bool IsScanning()
        {
            return shouldScan;
        }
    }
}
