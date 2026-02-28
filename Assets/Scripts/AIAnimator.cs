using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(SpriteAnimation))]
public class AIAnimator : MonoBehaviour
{
    AIMovement aiMovement;
    SpriteAnimation spriteAnimation;
    public List<AnimationStateData> animations;
    private Dictionary<PlayerAnimationState, AnimationData> animationDictionary;
    public PlayerAnimationState currentState;
    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        spriteAnimation = GetComponent<SpriteAnimation>(); 
        aiMovement.OnVelocityChange += UpdateAnimation;
        InitializeAnimationDictionary();
    }

    private void InitializeAnimationDictionary()
    {
        animationDictionary = new Dictionary<PlayerAnimationState, AnimationData>();
        foreach (AnimationStateData animationStateData in animations)
        {
            animationDictionary.Add(animationStateData.state, animationStateData.animation);
        }
    }

    private void UpdateAnimation(Vector2 moveDir_)
    {
       float absX = Mathf.Abs(moveDir_.x);
       float absY = Mathf.Abs(moveDir_.y);
        PlayerAnimationState prevState = currentState;
        if(absX > absY)
        {
            if (moveDir_.x > 0) currentState = PlayerAnimationState.WALK_RIGHT;
            else currentState = PlayerAnimationState.WALK_LEFT;
        }
        else
        {
            if (moveDir_.y > 0) currentState = PlayerAnimationState.WALK_UP;
            else currentState = PlayerAnimationState.WALK_DOWN;
        }

        if(currentState != prevState) spriteAnimation.InitializeAnimation(animationDictionary[currentState]);

    }
}
