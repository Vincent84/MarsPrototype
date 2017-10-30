using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActivityManager {

    public static List<Activity> availableActivities = new List<Activity>();
    public static List<Activity> completedActivities = new List<Activity>();

    public static void StartActivity(string name)
    {
        Activity activity = availableActivities.Find(x => x.activityName.Contains(name));

        if(activity.currentState == Activity.State.READY) ResourceManager.DecreasesResources(activity.resourcesNeeded);
           
        activity.currentState = Activity.State.RUNNING;
        
        activity.StartCoroutine(StartTimerActivity(activity));
    }

	public static void StopActivity(string name)
	{
		Activity activity = availableActivities.Find(x => x.activityName.Contains(name));
	}

    private static void CompleteActivity(Activity activity)
    {   
        completedActivities.Add(activity);
        availableActivities.Remove(activity);
        activity.SetCompletedActivity();
    }

	public static IEnumerator StartTimerActivity(Activity activity)
	{
        while (activity.timer > 1 && activity.currentState == Activity.State.RUNNING)
		{
			activity.timer -= Time.deltaTime * 1;
			yield return null;
		}
        if(activity.timer < 1) 
        {
			CompleteActivity(activity);
		}

	}

}
