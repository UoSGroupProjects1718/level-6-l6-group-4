//using System.Collections.Generic;
//using UnityEngine;


//namespace BehaviourTree
//{
    //public enum Status
    //{
    //    BT_INVALID,
    //    BT_SUCCESS,
    //    BT_FAILURE,
    //    BT_RUNNING,
    //    BT_ABORTED,
    //};

    //base behaviour class, all inherit from this
    //public class Behaviour : ScriptableObject
    //{

    //    private Status _Status;

    //    public Behaviour()
    //    {
    //        _Status = Status.BT_INVALID;
    //    }
    //    //function to affect what happens on the instantiation of an object (this should only affect the leaf nodes really)
    //    //giving it an agent input as a basic argument because we mostly want this for passing down the agent blackboard
    //    public virtual void OnInstantiate(AgentInfo agent) { }
    //    public virtual Status updateFunc() { return Status.BT_INVALID; }
    //    public virtual void onInitialise() { }
    //    public virtual void onTerminate(Status status) { }

    //    public Status tick()
    //    {
    //        if (_Status != Status.BT_RUNNING)
    //            onInitialise();

    //        _Status = updateFunc();

    //        if (_Status != Status.BT_RUNNING)
    //            onTerminate(_Status);

    //        return _Status;
    //    }

    //    public void reset() { _Status = Status.BT_INVALID; }
    //    public void abort() { onTerminate(Status.BT_ABORTED); _Status = Status.BT_ABORTED; }
    //    public bool isTerminated() { return _Status == Status.BT_SUCCESS || _Status == Status.BT_FAILURE; }
    //    public bool isRunning() { return _Status == Status.BT_RUNNING; }
    //    public Status getStatus() { return _Status; }



    //}
    //the base decorator class, only has one child
    //public class Decorator : Behaviour
    //{
    //    protected Behaviour Child;

    //    public Decorator(Behaviour child)
    //    {
    //        Child = child;
    //    }
    //}
    //public class Repeat : Decorator
    //{

    //    protected int Limit;
    //    protected int Counter;

    //    public Repeat(Behaviour child) : base(child) { }
    //    public void setCount(int count)
    //    {
    //        Limit = count;
    //    }
    //    public override void onInitialise()
    //    {
    //        Counter = 0;
    //    }
    //    public override Status updateFunc()
    //    {
    //        for (int counter = Counter; counter < Limit; counter++)
    //        {
    //            Counter = counter;
    //            Child.tick();
    //            if (Child.getStatus() == Status.BT_RUNNING) break;
    //            if (Child.getStatus() == Status.BT_FAILURE) return Status.BT_FAILURE;
    //            if (Counter == Limit) return Status.BT_SUCCESS;
    //        }
    //        return Status.BT_INVALID;
    //    }
    //}

   //public class Composite : BehaviourBehaviourBase
   // {
   //     protected List<Behaviour> Children = new List<Behaviour>();

   //     public void addChild(Behaviour child)
   //     {
   //         if (Children == null)
   //             Children = new List<Behaviour>();
   //         Children.Add(child);
   //     }
   //     public void removeChild(Behaviour child) { Children.Remove(child); }
   //     public void clearChildren() { Children.Clear(); }

   // }
    /// <summary>
    /// The sequence node will return failure if any of it's child nodes return failure, only when a child node suceeds
    /// will it continue to the next node in the sequence.
    /// </summary>
    //public class Sequence : Composite
    //{
    //    protected Behaviour currentChild;
    //    //private int childIndex;
    //    public override void onInitialise()
    //    {
    //        //childIndex = 0;
    //        //always make sure that it is the very front of the list of children
    //        currentChild = Children[0];
    //    }
    //    public override Status updateFunc()
    //    {
    //        //loop through all children
    //        for (int i = 0; i < Children.Count; i++)
    //        {
    //            //and execute their tick function
    //            Status childStatus = Children[i].tick();
    //            //if they are running, return that
    //            if (childStatus == Status.BT_RUNNING)
    //            {
    //                return Status.BT_RUNNING;
    //            }
    //            //or if they fail, do the same
    //            else if (childStatus == Status.BT_FAILURE)
    //            {
    //                return Status.BT_FAILURE;
    //            }
    //        }
    //        //if neither of these are true, then they have succeeded, we are at the end of the list of children and the sequencer may return success
    //        return Status.BT_SUCCESS;
    //    }
    //}
    ///// <summary>
    /////The selector will return failure only when none of it's child behaviours return sucess. Otherwise known as the
    /////"Fallback" node, if a child returns failure, it will check the next child in the list. Checking in order of priority from the first
    /////child to the last (left to right)
    ///// </summary>
    //public class Selector : Composite
    //{
    //    protected Behaviour Current;
    //    private int index;
    //    public override void onInitialise()
    //    {
    //        index = 0;
    //        Current = Children[index];
    //    }
    //    public override Status updateFunc()
    //    {
    //        //loop through every child
    //        for (int i = 0; i < Children.Count; i++)
    //        {
    //            //then call their tick function
    //            Status childStatus = Children[i].tick();
    //            //if it is running, return running
    //            if (childStatus == Status.BT_RUNNING)
    //            {
    //                return Status.BT_RUNNING;
    //            }
    //            //or if it succeeds then return the same
    //            else if (childStatus == Status.BT_SUCCESS)
    //                return Status.BT_SUCCESS;
    //        }
    //        //if none of the child behaviours succeed then the selector must do the same
    //        return Status.BT_FAILURE;
    //    }
    //}

    //public class ActiveSelector : Selector
    //{
    //    public override void onInitialise()
    //    {
    //        //set current to the last object in the array
    //        Current = Children[Children.Count - 1];
    //    }
    //    public override Status updateFunc()
    //    {
    //        Behaviour previous = Current;
    //        onInitialise();
    //        Status result = base.updateFunc();  

    //        if (previous != Children[Children.Count - 1])
    //        {
    //            previous.onTerminate(Status.BT_ABORTED);
    //        }
    //        return result;
    //    }
    //}

    //public class Parallel : Composite
    //{
    //    public enum Policy
    //    {
    //        RequireOne,
    //        Requireall,
    //    };
    //    protected Policy e_SuccessPolicy;
    //    protected Policy e_FailurePolicy;

    //    //constructor
    //    public Parallel(Policy forSuccess, Policy forFailure)
    //    {
    //        e_SuccessPolicy = forSuccess;
    //        e_FailurePolicy = forFailure;
    //    }

    //    public override Status updateFunc()
    //    {
    //        int successCount = 0;
    //        int failureCount = 0;
    //        //loop through children
    //        for (int i = 0; i < Children.Count; ++i)
    //        {

    //            Behaviour b = Children[i];
    //            //if the behaviour isnt terminated, tick
    //            if (!b.isTerminated())
    //            {
    //                b.tick();
    //            }
    //            //if the status is success
    //            if (b.getStatus() == Status.BT_SUCCESS)
    //            {
    //                //increase success count
    //                ++successCount;
    //                //if the policy is to require only one, return success
    //                if (e_SuccessPolicy == Policy.RequireOne)
    //                {
    //                    return Status.BT_SUCCESS;
    //                }
    //            }
    //            //if the status is failure
    //            if (b.getStatus() == Status.BT_FAILURE)
    //            {
    //                ++failureCount;
    //                //and the failure policy is to only require one, return fail
    //                if (e_FailurePolicy == Policy.RequireOne)
    //                {
    //                    return Status.BT_FAILURE;
    //                }
    //            }
    //        }
    //        //if the policy is to require all to fail, and the failure count is the same as the number of children, return failure
    //        if (e_FailurePolicy == Policy.Requireall && failureCount == Children.Count)
    //        {
    //            return Status.BT_FAILURE;
    //        }
    //        //or if the policy is to require all to succeed and all have succeeded, return success
    //        if (e_SuccessPolicy == Policy.Requireall && successCount == Children.Count)
    //        {
    //            return Status.BT_SUCCESS;
    //        }
    //        //otherwise just return running
    //        return Status.BT_RUNNING;
    //    }
    //}
//}



