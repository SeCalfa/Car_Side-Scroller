using CodeBase.Logic.Buttons;
using CodeBase.Logic.Presenters;
using UnityEngine;

namespace CodeBase.Logic.Panels
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField]
        private LongButton gas;
        [SerializeField]
        private LongButton brake;
        [SerializeField]
        private LongButton left;
        [SerializeField]
        private LongButton right;

        public LongButton GetGas { get => gas; }
        public LongButton GetBrake { get => brake; }
        public LongButton GetLeft { get => left; }
        public LongButton GetRight { get => right; }
    }
}