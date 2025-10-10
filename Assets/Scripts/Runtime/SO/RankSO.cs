using UnityEngine;
namespace Abstraction
{
    [CreateAssetMenu(fileName = "RankSO", menuName = "SO/RankSO")]
    public class RankSO : ScriptableObject
    {
        public char Rank;
        public Color Color;
    }
}
