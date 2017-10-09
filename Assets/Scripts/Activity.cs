using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

    public string activityName = "";
    public float duration = 0f;
    public float timer = 0f;
    public bool active = false;
    public bool enabledActivity = false;    
    public GameObject assignedPlayer = null;
    public Dictionary<GameObject, int>;

    private bool triggered = false;

    private void Awake()
    {
        timer = duration;
        ActivityManager.availableActivities.Add(this);
    }

    private void Update()
    {
        if(this.triggered && Input.GetKeyDown(KeyCode.P) && !this.active && assignedPlayer.GetComponent<PlayerControl>().active)
        {
            ActivityManager.StartActivity(this.activityName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && assignedPlayer == other.gameObject && enabledActivity && !triggered)
        {
            this.triggered = true;
            print(other.name + "Enter");
        }
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" && assignedPlayer == other.gameObject && enabledActivity && triggered)
		{
            this.triggered = false;
            print(other.name + "Exit");
            if(this.active)
            {
                ActivityManager.StopActivity(this.activityName);
            }
		}
	}

    public void SetCompletedActivity()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }


}
