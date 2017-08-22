using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject player;

    private float moveSpeed;
    public float juiceSpeed = 1f;
    public float jankSpeed = 2f;
    public float camDist = -10;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CallJuice(GameController.Instance.isJuicy);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (player != null)
            FollowPlayer();
	}

    // Function that follows the player
    private void FollowPlayer ()
    {
        Vector3 target = player.transform.position;
        target.z = camDist;
        transform.position = Vector3.Slerp(transform.position, target, moveSpeed * Time.deltaTime);
    }

    public void CallJuice (bool isJuice)
    {
        if (isJuice)
            moveSpeed = juiceSpeed;
        else
            moveSpeed = jankSpeed;
    }
}
