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
            BehaviourTreeManager.Colonists[i].selected = false;
        }
        for (int i = 0; i < BehaviourTreeManager.Monsters.Count; i++)
        {
            BehaviourTreeManager.Monsters[i].SelectionCircle.enabled = false;
            BehaviourTreeManager.Monsters[i].selected = false;
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

        }

        if (Input.GetKeyUp(Keybinds.Instance.PrimaryActionKey))
        {
            isSelecting = false;
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
