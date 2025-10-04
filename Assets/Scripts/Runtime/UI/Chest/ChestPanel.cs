using Abstraction.SharedModel;
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
                slot.View.ClickEvt += (e =>
                {
                    if (!slot.View.IsData) return;
                    Sl(e as ChestSlot);
                });
            }
        }

        public void Sl(ChestSlot sl)
        {
            if (!sl.View.gameObject.activeInHierarchy) return;

            if (slots_L.Contains(sl))
            {
                sl.transform.SetParent(_scroll_R.content);
                sl.transform.SetSiblingIndex(slots_R.IndexOf(slots_R.First(x => !x.View.IsData)));

                int mt = slots_R.First(x => !x.View.IsData).transform.GetSiblingIndex();
                _scroll_R.content.GetChild(mt).SetParent(_scroll_L.content);
            }
            else
            {
                sl.transform.SetParent(_scroll_L.content);
                sl.transform.SetSiblingIndex(slots_L.IndexOf(slots_L.First(x => !x.View.IsData)));

                int mt = slots_L.First(x => !x.View.IsData).transform.GetSiblingIndex();
                _scroll_L.content.GetChild(mt).SetParent(_scroll_R.content);
            }
        }

        public void LoadData_L()
        {
            var playerInventory = gameData.PlayerInventory;

            for (int i = 0; i < playerInventory.Count; i++)
            {
                ItemSO item = gameData.ItemSOs.FirstOrDefault(it => it.Id == playerInventory[i].Id);

                slots_L[i].View.SetView(item.Sprite, playerInventory[i].Quantity, item.Rank.Color);
            }
        }

        public void LoadData_R()
        {
            var chest = gameData.ChestInventory.FirstOrDefault(c => c.Id == ChestId);

            for (int i = 0; i < chest.Items.Count; i++)
            {
                ItemSO item = gameData.ItemSOs.FirstOrDefault(it => it.Id == chest.Items[i].Id);
                Color rank = gameData.RankSOs.FirstOrDefault(r => r.Color == item.Rank.Color).Color;

                slots_R[i].View.SetView(item.Sprite, chest.Items[i].Quantity, rank);
            }

        }

        public override void Open()
        {
            foreach (var slot in slots_L.Concat(slots_R).Where(x => x.View.gameObject.activeSelf))
            {
                slot.View.SetViewNull();
            }

            LoadData_L();

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

        public override void OnSelected(SlotBase slot)
        {
            base.OnSelected(slot);
        }
    }
}

