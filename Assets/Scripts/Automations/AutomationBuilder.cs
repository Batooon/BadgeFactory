using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Automations
{
    public class AutomationBuilder : IAutomationBuilder
    {
        private AutomationPresentation _product;
        private readonly AutomationPresentation _automationPresentationPrefab;
        private AutomationLogic _automationLogic;
        private readonly PlayerData _playerData;
        private readonly AutomationsData _automationsData;
        private Automation _automation;

        public AutomationBuilder(PlayerData playerData, AutomationsData automationsData, AutomationPresentation automationPrefab)
        {
            _playerData = playerData;
            _automationsData = automationsData;
            _automationPresentationPrefab = automationPrefab;
            Reset();
        }

        private void Reset()
        {
            _product = _automationPresentationPrefab;
        }

        public void InstantiateAutomation(Transform parent, Automation automationData)
        {
            _product = Object.Instantiate(_automationPresentationPrefab.gameObject, parent)
                .GetComponent<AutomationPresentation>();
            _automation = automationData;
            _product.Init(automationData);
            _automationLogic = new AutomationLogic(_playerData, _automationsData, _product, _automation);
        }

        public void AddSkill(IAutomationCommand command, UpgradeComponentData skillData)
        {
            SpawnSkillIcon(_product.SkillIconsParent, skillData.IconSkillPath, skillData);
            _product.AddSkillPrefab(InitializeSkill(skillData.SkillPrefabPath, command, skillData));
        }

        public void SetAutomationType(IAutomation automation)
        {
            _automationLogic.SetAutomation(automation);
        }

        public AutomationPresentation GetFinalAutomation()
        {
            var result = _product;
            
            Reset();

            return result;
        }

        private AutomationUpgradeComponentPresenter InitializeSkill(string skillPath, IAutomationCommand skillCommand,
            UpgradeComponentData skillData)
        {
            try
            {
                var skillPrefab = Resources.Load<AutomationUpgradeComponentPresenter>(skillPath);
                skillPrefab.SetUpgradeComponentData(skillData);
                var _ = new UpgradeComponent(_playerData, _automationsData, _automation, skillData,
                    skillCommand,
                    skillPrefab);

                return skillPrefab;
            }
            catch (Exception e)
            {
                Debug.LogError("Something wrong with skill initialization:");
                Debug.LogError(e);
            }

            return null;
        }

        private void SpawnSkillIcon(Transform parent, string prefabPath, UpgradeComponentData skillData)
        {
            var skillIcon = Resources.Load<SkillIconPresentation>(prefabPath);
            Object.Instantiate(skillIcon.gameObject, parent);
            skillIcon.Init(skillData);
        }
    }
}