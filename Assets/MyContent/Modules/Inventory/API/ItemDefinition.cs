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
        [SerializeField] private Vector3 equippedPositionOffset;
        [SerializeField] private Vector3 equippedRotationOffset;
        [SerializeField, Min(0.01f)] private float inventoryPreviewScale = 1f;

        
        public string DisplayName => displayName;
        public GameObject ModelPrefab => modelPrefab;
        public Vector3 EquippedPositionOffset => equippedPositionOffset;
        public Vector3 EquippedRotationOffset => equippedRotationOffset;
        public float InventoryPreviewScale => inventoryPreviewScale;
    }
}