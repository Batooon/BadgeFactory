using UnityEngine;
using Zenject;

public class UntitledInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject BadgeButton;

    public override void InstallBindings()
    {
        SessionController sessionController = GetComponent<SessionController>();
        Container.Bind<SessionController>().FromInstance(sessionController);
        Container.Bind<Data>().FromInstance(sessionController._sessionModel.PlayerData);
        Container.InstantiateComponent(typeof(BadgeController), BadgeButton);
    }
}