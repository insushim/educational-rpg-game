// Village.cs
using UnityEngine;

public class Village : MonoBehaviour {
    public bool isSafeZone;

    public void HealPlayer(Player player) {
        if (isSafeZone) {
            player.RestoreHealth();
        }
    }
}