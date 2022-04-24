using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxTorque;
    public float maxSteerAngle;

    public Joystick JoyStickRight;
    public Joystick JoyStickLeft;

    public void WheelsTransform(WheelCollider wCollider)
    {
        if (wCollider.transform.childCount == 0)
            return;

        Transform wheel = wCollider.transform.GetChild(0);

        Vector3 pos;
        Quaternion rot;
        wCollider.GetWorldPose(out pos, out rot);

        wheel.transform.position = pos;
        wheel.transform.rotation = rot;
    }

    private void FixedUpdate()
    {
        //float motor = maxTorque * Input.GetAxis("Vertical");
        //float steer = maxSteerAngle * Input.GetAxis("Horizontal");
        float motor = maxTorque * JoyStickLeft.Vertical;
        float steer = maxSteerAngle * JoyStickRight.Horizontal;

        foreach(AxleInfo axleInfo in axleInfos)
        {
            if(axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steer;
                axleInfo.rightWheel.steerAngle = steer;
            }

            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            WheelsTransform(axleInfo.leftWheel);
            WheelsTransform(axleInfo.rightWheel);
        }
    }

}
