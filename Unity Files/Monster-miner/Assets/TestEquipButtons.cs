using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEquipButtons : MonoBehaviour {

    public Armour head;
    public Armour torso;
    public Armour legs;

    public ColonistController colonist;

    public void HeadClick()
    {
        colonist.EquipWearable(head);
    }
    public void TorsoClick()
    {
        colonist.EquipWearable(torso);
    }
    public void LegsClick()
    {
        colonist.EquipWearable(legs);
    }
}
