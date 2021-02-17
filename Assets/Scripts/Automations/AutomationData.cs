using UnityEngine;

namespace Automations
{
    [CreateAssetMenu(fileName = "New Automation", menuName="New Automation")]
    public class AutomationData : ScriptableObject
    {
        [SerializeField] private Automation _automationData;
        [SerializeField] private AutomationType _automationType;

        public Automation Data => _automationData;
        public AutomationType AutomationType => _automationType;
    }
}