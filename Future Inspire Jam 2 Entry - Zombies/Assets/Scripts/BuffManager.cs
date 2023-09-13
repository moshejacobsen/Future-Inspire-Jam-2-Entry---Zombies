using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [Header("Buffs Scriptable Objects")]
    [SerializeField] BuffScriptableObject[] BuffsSOArray = new BuffScriptableObject[] {};
    [SerializeField] BuffScriptableObject currentBuff;
    public Buffs buffState;

    private void OnEnable()
    {
        currentBuff = BuffsSOArray[Random.Range(0, BuffsSOArray.Length)];
        GetComponent<SpriteRenderer>().sprite = currentBuff.buffSprite;
        GetComponent<SpriteRenderer>().color = currentBuff.color;
        buffState = currentBuff.buffs;
    }
}
