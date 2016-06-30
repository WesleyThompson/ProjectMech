using UnityEngine;
using System.Collections;

public class EnemyScan : MonoBehaviour {

    [Range(0, 45)]
    public float scanRange = 45;
    public float scanTime = 1f;
    private int scanDirection = -1;
    private Vector3 centerRotation;
    private Vector3 startRotation;
    private Vector3 endRotation;
    private float startTime;
    private Transform topTransform;

	void Awake ()
    {
        topTransform = transform.FindChild("Top");
        centerRotation = topTransform.localEulerAngles;
        print(centerRotation);
        topTransform.localEulerAngles = new Vector3(topTransform.localEulerAngles.x, centerRotation.y, topTransform.localEulerAngles.z);
        endRotation = startRotation = topTransform.localEulerAngles;
        startTime = Time.time;
	}
	
	void Update ()
    {
        if(RoughlyEqual(topTransform.localEulerAngles.y, endRotation.y, 0.001f))
        {
            if (RoughlyEqual(topTransform.localEulerAngles.y, centerRotation.y, 0.001f))
            {
                startRotation = topTransform.localEulerAngles;
                endRotation = new Vector3(topTransform.localEulerAngles.x, centerRotation.y + scanRange * scanDirection, topTransform.localEulerAngles.z);
            }
            else
            {
                scanDirection *= -1;
                startRotation = topTransform.localEulerAngles;
                endRotation = new Vector3(topTransform.localEulerAngles.x, centerRotation.y, topTransform.localEulerAngles.z);
            }
            startTime = Time.time;
        }
        float tRot = Mathf.LerpAngle(startRotation.y, endRotation.y, (Time.time - startTime) / scanTime);
        topTransform.localEulerAngles = new Vector3(topTransform.localEulerAngles.x, tRot, topTransform.localEulerAngles.z);
    }

    bool RoughlyEqual(float firstVal, float secondVal, float buffer)
    {
        while (firstVal >= 360)
        {
            firstVal -= 360;
        }
        while (firstVal < 0)
        {
            firstVal += 360;
        }

        while (secondVal >= 360)
        {
            secondVal -= 360;
        }
        while (secondVal < 0)
        {
            secondVal += 360;
        }
        return Mathf.Abs(firstVal - secondVal) < buffer;
    }
}
