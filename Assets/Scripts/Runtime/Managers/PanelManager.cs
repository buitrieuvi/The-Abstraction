using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Abstraction
{
    public class PanelManager : MonoBehaviour
    {
        private DarkPanel _darkPanel;

        [Inject] private GameDataController _gameData;
        [Inject] private InputController _input;
        [Inject] private DiContainer _container;

        public bool IsLoading;
        public PanelBase PanelSelected { get; set; }

        [SerializeField] GameObject _content;

        public void Awake()
        {
            _darkPanel = GetComponentInChildren<DarkPanel>(true);
        }

        public void Start()
        {
            _input.InputActions.Player.Inventory.started += ctx =>
            {
                OnInputAction(_gameData.Panels.First(x => x is InventoryPanel));
            };

            _input.InputActions.Player.Esc.started += ctx =>
            {
                OnInputAction(_gameData.Panels.First(x => x is MenuPanel));
            };
        }

        public async void OnInputAction(PanelBase pnPrefab)
        {
            if (IsLoading) 
            {
                Debug.Log("Loading...");
                return;
            }

            if (pnPrefab == null)
            {
                Debug.Log("pnPrefab null");
                return;
            }

            if (PanelSelected == null) 
            {
                IsLoading = true;
                await _darkPanel.Transition(() => OpenPanel(pnPrefab));
                return;
            }

            if (PanelSelected.GetType() == pnPrefab.GetType()) 
            {
                IsLoading = true;
                await _darkPanel.Transition(() => OpenPanel(pnPrefab));
                return;
            }

            if (pnPrefab is MenuPanel) 
            {
                IsLoading = true;
                await _darkPanel.Transition(() => OpenPanel(PanelSelected));
                return;
            }

        }

        public void OpenPanel(PanelBase pnPrefab)
        {
            var hies = transform.GetComponentsInChildren<PanelBase>(true).ToList();
            var hie = hies.FirstOrDefault(x => x.GetType() == pnPrefab.GetType());

            if (hie == null)
            {
                var panel = _container.InstantiatePrefabForComponent<PanelBase>(pnPrefab.gameObject, _content.transform);
                panel.Open();
            }
            else
            {
                if (hie.gameObject.activeSelf)
                {
                    hie.Close();
                }
                else
                {
                    hie.Open();
                }
            }
        }
    }
}