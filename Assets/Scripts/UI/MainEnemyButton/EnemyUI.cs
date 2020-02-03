using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Developer: Antoshka

public class EnemyUI : MonoBehaviour
{
    private Image _enemyImage;
    private EnemyDataVariable _currentEnemy;

    public void InitializeEnemy(EnemyDataVariable _newEnemyData)
    {
        _enemyImage = GetComponent<Image>();
        _currentEnemy = _newEnemyData;
        _enemyImage.sprite = _currentEnemy.EnemyDataVar.EnemySprite;
    }
}
