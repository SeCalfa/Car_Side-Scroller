using CodeBase.Logic.Buttons;
using CodeBase.Logic.Presenters;
using UnityEngine;

namespace CodeBase.Logic.Panels
{
    public class StatsPanel : MonoBehaviour
    {
        [SerializeField]
        private SimpleButton pause;
        [Space]
        [SerializeField]
        private ScorePresenter scorePresenter;
        [SerializeField]
        private ImageBarPresenter healthPresenter;
        [SerializeField]
        private ImageBarPresenter magnetPresenter;
        [SerializeField]
        private ImageBarPresenter shieldPresenter;
        [SerializeField]
        private ImageBarPresenter nitroPresenter;

        public SimpleButton GetPauseButton { get => pause; }
        public ScorePresenter GetScorePresenter { get => scorePresenter; }
        public ImageBarPresenter GetHealthPresenter { get => healthPresenter; }
        public ImageBarPresenter GetMagnetPresenter { get => magnetPresenter; }
        public ImageBarPresenter GetShieldPresenter { get => shieldPresenter; }
        public ImageBarPresenter GetNitroPresenter { get => nitroPresenter; }
    }
}