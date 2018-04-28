using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BarracksPanelUI : MonoBehaviour {

    //public MonsterType[] monsterTypes;
    public Transform ContentParent;
    public GameObject Content;

	// Use this for initialization
	void Start () {

        string[] keys = MonsterTypes.Instance.Mons.Keys.ToArray();
		for(int i = 0; i < keys.Length; i++)
        {
            GameObject newElement =  Instantiate(Content, ContentParent) as GameObject;
            newElement.transform.GetComponent<BarracksJobPanel>().monster = MonsterTypes.Instance.Mons[keys[i]];
            //when added
            newElement.transform.GetChild(0).GetComponent<Image>().sprite =  MonsterTypes.Instance.Mons[keys[i]].monsterSprite;
            newElement.transform.GetChild(1).GetComponent<Text>().text = MonsterTypes.Instance.Mons[keys[i]].monsterName;
            
           
        }
	}

}
