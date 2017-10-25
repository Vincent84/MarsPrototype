using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager {

    public enum ResourceType
    {
        FOOD = 0,
        WATER = 1,
        OXYGEN = 2,
        ELECTRICITY = 3,
        CIRUITRY = 4,
        BUILDING = 5,
        BOTANICAL = 6,
        GEOLOGICAL = 7,
        MEDICAL = 8,
        ROBOTIC = 9
    }

    private Resource[] resourcesAvailable = new Resource[Enum.GetNames(typeof(ResourceType)).Length];

    public Resource[] ResourcesAvailable
    {
        get
        {
            return resourcesAvailable;
        }
    }

    private ResourceManager()
    {
        for (int i = 0; i < resourcesAvailable.Length; i++)
        {
            Resource newResource = new Resource((ResourceType)i, 20);
            resourcesAvailable[i] = newResource;
        }
    }

    public static ResourceManager Instance { get { return Nested.instance; } }

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly ResourceManager instance = new ResourceManager();
    }

    
    /*public ResourceManager()
    {
        resourcesAvailable = new Resource[Enum.GetNames(typeof(ResourceType)).Length];
        for (int i = 0; i < resourcesAvailable.Length; i++)
        {
            Resource newResource = new Resource( (ResourceType)i, 0);
            resourcesAvailable[i] = newResource;
        }
    }*/

    /// <summary>
    /// Decreaseses the resources.
    /// </summary>
    /// <param name="resources">Resources.</param>
    public void DecreasesResources(Resource[] resources)
    {

        // Downgrades the quantity
        foreach(Resource resource in resources)
        {
            resourcesAvailable[(int)resource.type].quantity -= resource.quantity;
        }


    }

    public bool ChecksResourcesAvailibility(Resource[] resources)
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

    /*public static Dictionary<string, int> resourcesAvailable = new Dictionary<string, int>(); // Dictionary of available resources

    /// <summary>
    /// Increaseses the resources when the player collects it.
    /// </summary>
    /// <param name="resource">The object Resource.</param>
    public static void IncreasesResources(Resource resource)
    {

        // Checks if the resource is already in the dictionary
        if(!resourcesAvailable.ContainsKey(resource.resourceName))
        {
            // If the resource isn't already in the dictionary, adds it
            resourcesAvailable[resource.resourceName] = resource.quantity;
        }
        else
        {
            // If the resource is already in the dictionary, upgrades the quantity
            resourcesAvailable[resource.resourceName] += resource.quantity;
        }

    }

    /// <summary>
    /// Increaseses the resources at the end of an activity.
    /// </summary>
    /// <param name="resourceName">Resource name.</param>
    /// <param name="resourceQuantity">Resource quantity.</param>
    public static void IncreasesResources(string resourceName, int resourceQuantity)
    {
        // Checks if the resource is already in the dictionary
        if (!resourcesAvailable.ContainsKey(resourceName))
        {
            // If the resource isn't already in the dictionary, adds it
            resourcesAvailable[resourceName] = resourceQuantity;
        }
        else
        {
            // If the resource is already in the dictionary, upgrades the quantity
            resourcesAvailable[resourceName] += resourceQuantity;
        }

    }

    /// <summary>
    /// Decreaseses the resources.
    /// </summary>
    /// <param name="resourceName">Resource name.</param>
    /// <param name="resourceQuantity">Resource quantity.</param>
    public static void DecreasesResources(string resourceName, int resourceQuantity)
    {

        // Downgrades the quantity
        resourcesAvailable[resourceName] -= resourceQuantity;

    }

    public static bool ChecksResourcesAvailibility(string resourceName, int resourceQuantity)
    {
        // Checks if the resource is already in the dictionary
        if (!resourcesAvailable.ContainsKey(resourceName))
        {
            // If the resource isn't in the dictionary, return false
            return false;
        }
        else
        {
            int quantity = resourcesAvailable[resourceName];

            // If the resource is in the dictionary and there is enough quantity of it, return true
            if (quantity >= resourceQuantity) return true;

            // If the resource is in the dictionary and there isn't enough quantity of it, return false
            return false;
        }
    }*/

    public void Print()
    {
        /*foreach(KeyValuePair<string, int> resource in resourcesAvailable)
        {
            Debug.Log(resource.Key + ": " + resource.Value);
        }*/

        foreach(Resource resource in resourcesAvailable)
        {
            Debug.Log(resource.type + ": " + resource.quantity);
        }
    }

}
