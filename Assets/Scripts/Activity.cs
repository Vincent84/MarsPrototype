using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

    // State
    public enum State
    {
        DISABLED,
        ENABLED,
        READY,
        ACTIVED,
        RUNNING,
        STOPPED,
        COMPLETED
    }

    // Attributes
    public string activityName = "";                // The activity's name
    public float duration = 0f;                     // The activity's duration
    public float timer = 0f;                        // The timer
    public GameObject assignedPlayer = null;        // The player assigned to activity
    public Resource[] resourcesNeeded;              // The resources needed for the activity
    public State currentState;
    public bool isCommon = false;                   // Check if the activity is common

    private bool isTriggered = false;               // Check if the activity is triggered

    private void Awake()
    {
        timer = duration;
        ActivityManager.availableActivities.Add(this);
    }

    private void Update()
    {
        if((currentState == State.READY || currentState == State.ACTIVED)  && Input.GetKeyDown(KeyCode.P))
        {
            ActivityManager.StartActivity(this.activityName);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (isCommon) assignedPlayer = other.gameObject;

        if (!assignedPlayer.GetComponent<PlayerControl>().active && isTriggered)
        {
            isTriggered = false;
        }

        switch (currentState)
        {

            case State.ENABLED:
                
                if (other.tag == "Player" && assignedPlayer == other.gameObject && assignedPlayer.GetComponent<PlayerControl>().active && !isTriggered)
                {
                    
                    if (ResourceManager.Instance.ChecksResourcesAvailibility(resourcesNeeded))
                    {
                        currentState = State.READY;
                    }
                    else
                    {
                        isTriggered = true;
                        print("Non si può attivare perche mancano le risorse necessarie!");
                    }

                }

                break;

            case State.READY:

                if (other.tag == "Player" && assignedPlayer == other.gameObject && !assignedPlayer.GetComponent<PlayerControl>().active)
                {
                    isTriggered = true;
                    currentState = State.ENABLED;
                }

                break;

            case State.STOPPED:

                if (other.tag == "Player" && assignedPlayer == other.gameObject && assignedPlayer.GetComponent<PlayerControl>().active && !isTriggered)
                {
                    isTriggered = true;
                    currentState = State.ACTIVED;
                }

                break;

        }
    }

    private void OnTriggerExit(Collider other)
	{

        switch (currentState)
        {
            case State.READY:

                if (other.tag == "Player" && assignedPlayer == other.gameObject)
                {
                    isTriggered = false;
                    currentState = State.ENABLED;

                }

                break;
            case State.RUNNING:

                if (other.tag == "Player" && assignedPlayer == other.gameObject)
                {
                    isTriggered = false;
                    currentState = State.STOPPED;
                    ActivityManager.StopActivity(this.activityName);

                }

                break;
        }
		
	}

    public void SetCompletedActivity()
    {
        currentState = State.COMPLETED;
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }


}
