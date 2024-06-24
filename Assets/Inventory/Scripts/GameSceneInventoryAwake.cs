using FlMr_Inventory;
using System;
using UnityEditor;
using UnityEngine;
public class GameSceneInventoryAwake : MonoBehaviour
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TagAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    /// <summary>
    /// タグ名の専用UIを表示させるためのPropertyDrawer
    /// </summary>
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 対象のプロパティが文字列かどうか
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            // タグフィールドを表示
            var tag = EditorGUI.TagField(position, label, property.stringValue);

            // タグ名を反映
            property.stringValue = tag;
        }
    }
#endif
    [SerializeField][Tag] private string _tagName;
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
        if (
            GameObject.FindGameObjectWithTag("ArcanaBox_M0") &&
            GameObject.FindGameObjectWithTag("ArcanaBox_M1") &&
            GameObject.FindGameObjectWithTag("ArcanaBox_Q") &&
            GameObject.FindGameObjectWithTag("ArcanaBox_E")
            )
        {
            var gameSceneInventory = gameObject.GetComponent<GameSceneInventory>();
            switch (_tagName)
            {
                case "ArcanaBox_M0":
                    var M0 = GameObject.FindGameObjectWithTag("ArcanaBox_M0").GetComponent<ArcanaBox_Key>();
                    if (M0.GetItem()) gameSceneInventory.AddItem(M0.GetItem().UniqueId, 1);
                    break;
                case "ArcanaBox_M1":
                    var M1 = GameObject.FindGameObjectWithTag("ArcanaBox_M1").GetComponent<ArcanaBox_Key>();
                    if (M1.GetItem()) gameSceneInventory.AddItem(M1.GetItem().UniqueId, 1);
                    break;
                case "ArcanaBox_Q":
                    var Q = GameObject.FindGameObjectWithTag("ArcanaBox_Q").GetComponent<ArcanaBox_Key>();
                    if (Q.GetItem()) gameSceneInventory.AddItem(Q.GetItem().UniqueId, 1);
                    break;
                case "ArcanaBox_E":
                    var E = GameObject.FindGameObjectWithTag("ArcanaBox_E").GetComponent<ArcanaBox_Key>();
                    if (E.GetItem()) gameSceneInventory.AddItem(E.GetItem().UniqueId, 1);
                    break;
            }
        }
        if (_camera)
        {
            _camera.gameObject.SetActive(false);
            _camera.gameObject.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
