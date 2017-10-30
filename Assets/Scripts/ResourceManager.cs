using System;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager {

    public enum ResourceType
    {
        FOOD = 0,
        WATER = 1,
        OXYGEN = 2,
        ELECTRICITY = 3,
        RESEARCH = 4
        /*CIRUITRY = 4,
        BUILDING = 5,
        BOTANICAL = 6,
        GEOLOGICAL = 7,
        MEDICAL = 8,
        ROBOTIC = 9*/
    }

    private static Resource[] resourcesAvailable = new Resource[Enum.GetNames(typeof(ResourceType)).Length];

    public static Resource[] ResourcesAvailable
    {
        get { return resourcesAvailable; }
    }

    /// <summary>
    /// Increases the resources.
    /// </summary>
    /// <param name="resources">Resources.</param>
    public static void IncreasesResources(Resource[] resources)
    {

        // Downgrades the quantity
        foreach (Resource resource in resources)
        {
            resourcesAvailable[(int)resource.type].quantity += resource.quantity;
            resourcesAvailable[(int)resource.type].uiText.GetComponent<Text>().text =
                resourcesAvailable[(int)resource.type].type.ToString() + ": " + resourcesAvailable[(int)resource.type].quantity;
        }


    }

    /// <summary>
    /// Decreaseses the resources.
    /// </summary>
    /// <param name="resources">Resources.</param>
    public static void DecreasesResources(Resource[] resources)
    {

        // Downgrades the quantity
        foreach(Resource resource in resources)
        {
            resourcesAvailable[(int)resource.type].quantity -= resource.quantity;
            resourcesAvailable[(int)resource.type].uiText.GetComponent<Text>().text = 
                resourcesAvailable[(int)resource.type].type.ToString() + ": " + resourcesAvailable[(int)resource.type].quantity;
        }


    }

    public static bool ChecksResourcesAvailibility(Resource[] resources)
    {

        foreach(Resource resource in resources)
        {
            if(resourcesAvailable[(int)resource.type].quantity < resource.quantity)
            {
                return false;
            }
        }

        return true;

    }

    /*public void Print()
    {

        foreach(Resource resource in resourcesAvailable)
        {
            Debug.Log(resource.type + ": " + resource.quantity);
        }
    }*/

}
