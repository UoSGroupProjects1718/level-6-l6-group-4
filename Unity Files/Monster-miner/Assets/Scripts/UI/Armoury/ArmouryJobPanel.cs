using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class ArmouryJobPanel : MonoBehaviour {

    public Transform contentParent;
    public GameObject content;
    [SerializeField]
    private GameObject poolParent;
    private List<GameObject> picturePool;
    [SerializeField]
    private int poolSize = 40;
	

	void Awake () {
       
        picturePool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
           GameObject panel =  Instantiate(content,poolParent.transform) as GameObject;
            picturePool.Add(panel);
            panel.SetActive(false);
        }
       
	}
    private GameObject GetColonistPicture()
    {
        for (int i = 0; i < picturePool.Count; i++)
        {
            if (!picturePool[i].activeSelf)
            {
                picturePool[i].SetActive(true);
                picturePool[i].transform.SetParent(contentParent);
                return picturePool[i];
            }
        }
        return null;
    }
    public void OnPanelOpen()
    {
        for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
        {
            GameObject picture = GetColonistPicture();
            if (picture != null)
            {
                //set names etc 
            }
        }

    }
    //on disable reset the pictures
    public void OnDisable()
    {
       while(contentParent.transform.childCount > 0)
        {
            if (poolParent != null)
            {
                contentParent.transform.GetChild(0).gameObject.SetActive(false);
                contentParent.transform.GetChild(0).SetParent(poolParent.transform);
            }
            else
            {
                break;
            }
        }
    }



}
