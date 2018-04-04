//Oliver

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//http://hyunkell.com/blog/rts-style-unit-selection-in-unity-5/

public class UnitSelection : MonoBehaviour {

    bool isSelecting = false;
    Vector3 mousePosition1;

    public static List<ColonistController> SelectedColonists = new List<ColonistController>();
    public static List<MonsterController> SelectedMonsters = new List<MonsterController>();
    private void Start()
    {
        for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
        {
            BehaviourTreeManager.Colonists[i].selectionCircle.enabled = false;
        }
        for (int i = 0; i < BehaviourTreeManager.Monsters.Count; i++)
        {
            BehaviourTreeManager.Monsters[i].SelectionCircle.enabled = false;
        }
    }

    private void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            isSelecting = false;
            return;
        }

        if (Input.GetKeyDown(Keybinds.Instance.PrimaryActionKey))
        {
 
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
            SelectedColonists.Clear();
            SelectedMonsters.Clear();

            //send out a ray
            RaycastHit rayhit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayhit))
            {
                //and if they hit something
                if (rayhit.collider != null && rayhit.collider.tag != "Building")
                {
                    //check to see if there is a colonist controller attached to the object
                    ColonistController colonist = rayhit.collider.GetComponent<ColonistController>();
                    //if there is
                    if (colonist != null)
                    {
                        //add it to the list
                        SelectedColonists.Add(colonist);
                        colonist.selected = true;
                        colonist.selectionCircle.enabled = true;
                        // and do the things that mouse up would normally do in order for the selection to seem responsive
                        isSelecting = false;
                    }
                    else
                    {
                        MonsterController monster = rayhit.collider.transform.parent.GetComponent<MonsterController>();
                        if(monster != null)
                        {
                            SelectedMonsters.Add(monster);
                            monster.selected = true;
                            monster.SelectionCircle.enabled = true;
                            isSelecting = false;
                        }
                    }
                }
            }
            CheckButtonActivate();
        }

        if (Input.GetKeyUp(Keybinds.Instance.PrimaryActionKey))
        {
            isSelecting = false;

            CheckButtonActivate();
        }
        //check both lists, if the unit is within the bounds, select them.
        if (isSelecting)
        {
            for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
            {
                if (IsWithinBounds(BehaviourTreeManager.Colonists[i].gameObject))
                {
                    BehaviourTreeManager.Colonists[i].selectionCircle.enabled = true;
                    BehaviourTreeManager.Colonists[i].selected = true;
                    if(!SelectedColonists.Contains(BehaviourTreeManager.Colonists[i]))
                        SelectedColonists.Add(BehaviourTreeManager.Colonists[i]);
                }
                else
                {
                    BehaviourTreeManager.Colonists[i].selectionCircle.enabled = false;
                    BehaviourTreeManager.Colonists[i].selected = false;
                    if (SelectedColonists.Contains(BehaviourTreeManager.Colonists[i]))
                        SelectedColonists.Remove(BehaviourTreeManager.Colonists[i]);
                }
            }
            for (int i = 0; i < BehaviourTreeManager.Monsters.Count; i++)
            {
                if (IsWithinBounds(BehaviourTreeManager.Monsters[i].gameObject))
                {
                    BehaviourTreeManager.Monsters[i].SelectionCircle.enabled = true;
                    BehaviourTreeManager.Monsters[i].selected = true;
                    if(!SelectedMonsters.Contains(BehaviourTreeManager.Monsters[i]))
                        SelectedMonsters.Add(BehaviourTreeManager.Monsters[i]);
                }
                else
                {
                    BehaviourTreeManager.Monsters[i].SelectionCircle.enabled = false;
                    BehaviourTreeManager.Monsters[i].selected = false;
                    if(SelectedMonsters.Contains(BehaviourTreeManager.Monsters[i]))
                        SelectedMonsters.Remove(BehaviourTreeManager.Monsters[i]);
                }
            }
        }
    }
    private void CheckButtonActivate()
    {
        //if there is a selected colonist, show the button
        if(SelectedColonists.Count > 0)
        {
            UIPanels.Instance.clearHuntButton.SetActive(true);
        }
        else
        {
            UIPanels.Instance.clearHuntButton.SetActive(false);
        }
        //if there is a selected monster, show the hunt button
        if(SelectedMonsters.Count > 0)
        {
            UIPanels.Instance.huntSelectedButton.SetActive(true);
        }
        else
        {
            UIPanels.Instance.huntSelectedButton.SetActive(false);
        }
    }

    public bool IsWithinBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;
        var camera = Camera.main;
        var viewportBounds = Utils.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }

    private void OnGUI()
    {
        if(isSelecting)
        {
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(.8f, .8f, .95f, .25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(.8f, .8f, .95f));
        }
    }

}
