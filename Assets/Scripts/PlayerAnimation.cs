using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(SpriteAnimation))]
public class PlayerAnimation : MonoBehaviour
{
    public List<AnimationStateData> animationStates = new List<AnimationStateData>();
    private Dictionary<PlayerAnimationState, AnimationData> animationDictionary = new Dictionary<PlayerAnimationState, AnimationData>();
    private SpriteAnimation spriteAnimator;
    public PlayerAnimationState currentState;
    public void Start()
    {
        currentState = PlayerAnimationState.IDLE_DOWN;
        InitializeDictionary();
        spriteAnimator = GetComponent<SpriteAnimation>();
        TopDownPlayerMovement topDownPlayerMovement = GetComponent<TopDownPlayerMovement>();
        topDownPlayerMovement.OnMove += SetAnimationState;
    }
    public void SetAnimationState(Vector2 moveDirection)
    {
        if(moveDirection == Vector2.zero)
        {
            currentState = GetIdleState(currentState);
        }
        if(moveDirection.y < 0)
        {
            currentState = PlayerAnimationState.WALK_DOWN;
        }
        else if(moveDirection.y > 0)
        {
            currentState = PlayerAnimationState.WALK_UP;
        }

        else if (moveDirection.x > 0)
        {
            currentState = PlayerAnimationState.WALK_RIGHT;
        }

        else if(moveDirection.x < 0)
        {
            currentState = PlayerAnimationState.WALK_LEFT;
        }
        spriteAnimator.InitializeAnimation(animationDictionary[currentState]);

    }
    public PlayerAnimationState GetIdleState(PlayerAnimationState currentState)
    {
        PlayerAnimationState tmp = PlayerAnimationState.IDLE_DOWN;
        switch (currentState)
        {
            case PlayerAnimationState.WALK_DOWN:
                tmp = PlayerAnimationState.IDLE_DOWN;
                break;
            case PlayerAnimationState.WALK_UP:
                tmp = PlayerAnimationState.IDLE_UP;
                break;
            case PlayerAnimationState.WALK_RIGHT:
                tmp = PlayerAnimationState.IDLE_RIGHT;
                break;
            case PlayerAnimationState.WALK_LEFT:
                tmp = PlayerAnimationState.IDLE_LEFT;
                break;
            default:
                tmp = PlayerAnimationState.IDLE_DOWN;
                break;


        }

        return tmp;
    }

    public void InitializeDictionary()
    {
        foreach (AnimationStateData animationStateData in animationStates)
        {
            animationDictionary.Add(animationStateData.state, animationStateData.animation);
        }
    }
}


public enum PlayerAnimationState
{
    WALK_UP,
    WALK_DOWN,
    WALK_LEFT,
    WALK_RIGHT,
    IDLE_UP,
    IDLE_DOWN,
    IDLE_LEFT,
    IDLE_RIGHT
   
}

[Serializable]
public class AnimationStateData
{
    public PlayerAnimationState state;
    public AnimationData animation;
}
