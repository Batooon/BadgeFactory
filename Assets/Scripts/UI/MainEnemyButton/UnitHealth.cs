using UnityEngine;
using UnityEngine.Events;

//Developer: Antoshka

public class UnitHealth : MonoBehaviour
{
    public FloatVariable CurrentHp;

    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    private DamageDealer _damageDealer;

    private EnemyDataVariable _currentEnemy;

    public void Awake()
    {
        _damageDealer = GetComponent<DamageDealer>();
    }

    public void ClickDamage()
    {
        DealDamage(_damageDealer.DamageAmount);
    }

    private void DealDamage(FloatReference amountOfDamage)
    {
        CurrentHp.ApplyChange(-amountOfDamage);
        DamageEvent.Invoke();

        if (CurrentHp.Value <= 0)
            DeathEvent.Invoke();
    }

    public void InitializeNewEnemy(EnemyDataVariable _newEnemy)
    {
        _currentEnemy = _newEnemy;
        ResetHP();
    }

    private void ResetHP()
    {
        CurrentHp.SetValue(_currentEnemy.EnemyDataVar.Hp);
    }

    private void Update()
    {
        
    }
}
