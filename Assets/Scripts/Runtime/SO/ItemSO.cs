using UnityEngine;
using UnityEngine.UI;

namespace Abstraction
{

    [CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public string Id;
        public string Name;
        public Sprite Sprite;
        public RankSO Rank;
    }

}