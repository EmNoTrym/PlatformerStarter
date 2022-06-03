using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class SpineControl : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset run, walk, idle, jump, attack, dead;
    public string currentState;
    public string animationState;
    public string previousState;

    private void Start()
    {
        currentState = "Idle";
        SetCharacterState(currentState);
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        if (animation.name.Equals(animationState))
        {
            return;
        }

        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timescale;
        animationEntry.Complete += AnimationEntry_Complete;
        animationState = animation.name;
        
    }

    //do sth after the animation completes
    private void AnimationEntry_Complete(TrackEntry trackEntry)
    {
        if (currentState.Equals("Jump") )
        {
            SetCharacterState(previousState);
        }

        else if (currentState.Equals("Attack"))
        {
            SetCharacterState("Idle");
        }
    }

    public void SetCharacterState(string state)
    {
        if (state.Equals("Walk"))
        {
            SetAnimation(walk, true, 1f);
        }
        else if (state.Equals("Run"))
        {
            SetAnimation(run, true, 1f);
        }
        else if (state.Equals("Jump"))
        {
            SetAnimation(jump, false, 1f);   
        }
        
        else if (state.Equals("Attack"))
        {
            SetAnimation(attack, false, 1f);
        }

        else if (state.Equals("Idle"))
        {
            SetAnimation(idle, true, 1f);
        }

        else if (state.Equals("Dead"))
        {
            SetAnimation(dead, false, 1f);
        }

        currentState = state;
        
    }

}
