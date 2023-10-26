using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] private UIIntentoryItem itemPrefab;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private UIInventoryDescription itemDescription;
        [SerializeField] private MouseFollower mouseFollower;

        public event Action<int> OnDescriptionRequested, OnItemActionRequest, OnStartDragging;
        public event Action<int, int> onSwapItems;

        private int currentlyDragItemIndex = -1;

        List<UIIntentoryItem> listOfUIItems = new List<UIIntentoryItem>();

        private void Awake()
        {
            Hide();
            mouseFollower.Toogle(false);
            itemDescription.ResetDescription();
        }

        public void IntializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIIntentoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseButtonClick += HandleShowItemActions;

            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UIIntentoryItem item)
        {

        }

        private void HandleSwap(UIIntentoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            onSwapItems?.Invoke(currentlyDragItemIndex, index);

        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toogle(false);
            currentlyDragItemIndex = -1;
        }

        private void HandleEndDrag(UIIntentoryItem item)
        {
            ResetDraggedItem();
        }

        private void HandleBeginDrag(UIIntentoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
                return;
            currentlyDragItemIndex = index;
            HandleItemSelection(item);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toogle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIIntentoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();

        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIIntentoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }

        public void ResetAllItems()
        {
            foreach (var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}