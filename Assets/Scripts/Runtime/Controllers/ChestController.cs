using Abstraction.SharedModel;
using System.Linq;
using Zenject;
using System;
using UnityEngine;

namespace Abstraction
{
    public class ChestController : EventTrigger
    {
        public string Id;

        [Inject] GameDataController _gameData;
        [Inject] PanelManager _panelManager;

        public override void Enter()
        {
            base.Enter();

            ChestPanel panel = (ChestPanel)_gameData.Panels.FirstOrDefault(p => p is ChestPanel);

            //panel.LoadData_R(Id);
            //panel.LoadData_L();
            _panelManager.OnInputAction(panel);


        }
    }
}
