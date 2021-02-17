using Automations;
using UnityEngine;

public class HitDamageSpawner : MonoBehaviour
{
    [SerializeField] private UIObjectPooler _hitTextPooler;
    [SerializeField] private Camera _mainCamera;

    private AutomationsData _automationsData;

    public void Init(AutomationsData automationsData)
    {
        _automationsData = automationsData;
        _hitTextPooler.Init();
    }

    public void SpawnText(long hitValue)
    {
        GameObject hitText = _hitTextPooler.GetPooledObjects();
        hitText.transform.position = transform.position;
        HitDamagePresentation hitDamagePresentation;
        if (hitText.TryGetComponent(out hitDamagePresentation))
        {
            hitDamagePresentation.Init(_automationsData);
            hitDamagePresentation.SpawnHit(hitValue);
        }
    }
}
