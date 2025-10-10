using Cysharp.Threading.Tasks;
using System.Collections;
using System.Xml;
using TMPro;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Abstraction
{
    public class TalkPanel : PanelBase
    {
        [SerializeField] TMP_Text tmpText;
        [SerializeField] TalkSlot talkSlot;

        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Open()
        {
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

