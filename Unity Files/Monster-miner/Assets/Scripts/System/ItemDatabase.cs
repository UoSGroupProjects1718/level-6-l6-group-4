using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{ 

    [Header("Deposit all ingame items in this array, it will be sorted into a database")]
    [SerializeField]
    private ItemInfo[] databaseItems;
    [SerializeField]
    GameObject ItemPrefab;
    private static int numWearables;

    private static GameObject SkinnedMeshParent;
    //for now we will just include the same for every object
    [SerializeField]
    private int ObjectPoolSize = 20;
    private static Dictionary<string,ItemInfo> itemDatabase;
    private static Dictionary<string, List<SkinnedMeshRenderer>> skinnedMeshRendererPools;
    private static Dictionary<string, List<GameObject>> WorldItemPool;


    public void Awake()
    {

        
        if (databaseItems.Length == 0)
            Debug.LogError("Database has not been assigned");
        itemDatabase = new Dictionary<string, ItemInfo>();
        WorldItemPool = new Dictionary<string, List<GameObject>>();
        skinnedMeshRendererPools = new Dictionary<string, List<SkinnedMeshRenderer>>();

        GameObject Item = new GameObject();
        Item.name = "Item Pools";


        SkinnedMeshParent = new GameObject();
        SkinnedMeshParent.name = "Skinned Mesh Renderer Pool";

        //populate the database
        for(int i = 0; i < databaseItems.Length; i++)
        {
            //add the item reference to the database
            itemDatabase.Add(databaseItems[i].itemName,databaseItems[i]);

            //then create the mesh pool reference if needed
            if(databaseItems[i].itemMesh)
            {
                List<GameObject> itemPool = new List<GameObject>();
                //create pool for item mesh
                for(int j =0; j < ObjectPoolSize; j++)
                {
                    GameObject newItem = Instantiate(ItemPrefab) as GameObject;
                    newItem.transform.parent = Item.transform;
                    newItem.GetComponent<Item>().item = databaseItems[i];
                    newItem.GetComponent<MeshFilter>().mesh = databaseItems[i].itemMesh;
                    newItem.SetActive(false);
                    itemPool.Add(newItem);
                }
                WorldItemPool.Add(databaseItems[i].itemName, itemPool);
            }

            if(databaseItems[i].type == ItemType.Wearable && (databaseItems[i] as Wearable).equippableMesh)
            {
                numWearables++;
                //create pool for wearableMesh
                List<SkinnedMeshRenderer> skinnedMeshPool = new List<SkinnedMeshRenderer>();
                for(int j =0; j < ObjectPoolSize; j++)
                {
                    SkinnedMeshRenderer newMeshRenderer = Instantiate((databaseItems[i] as Wearable).equippableMesh) as SkinnedMeshRenderer;
                    newMeshRenderer.transform.parent = SkinnedMeshParent.transform;
                    newMeshRenderer.gameObject.SetActive(false);
                    skinnedMeshPool.Add(newMeshRenderer);
                }
                skinnedMeshRendererPools.Add(databaseItems[i].itemName, skinnedMeshPool);
            }

        }
    }

    public static int NumWearables
    {
        get
        {
            return numWearables;
        }
    }
    public static ItemInfo GetItem(string slug)
    {
        if (!itemDatabase.ContainsKey(slug))
            return null;

        return itemDatabase[slug];
    }

    public static SkinnedMeshRenderer GetItemSkinnedMeshRenderer(string slug)
    {
        if (!skinnedMeshRendererPools.ContainsKey(slug))
            return null;


        for(int i = 0; i < skinnedMeshRendererPools[slug].Count; i++)
        {
            if(!skinnedMeshRendererPools[slug][i].gameObject.activeSelf)
            {
                skinnedMeshRendererPools[slug][i].gameObject.SetActive(true);
                return skinnedMeshRendererPools[slug][i];
            }
        }
        return null;

    }
    public static GameObject SpawnItemToWorld(string slug)
    {
        if (!WorldItemPool.ContainsKey(slug))
            return null;

        for(int i = 0; i < WorldItemPool[slug].Count; i++)
        {
            if(!WorldItemPool[slug][i].activeSelf)
            {
                WorldItemPool[slug][i].SetActive(true);
                return WorldItemPool[slug][i];
            }
        }
        return null;
    }
    public static void ReturnSkinnedMeshRenderer(SkinnedMeshRenderer renderer)
    {
        renderer.gameObject.SetActive(false);
        renderer.transform.SetParent(SkinnedMeshParent.transform);
    }


}
