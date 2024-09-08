using PresentationModel.Scripts.Model;
using PresentationModel.Scripts.Presenter;
using PresentationModel.Scripts.View;
using Zenject;
using CharacterInfo = PresentationModel.Scripts.Model.CharacterInfo;

namespace PresentationModel.Scripts.Fabric
{
    public class StatBlockFabric
    {
        private DiContainer _diContainer;
        private StatBlockView _statBlockView;
        private readonly StatWrapper _statWrapper;

        [Inject]
        public StatBlockFabric(DiContainer diContainer, StatBlockView statBlockView, StatWrapper statWrapper)
        {
            _diContainer = diContainer;
            _statBlockView = statBlockView;
            _statWrapper = statWrapper;
        }

        public void Create(CharacterStat statModel, CharacterInfo characterInfo)
        {
            var statPresenter = new StatPresenter(statModel, characterInfo);   
            var pref = _diContainer.InstantiatePrefab(_statBlockView.gameObject, _statWrapper.transform);
            var statView = pref.GetComponent<StatBlockView>();
            statView.Init(statPresenter);
        }
    }
}