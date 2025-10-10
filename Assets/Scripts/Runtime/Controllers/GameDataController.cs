using Abstraction.SharedModel;
using Cysharp.Threading.Tasks;
using ModestTree;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Abstraction
{
    public class GameDataController
    {
        public List<PanelBase> Panels = new();
        public List<ItemSO> ItemSOs = new();
        public List<RankSO> RankSOs = new();

        //[Inject] public PlayerSM PlayerData;

        public ReactiveCollection<InventorySlotSM> PlayerInventory = new(); //add
        public ReactiveCollection<ChestSM> ChestInventory = new(); //add

        public GameDataController()
        {
            LoadAllDataAsync().Forget();
        }

        public async UniTask LoadAllDataAsync()
        {
            Caching.ClearCache();

            var panels = LoadAssetsAsync<GameObject>("panel", so => Panels.Add(so.GetComponent<PanelBase>()));

            var items = LoadAssetsAsync<ItemSO>("item", so => ItemSOs.Add(so));
            var rank = LoadAssetsAsync<RankSO>("rank", so => RankSOs.Add(so));

            await UniTask.WhenAll(panels, items, rank).ContinueWith(() =>
            {
                PlayerInventory.Add(new InventorySlotSM("1", 10));
                PlayerInventory.Add(new InventorySlotSM("2", 10));

                ChestInventory.Add(new ChestSM("1", new List<InventorySlotSM>
                {
                    new InventorySlotSM("1", 5),
                    new InventorySlotSM("2", 3)
                }));
            });
        }

        private async UniTask UpdateRemoteCatalogIfAvailable()
        {
            var checkHandle = Addressables.CheckForCatalogUpdates();
            var catalogs = await checkHandle.Task;

            if (catalogs != null && catalogs.Count > 0)
            {
                Debug.Log("📦 Catalog update found. Updating...");
                var updateHandle = Addressables.UpdateCatalogs(catalogs);
                await updateHandle.Task;
                Debug.Log("✅ Catalog updated.");
            }
            else
            {
                Debug.Log("ℹ️ No catalog updates found.");
            }
        }

        private async UniTask LoadAssetsAsync<T>(string label, System.Action<T> onAssetLoaded)
        {
            var handle = Addressables.LoadAssetsAsync<T>(label, onAssetLoaded);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"✅ Loaded {typeof(T).Name}: {handle.Result.Count}");
            }
            else
            {
                Debug.LogError($"❌ Failed to load {typeof(T).Name} with label '{label}'");
            }
        }


        public List<ItemSO> GetItemSOsInventory() 
        {
            var list = new List<ItemSO>();


            return ItemSOs;
        }
    } 
}