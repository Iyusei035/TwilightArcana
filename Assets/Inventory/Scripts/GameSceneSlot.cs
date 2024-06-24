using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
namespace FlMr_Inventory
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TagAttribute : PropertyAttribute
    {
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }
            var tag = EditorGUI.TagField(position, label, property.stringValue);
            property.stringValue = tag;
        }
    }
#endif
    internal class GameSceneSlot : MonoBehaviour
    {
        [SerializeField][Tag] private string _tagName;
        [SerializeField] private Image icon;
        [SerializeField] private Image shutOut;
        internal ItemBase Item { get; private set; }
        internal void UpdateItem(ItemBase item, int number)
        {
            if (number > 0 && item != null)
            {
                Item = item;
                icon.sprite = item.Icon;
                icon.color = Color.white;
                Number = number;
            }
            else
            {
                Item = null;
                Number = 0;
                icon.sprite = null;
                icon.color = new Color(0, 0, 0, 0);
            }
        }
        private int Number { get; set; }
        private Action<ItemBase, int, GameObject> OnClickCallback { get; set; }
        internal void Initialize(Action<ItemBase, int, GameObject> onClickCallback)
        {
            OnClickCallback = onClickCallback;
        }
        public void OnClicked()
        {
            if (Item != null)
            {
                OnClickCallback(Item, Number, this.gameObject);
            }
        }
        private float coolTimeCount = 0;
        private void Update()
        {
            if (Item == null) return;
            if (!Item.GetActiveFlg())
            {
                shutOut.gameObject.SetActive(true);
                coolTimeCount =Item.GetAdjustmentCooltime();
                shutOut.rectTransform.sizeDelta =
                    new Vector2
                    (
                        shutOut.rectTransform.rect.width, shutOut.rectTransform.rect.width - (coolTimeCount * 8)
                        );
            }
            else
            {
                shutOut.gameObject.SetActive(false);
            }
        }
    }
}
