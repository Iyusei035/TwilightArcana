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
/// �^�O���̐�pUI��\�������邽�߂�PropertyDrawer
/// </summary>
[CustomPropertyDrawer(typeof(TagAttribute))]
public class TagAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // �Ώۂ̃v���p�e�B�������񂩂ǂ���
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        // �^�O�t�B�[���h��\��
        var tag = EditorGUI.TagField(position, label, property.stringValue);

        // �^�O���𔽉f
        property.stringValue = tag;
    }
}
#endif

public class ArcanaSlot_Key : MonoBehaviour, IDropHandler
{
    /// UI�摜�̕\�����i��N���X
    /// �������Ă���A�C�e���̃A�C�R����\������
    [SerializeField] private Image icon;
    /// ���̃X���b�g�ɓ����Ă���A�C�e��
    internal ItemBase Item { get; private set; }
    /// �A�C�e���̃A�C�R����\������
    internal void UpdateItem(ItemBase item, int number)
    {
        if (number > 0 && item != null)
        {
            // �A�C�e������ł͂Ȃ��ꍇ
            Item = item;
            icon.sprite = item.Icon;
            icon.color = Color.white;
            // �A�C�R���̕\��
            icon.sprite = item.Icon;
            icon.color = Color.white;
            // ���ʂ̕\��
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
    /// ���̃X���b�g�ɓ����Ă���A�C�e���̌���\������e�L�X�g
    [SerializeField] private TextMeshProUGUI numberText;
    /// ����
    private int Number { get; set; }
    /// �X���b�g���N���b�N���ꂽ�ۂɎ��s���郁�\�b�h
    private Action<ItemBase, int, GameObject> OnClickCallback { get; set; }

    [SerializeField][Tag] private string _tagName;

    /// ���̃N���X�̃C���X�^���X���������ꂽ�ۂɌĂԃ��\�b�h
    internal void Initialize(Action<ItemBase, int, GameObject> onClickCallback)
    {
        OnClickCallback = onClickCallback;
    }
    /// �X���b�g���N���b�N���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    public void OnClicked()
    {
        //���̃X���b�g�ɃA�C�e�������݂��Ă���ꍇ
        if (Item != null)
        {
            // �R�[���o�b�N���\�b�h�����s
            OnClickCallback(Item, Number, this.gameObject);
            if (SceneManager.GetActiveScene().name == "BildScene")
            {
                Debug.Log("�I�𒆂̃A�C�e����" + Item.name + "�ł�");
            }
        }
        else if (SceneManager.GetActiveScene().name == "BildScene")
        {
            Debug.Log("�I�𒆂̃A�C�e���͖��ł�");
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
