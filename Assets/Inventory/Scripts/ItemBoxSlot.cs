using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlMr_Inventory
{
    [RequireComponent(typeof(Image))]
    internal class ItemBoxSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image icon;
        internal ItemBase Item { get; private set; }
        internal void UpdateItem(ItemBase item, int number)
        {
            if (number > 0 && item != null)
            {
                Item = item;
                icon.sprite = item.Icon;
                icon.color = Color.white;
                icon.sprite = item.Icon;
                icon.color = Color.white;
                numberText.gameObject.SetActive(number > 1);
                numberText.text = number.ToString();
                Number = number;
                switch (Item.arcanaType)
                {
                    case ItemBase.ArcanaType.Attack:
                        gameObject.GetComponent<Image>().color = Color.red;
                        break;
                    case ItemBase.ArcanaType.Support:
                        gameObject.GetComponent<Image>().color = Color.green;
                        break;
                    case ItemBase.ArcanaType.Ex:
                        gameObject.GetComponent<Image>().color = Color.yellow;
                        break;
                }
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
        [SerializeField] private TextMeshProUGUI numberText;
        private int Number { get; set; }
        private Action<ItemBase, int, GameObject> OnClickCallback { get; set; }
        internal void Initialize(Action<ItemBase, int, GameObject> onClickCallback)
        {
            OnClickCallback = onClickCallback;
        }
        public void OnClicked()
        {
            if (Item == null) return;
            OnClickCallback(Item, Number, gameObject);
        }
        private Transform canvasTran;
        private GameObject draggingObject;
        void Awake()
        {
            canvasTran = transform.parent.parent;
        }

        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            if (Item == null) return;
            var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
            if (box.ArcanaSlotaCheck(Item.UniqueId)) return;
            CreateDragObject();
            draggingObject.transform.position = pointerEventData.position;
        }

        public void OnDrag(PointerEventData pointerEventData)
        {
            if (Item == null) return;
            var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
            if (box.ArcanaSlotaCheck(Item.UniqueId)) return;
            draggingObject.transform.position = pointerEventData.position;
        }

        public void OnEndDrag(PointerEventData pointerEventData)
        {
            if (Item == null) return;
            var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
            if (box.ArcanaSlotaCheck(Item.UniqueId)) return;
            Destroy(draggingObject);
        }

        private void CreateDragObject()
        {
            if (Item == null) return;
            draggingObject = new GameObject("Dragging Object");
            draggingObject.transform.SetParent(canvasTran);
            draggingObject.transform.SetAsLastSibling();
            draggingObject.transform.localScale = Vector3.one;
            CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = false;
            draggingObject.AddComponent<ItemBoxSlot>().Item = Item;
        }

        public void DropSuccess()
        {
            Debug.Log("ê¨å˜!!");
        }

        private void Update()
        {
            var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
            if (box == null) return;
            if (Item == null) return;
            if (icon == null) return;
            if (box.ArcanaSlotaCheck(Item.UniqueId))
            {
                icon.color = Color.white;
            }
            else
            {
                icon.color = Color.black;
            }
        }
    }
}
