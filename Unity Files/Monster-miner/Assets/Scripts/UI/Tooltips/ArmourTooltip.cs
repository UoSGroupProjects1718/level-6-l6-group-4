using TMPro;


public class ArmourTooltip : ItemTooltip {

    protected override void UpdateTooltip()
    {
        UIPanels.Instance.tooltipPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = referenceItem.itemName;
        //Description
        SetElementText(1, referenceItem.itemDescription);
        //slot
        SetElementText(2, "Armour slot: " + (referenceItem as Armour).armourSlot.ToString());
        //walk speed mod
        string walkSpeedOperator = ((referenceItem as Wearable).walkSpeedModifier >= 0) ? "+" : "-";
        SetElementText(3, "Walk speed: " +walkSpeedOperator+ (referenceItem as Wearable).walkSpeedModifier.ToString());
        //work speed mod
        string workSpeedOperator = ((referenceItem as Wearable).workSpeedModifier >= 0) ? "+": "-";
        SetElementText(4, "Work speed: " + workSpeedOperator +  (referenceItem as Wearable).workSpeedModifier.ToString());
        //DR
        SetElementText(5, "Damage Reduction: " + (referenceItem as Armour).DamageReduction.ToString());
    }


}
