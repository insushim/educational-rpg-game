// MonsterData.cs
using System;

[Serializable]
public class MonsterData {
    public string monsterName;
    public int level;
    public float expReward;

    public void ScaleExpReward() {
        expReward = level * 10f; // Example scaling logic
    }
}