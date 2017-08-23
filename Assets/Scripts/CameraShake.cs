using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity = 1f;
    public float shakeDuration = 1f;
    public float shakeInterval = 1f;
    private float amount;
    private float length;

    public void CallShake (float amt)
    {
        amount = amt * shakeIntensity;
        length = amt * shakeDuration;
        InvokeRepeating("Shake", 0, shakeInterval);
        Invoke("StopShaking", length);
    }

    private void Shake ()
    {
        if (amount > 0)
        {
            float shakeAmt = Random.value * amount * 2 - amount;
            Vector3 pp = transform.position;
            pp.y += shakeAmt;
            transform.position = pp;
        }
    }

    private void StopShaking ()
    {
        CancelInvoke("Shake");
        transform.localPosition = Vector3.zero;
    }
}
