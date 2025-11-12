// PlayerHUD.cs
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {
    public Text healthText;
    public Text expText;
    public Text levelText;

    public void UpdateHUD(Player player) {
        healthText.text = "Health: " + player.health;
        expText.text = "EXP: " + player.experience;
        levelText.text = "Level: " + player.level;
    }
}