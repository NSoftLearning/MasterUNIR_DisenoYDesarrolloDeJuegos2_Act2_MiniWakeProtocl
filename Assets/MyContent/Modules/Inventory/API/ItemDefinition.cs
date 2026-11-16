using UnityEngine;

namespace Modules.Inventory.API
{
    [CreateAssetMenu(
        fileName = "ItemDefinition",
        menuName = "Tools/Config/Modules/Inventory/Item")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField] private string displayName;
        [SerializeField] private GameObject modelPrefab;
        
        public string DisplayName => displayName;
        public GameObject ModelPrefab => modelPrefab;
    }
}