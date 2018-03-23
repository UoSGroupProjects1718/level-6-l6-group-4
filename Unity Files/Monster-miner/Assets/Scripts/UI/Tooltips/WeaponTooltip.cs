using TMPro;

public class WeaponTooltip : ItemTooltip {

    protected override void UpdateTooltip()
    {
        UIPanels.Instance.tooltipPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = referenceItem.itemName;
        //description
        SetElementText(1, referenceItem.itemDescription);
        //walk speed modifier
        string walkSpeedOperator = ((referenceItem as Wearable).walkSpeedModifier >= 0) ? "+" : "-";
        SetElementText(2, "Walk speed: "+ walkSpeedOperator + (referenceItem as Weapon).walkSpeedModifier.ToString());
        //work speed modifier
        string workSpeedOperator = ((referenceItem as Wearable).workSpeedModifier >= 0) ? "+" : "-";
        SetElementText(3,"Work speed: " +workSpeedOperator+ (referenceItem as Weapon).workSpeedModifier.ToString());
        //damage
        SetElementText(4, "Damage: " + (referenceItem as Weapon).Damage.ToString());
        //range
        SetElementText(5, "Range: " + (referenceItem as Weapon).Range.ToString());
        //attack speed
        SetElementText(6, "Attack speed: " + (referenceItem as Weapon).AttackSpeed.ToString());
        //accuracy
        SetElementText(7, "Accuracy: " + (referenceItem as Weapon).Accuracy.ToString());
    }

}
