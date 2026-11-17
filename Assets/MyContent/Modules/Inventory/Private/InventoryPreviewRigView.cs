using UnityEngine;

namespace Modules.Inventory.Private
{
    public class InventoryPreviewRigView : MonoBehaviour
    {
        [SerializeField] private Transform modelAnchor;
    
        public Transform ModelAnchor  => modelAnchor;
    }
}
