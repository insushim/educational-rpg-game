// HuntingGround.cs
using UnityEngine;

public class HuntingGround : MonoBehaviour {
    public int levelRequirement;

    public bool IsPlayerAbleToEnter(Player player) {
        return player.level >= levelRequirement;
    }
}