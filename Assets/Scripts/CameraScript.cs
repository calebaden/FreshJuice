using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float moveSpeed;
    public float juiceSpeed = 1f;
    public float jankSpeed = 2f;
    public float camDist = -10;
    public float dirOffset = 1f;

	// Use this for initialization
	void Start ()
    {
        CallJuice(GameController.Instance.isJuicy);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (PlayerScript.Instance.gameObject != null)
            FollowPlayer();
	}

    // Function that follows the player
    private void FollowPlayer ()
    {
        Vector2 target = PlayerScript.Instance.transform.position;
        if (GameController.Instance.isJuicy)
        {
            target += PlayerScript.Instance.GetVelocity() * dirOffset;
        }
        Vector3 newPos = new Vector3(target.x, target.y, camDist);
        transform.position = Vector3.Slerp(transform.position, newPos, moveSpeed * Time.deltaTime);
    }

    public void CallJuice (bool isJuice)
    {
        if (isJuice)
            moveSpeed = juiceSpeed;
        else
            moveSpeed = jankSpeed;
    }
}
