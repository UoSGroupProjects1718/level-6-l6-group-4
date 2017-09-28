using UnityEngine;

public enum RawMaterialType
{
    Wood,
    Stone,
}

public class RawMaterial : Item {

    public int maxStackSize;
    [HideInInspector]
    public int currentStackSize;

    public RawMaterialType materialType;
	
}
