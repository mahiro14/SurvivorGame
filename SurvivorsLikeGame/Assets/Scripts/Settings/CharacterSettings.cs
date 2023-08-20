using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="CharacterSettings", menuName="ScriptableObjects/CharacterSettings")]
public class CharacterSettings : ScriptableObject
{
    public List<CharacterStats> datas;

    static CharacterSettings instance; 
    public static CharacterSettings Instance
    {
        get
        {
            if(!instance)
            {
                instance = Resources.Load<CharacterSettings>(nameof(CharacterSettings));
            }

            return instance;
        }
    }

    public CharacterStats Get(int id)
    {
        return (CharacterStats)datas.Find(item => item.Id == id).GetCopy();
    }
}

public enum MoveType
{
    TargetPlayer,
    TargetDirection,
}

[Serializable]
public class CharacterStats : BaseStats
{
    public GameObject Prefab;
    public List<int> DefaultWeaponIds;
    public List<int> UsableWeaponIds;
    public int UsableWeaponMax;
    public MoveType MoveType;

    // TODO:アイテム追加
}