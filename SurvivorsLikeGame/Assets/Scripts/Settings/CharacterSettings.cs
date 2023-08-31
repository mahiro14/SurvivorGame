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

    public EnemyController CreateEnemy(int id, GameSceneDirector sceneDirector, Vector3 position)
    {
        CharacterStats stats = Instance.Get(id);

        GameObject obj = Instantiate(stats.Prefab, position, Quaternion.identity);

        EnemyController ctrl = obj.GetComponent<EnemyController>();
        ctrl.Init(sceneDirector, stats);

        return ctrl;
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