using System;

[Serializable]
public class Resource {

    public ResourceManager.ResourceType type;
    public int quantity = 0;

    public Resource(ResourceManager.ResourceType pResourceType, int pQuantity)
    {
        
        this.type = pResourceType;
        this.quantity = pQuantity;

    }

}
