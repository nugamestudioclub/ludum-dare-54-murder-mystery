using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState 
{
    FreeRoam,
    Inventory,
    Dialogue
} 

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager stateManager;

    private PlayerState state;

    private void Awake() {
        stateManager = this;
        state = PlayerState.FreeRoam;
    }

    public PlayerState get() {
        return state;
    }

    public void set(PlayerState state) {
        this.state = state;
    }

    public bool matches(PlayerState state) {
        return (this.state == state);
    }
}
