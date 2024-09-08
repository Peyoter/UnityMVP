using PresentationModel.Scripts.Fabric;
using PresentationModel.Scripts.Model;
using PresentationModel.Scripts.Presenter;
using PresentationModel.Scripts.View;
using UnityEngine;
using Zenject;
using CharacterInfo = PresentationModel.Scripts.Model.CharacterInfo;

namespace PresentationModel.Scripts
{
    public class PopupInstaller : MonoInstaller
    {
        [SerializeField] private PopupView PopupView;
        [SerializeField] private StatBlockView StatBlockPref;
        [SerializeField] private StatWrapper StatWrapper;
        public override void InstallBindings()
        {
            
            Container.Bind<UserInfoModel>().AsSingle();
            Container.Bind<CharacterInfo>().AsSingle();
            Container.Bind<PlayerLevel>().AsSingle();
            Container.Bind<StatBlockFabric>().AsSingle();
            
            // View
            Container.Bind<PopupView>().FromInstance(PopupView).AsSingle();
            Container.Bind<StatWrapper>().FromInstance(StatWrapper).AsSingle();
            Container.Bind<StatBlockView>().FromInstance(StatBlockPref).AsSingle();
            
            //Presenter
            Container.Bind<PopupPresenter>().AsSingle();
            Container.Bind<PopupFabric>().AsSingle();
        }
    }
}