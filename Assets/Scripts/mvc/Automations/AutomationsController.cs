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

        InstantiateAutomations();
    }

    public void InstantiateAutomations()
    {
        _automationsView.InitAutomations(_automationsModel.AutomationData.Automations);

        for (int i = 0; i < _automationsView.AutomationsObjects.Count; i++)
        {
            AutomationLogic automationLogic = _automationsView.AutomationsObjects[i].AddComponent<AutomationLogic>();
            switch (_automationsModel.AutomationData.Automations[i].AutomationType)
            {
                case AutomationTypes.ClickPower:
                    automationLogic.InitializeAutomation(new ClickAutomation(),
                        _automationsModel.AutomationData.Automations[i], _automationsModel.PlayerData);
                    break;
                case AutomationTypes.UsualAutomation:
                    automationLogic.InitializeAutomation(new UsualAutomation(),
                        _automationsModel.AutomationData.Automations[i], _automationsModel.PlayerData);
                    break;
                default:
                    throw new System.NotSupportedException();
            }
        }
    }

    private void OnApplicationQuit()
    {
        _automationsModel.Save();
    }
}
