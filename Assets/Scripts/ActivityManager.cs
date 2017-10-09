using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActivityManager {

    public static List<Activity> availableActivities = new List<Activity>();
    public static List<Activity> completedActivities = new List<Activity>();

    public static void StartActivity(string name)
    {
        Activity activity = availableActivities.Find(x => x.activityName.Contains(name));
        activity.active = true;
        activity.StartCoroutine(StartTimerActivity(activity));
    }

	public static void StopActivity(string name)
	{
		Activity activity = availableActivities.Find(x => x.activityName.Contains(name));
        activity.active = false;
	}

    private static void CompleteActivity(Activity activity)
    {
        completedActivities.Add(activity);
        availableActivities.Remove(activity);
        activity.SetCompletedActivity();
    }

	public static IEnumerator StartTimerActivity(Activity activity)
	{
        while (activity.timer > 1 && activity.active)
		{
			activity.timer -= Time.deltaTime * 1;
			yield return null;
		}
        CompleteActivity(activity);

	}

}
