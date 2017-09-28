using UnityEngine;
using BehaviourTree;

public enum rootType
{
    Sequence,
    Selector,
}
public class AgentBehaviourTree : MonoBehaviour {



    //this is about as hacky as it gets, will need to do more research/get advice on the best way to approach this
    //  [SerializeField]
    //  private rootType RootType;

    //  [Header("Child Branches (From first to last in order they will be checked)")]
    //   public BehaviourBase[] branches;
    //[HideInInspector]
    //  public Sequence SeqRoot;
    //   [HideInInspector]
    //    public Selector SelRoot;

    //	void Awake ()
    //{
    //       SetupTree();	
    //}

    //   void SetupTree()
    //   {
    //       for (int i = 0; i < branches.Length; i++)
    //           branches[i].OnInitialise(GetComponent<AgentInfo>());
    //       switch (RootType)
    //       {
    //           case rootType.Sequence:
    //               SeqRoot = ScriptableObject.CreateInstance<Sequence>();
    //               for(int i = 0; i < branches.Length; i++)
    //               {
    //                   if(branches[i].SelRoot != null)
    //                       SeqRoot.addChild(branches[i].SelRoot);
    //                   else
    //                   {
    //                       SeqRoot.addChild(branches[i].SeqRoot);
    //                   }
    //               } 
    //               break;

    //           case rootType.Selector:
    //               SelRoot = ScriptableObject.CreateInstance<Selector>();
    //               for (int i = 0; i < branches.Length; i++)
    //               {
    //                   if (branches[i].SelRoot != null)
    //                       SelRoot.addChild(branches[i].SelRoot);
    //                   else
    //                   {
    //                       SelRoot.addChild(branches[i].SeqRoot);
    //                   }
    //               }
    //               break;
    //       }

    //  }
    [Header("Choose Root Type")]
    public rootType root;
    [Space]
    [Header("Add Branches")]
    [SerializeField]
    private BehaviourBase[] Branches;

    [HideInInspector]
    public Sequence seq;
    [HideInInspector]
    public Selector sel;


    private void Awake()
    {
        AgentInfo agent = GetComponent<AgentInfo>();
            //if there are no branches, we dont need to bother 
            if (Branches == null)
                return;
          switch(root)
            {
                //if the desired root type is sequence
                case rootType.Sequence:
                    //instantiate a new sequence
                     seq = ScriptableObject.CreateInstance<Sequence>();
                    //then add a new branch for each of the branches in the array
                    for (int i = 0; i < Branches.Length; i++)
                    {
                       
                        BehaviourBase child = Instantiate(Branches[i]);
                        child.OnInstantiate(agent);
                        seq.addChild(child);
                        
                    }
                    break;
                    //if the desired root type is selector
                case rootType.Selector:
                    //instantiate a new selector
                    sel = ScriptableObject.CreateInstance<Selector>();
                    //then add a new branch for each of the branches in the array
                    for(int i = 0; i < Branches.Length; i++)
                    {
                        BehaviourBase child = Instantiate(Branches[i]);
                        child.OnInstantiate(agent);
                        sel.addChild(child);
                    }
                    break;
            }
    }



}
