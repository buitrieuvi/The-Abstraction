using Abstraction.SharedModel;
using System.Linq;
using Zenject;
using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Abstraction
{
    public class ChestController : EventTrigger
    {
        public string Id;
        [SerializeField] TMP_Text _text;

        [Inject] GameDataController _gameData;
        [Inject] PanelManager _panelManager;
        [Inject] InputController _input;


        public void Awake()
        {
            _text.alpha = 0;
            isActive = false;
        }

        public override void Enter()
        {
            base.Enter();
            _text.DOFade(1, 0.5f).From(0);
            _input.InputActions.Player.Interact.started += ctx => OnInteract();
        }

        public override void Exit()
        {

            _text?.DOKill();
            _text.DOFade(0, 0.5f);

            _input.InputActions.Player.Interact.started -= ctx => OnInteract();

            base.Exit();
        }

        public void OnInteract()
        {
            if (isActive) return;

            isActive = true;
            ChestPanel cp = (ChestPanel)_gameData.Panels.FirstOrDefault(x => x is ChestPanel);
            cp.ChestId = Id;
            _panelManager.OnInputAction(cp);

        }

    }
}
