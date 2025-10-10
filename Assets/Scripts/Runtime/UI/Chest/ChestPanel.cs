using Abstraction.SharedModel;
using Codice.Client.BaseCommands.BranchExplorer;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;

namespace Abstraction
{
    public class ChestPanel : PanelBase
    {
        public string ChestId;
        public ChestSM ChestData => gameData.ChestInventory.First(c => c.Id == ChestId);

        [Inject] private GameDataController gameData;

        [SerializeField] private ScrollRect _scroll_L;
        [SerializeField] private ScrollRect _scroll_R;

        private List<ChestSlot> slots_L => _scroll_L.content.GetComponentsInChildren<ChestSlot>().ToList();
        private List<ChestSlot> slots_R => _scroll_R.content.GetComponentsInChildren<ChestSlot>().ToList();

        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
            
            foreach (var slot in slots_L.Concat(slots_R))
            {
                slot.SetItem();

                slot.View.ClickEvt += (e =>
                {
                    OnSelect(e);
                });
            }

        }

        public override void OnSelect(SlotBase slot)
        {
            var chestSlot = slot as ChestSlot;

            if (chestSlot.Item == null || chestSlot.Quantity == 0) 
            {
                Debug.Log("Item null");
                return;
            }
            

            if (slots_L.Contains(chestSlot))
            {
                int indexL = slots_L.IndexOf(chestSlot);
                var itemL = gameData.PlayerInventory[indexL];
                if (itemL.Quantity > 1)
                {
                    itemL.Quantity--;
                    Reload_L(indexL);

                    int itemR = ChestData.Items.IndexOf(ChestData.Items.FirstOrDefault(i => i.Id == itemL.Id));
                    if (itemR == -1)
                    {
                        ChestData.Items.Add(new InventorySlotSM(itemL.Id, 1));
                        itemR = ChestData.Items.Count - 1;
                    }
                    else 
                    {
                        ChestData.Items[itemR].Quantity++;
                    }

                    Reload_R(itemR);
                }
                else 
                {
                    gameData.PlayerInventory.RemoveAt(indexL);

                    chestSlot.Item = null;
                    chestSlot.Quantity = 0;

                    chestSlot.SetItem();
                    _scroll_L.content.GetChild(indexL).SetAsLastSibling();
                }
            }
            else
            {
                int indexR = slots_R.IndexOf(chestSlot);
                var itemR = ChestData.Items[indexR];

                if (itemR.Quantity > 1)
                {
                    itemR.Quantity--;
                    Reload_R(indexR);

                    int itemL = gameData.PlayerInventory.IndexOf(gameData.PlayerInventory.FirstOrDefault(i => i.Id == itemR.Id));
                    if (itemL == -1)
                    {
                        gameData.PlayerInventory.Add(new InventorySlotSM(itemR.Id, 1));
                        itemL = gameData.PlayerInventory.Count - 1;
                    }
                    else
                    {
                        gameData.PlayerInventory[itemL].Quantity++;
                    }

                    Reload_L(itemL);
                }
                else
                {
                    ChestData.Items.RemoveAt(indexR);

                    chestSlot.Item = null;
                    chestSlot.Quantity = 0;

                    chestSlot.SetItem();
                    _scroll_R.content.GetChild(indexR).SetAsLastSibling();
                }
            }
        }

        public void Reload_L(int l)
        {
            ItemSO item = gameData.ItemSOs.FirstOrDefault(it => it.Id == gameData.PlayerInventory[l].Id);
            int quantity = gameData.PlayerInventory[l].Quantity;

            slots_L[l].Item = item;
            slots_L[l].Quantity = quantity;

            slots_L[l].SetItem();
        }

        public void Reload_R(int r)
        {
            ItemSO item = gameData.ItemSOs.FirstOrDefault(it => it.Id == ChestData.Items[r].Id);
            int quantity = ChestData.Items[r].Quantity;

            slots_R[r].Item = item;
            slots_R[r].Quantity = quantity;

            slots_R[r].SetItem();
        }

        public override void Open()
        {
            foreach (var slot in slots_L.Concat(slots_R).Where(x => x.View.gameObject.activeSelf))
            {
                slot.Item = null;
                slot.Quantity = 0;

                slot.SetItem();
            }

            for (int i = 0; i < gameData.PlayerInventory.Count; i++)
            {
                Reload_L(i);
            }

            for (int i = 0; i < ChestData.Items.Count; i++)
            {
                Reload_R(i);
            }


            base.Open();
        }

        public override void Close()
        {
            base.Close();
        }

        public override void OnCompleted()
        {
            base.OnCompleted();
        }

        public override void OnCloseCompleted()
        {
            base.OnCloseCompleted();
        }

        public override void CloseImmediately()
        {
            base.CloseImmediately();
        }

        public override void OnCloseButton()
        {
            base.OnCloseButton();
        }
    }
}

