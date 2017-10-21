using UnityEngine;
using UnityEngine.UI;

public class BarracksPanelUI : MonoBehaviour {

    public MonsterType[] monsterTypes;
    public Transform ContentParent;
    public GameObject Content;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < monsterTypes.Length; i++)
        {
            GameObject newElement =  Instantiate(Content, ContentParent) as GameObject;
            //when added
            //newElement.transform.GetChild(0).GetComponent<Image>().sprite = monsterTypes[i].UISprite;
            newElement.transform.GetChild(1).GetComponent<Text>().text = monsterTypes[i].monsterName;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
