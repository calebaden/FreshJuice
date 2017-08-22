using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public bool isJuicy;

    // HUD variables
    public Slider healthSlider;
    public Text timeText;
    public Text speedText;

    // Panel UI objects
    public GameObject startPanel;
    public GameObject finishPanel;
    public Text levelTimeText;
    public Text healthPentalty;
    public Text totalTimeText;

    // Level variables
    private bool isPlaying;
    public float levelTime;

    private void Awake()
    {
        // Setup singleton/instance
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Use this for initialization
    private void Start ()
    {
        PlayerScript.Instance.enabled = false;
	}
	
	// Update is called once per frame
	private void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
            JuiceSwitch();

        if (Input.GetButtonDown("Submit"))
            StartLevel();

        if (isPlaying)
            levelTime += Time.deltaTime;

        timeText.text = levelTime.ToString("F2");
        healthSlider.value = PlayerScript.Instance.GetHealth01();
        speedText.text = PlayerScript.Instance.GetVelocityMagnitude().ToString("F2");
	}

    // Function that activates / deactivates the juice elements
    private void JuiceSwitch ()
    {
        isJuicy = !isJuicy;
        PlayerScript.Instance.CallJuice(isJuicy);
        Camera.main.GetComponent<CameraScript>().CallJuice(isJuicy);
    }

    // Function called when completing the level
    public void Success ()
    {
        isPlaying = false;
        float hp = PlayerScript.Instance.GetHealth01();
        finishPanel.SetActive(true);
        levelTimeText.text = "Level Time: = " + levelTime.ToString("F2") + " seconds";
        healthPentalty.text = "Health Penalty: = " + (hp * 10).ToString("F2") + " seconds";
        totalTimeText.text = "Total Time: = " + ((hp * 10) + levelTime).ToString("F2") + " seconds";
    }

    // Function called when player dies
    public void Fail ()
    {
        Debug.Log("Fail");
    }

    private void StartLevel ()
    {
        PlayerScript.Instance.enabled = true;
        startPanel.SetActive(false);
        isPlaying = true;
    }
}
