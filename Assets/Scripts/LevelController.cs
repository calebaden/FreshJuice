using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Material wallMat;
    public Color baseCol;
    public Color hitCol;
    public float flashDur = 1f;

	// Use this for initialization
	void Start ()
    {
        GameController.Instance.currentLevel = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Flash ()
    {
        StartCoroutine(FlashCR());
    }

    IEnumerator FlashCR ()
    {
        wallMat.color = hitCol;
        yield return new WaitForSeconds(flashDur);
        wallMat.color = baseCol;
    }
}
