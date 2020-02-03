using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class EnemyDataVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public EnemyData EnemyDataVar;

    public void SetValue(EnemyData data)
    {
        EnemyDataVar = data;
    }

    public static implicit operator EnemyData(EnemyDataVariable enemyData)
    {
        return enemyData.EnemyDataVar;
    }
}
