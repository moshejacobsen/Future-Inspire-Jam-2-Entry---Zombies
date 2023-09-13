using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Buff")]
public class BuffScriptableObject : ScriptableObject
{
    public Sprite buffSprite;
    public Buffs buffs;
    public Color color;
}
