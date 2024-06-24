using FlMr_Inventory;
using System;
using TMPro;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

public class ArcanaSlot_Key : MonoBehaviour, IDropHandler
{
    /// UI画像の表示を司るクラス
    /// 所持しているアイテムのアイコンを表示する
    [SerializeField] private Image icon;
    /// このスロットに入っているアイテム
    internal ItemBase Item { get; private set; }
    /// アイテムのアイコンを表示する
    internal void UpdateItem(ItemBase item, int number)
    {
        if (number > 0 && item != null)
        {
            // アイテムが空ではない場合
            Item = item;
            icon.sprite = item.Icon;
            icon.color = Color.white;
            // アイコンの表示
            icon.sprite = item.Icon;
            icon.color = Color.white;
            // 数量の表示
            numberText.gameObject.SetActive(number > 1);
            numberText.text = number.ToString();
            Number = number;
        }
        else
        {
            Item = null;
            Number = 0;
            icon.sprite = null;
            icon.color = new Color(0, 0, 0, 0);
            numberText.gameObject.SetActive(false);
        }
    }
    /// このスロットに入っているアイテムの個数を表示するテキスト
    [SerializeField] private TextMeshProUGUI numberText;
    /// 数量
    private int Number { get; set; }
    /// スロットがクリックされた際に実行するメソッド
    private Action<ItemBase, int, GameObject> OnClickCallback { get; set; }

    [SerializeField][Tag] private string _tagName;

    /// このクラスのインスタンスが生成された際に呼ぶメソッド
    internal void Initialize(Action<ItemBase, int, GameObject> onClickCallback)
    {
        OnClickCallback = onClickCallback;
    }
    /// スロットがクリックされたときに呼ばれるメソッド
    public void OnClicked()
    {
        //このスロットにアイテムが存在している場合
        if (Item != null)
        {
            // コールバックメソッドを実行
            OnClickCallback(Item, Number, this.gameObject);
            if (SceneManager.GetActiveScene().name == "BildScene")
            {
                Debug.Log("選択中のアイテムは" + Item.name + "です");
            }
        }
        else if (SceneManager.GetActiveScene().name == "BildScene")
        {
            Debug.Log("選択中のアイテムは無です");
        }
    }


    public void OnDrop(PointerEventData pointerEventData)
    {
        var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
        if (!box.ArcanaSlotaCheck(pointerEventData.pointerPress.GetComponent<ItemBoxSlot>().Item.UniqueId)) return;
        var m0Slot = GameObject.FindGameObjectWithTag(_tagName).GetComponent<ArcanaBox_Key>();
        if (Item) m0Slot.RemoveItem(Item.UniqueId, 1);
        m0Slot.AddItem(pointerEventData.pointerPress.GetComponent<ItemBoxSlot>().Item.UniqueId, 1);
    }
}
