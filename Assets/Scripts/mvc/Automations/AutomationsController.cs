using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AutomationsView))]
public class AutomationsController : MonoBehaviour
{
    private AutomationsView _automationsView;
    private AutomationsModel _automationsModel;

    [Inject]
    public void Construct(Data playerData)
    {
        _automationsModel = new AutomationsModel(playerData);
        _automationsView = GetComponent<AutomationsView>();
    }

    private void OnApplicationQuit()
    {
        _automationsModel.Save();
    }
}
