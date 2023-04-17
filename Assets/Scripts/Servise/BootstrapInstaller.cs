using GameStateMachine;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private StaticData _staticData;
    [SerializeField] private GameStateSwitcher _gameStateMachine;
    [SerializeField] private Plane _plane;
    [SerializeField] private PointsCounter _winCounter;
    [SerializeField] private AudioServise _audioServise;
    [SerializeField] private CreditPanel _creditPanel;

    public override void InstallBindings()
    {
        Container.Bind(typeof(PointsCounter)).FromComponentOn(_winCounter.gameObject).AsSingle();
        Container.Bind(typeof(Plane)).FromComponentOn(_plane.gameObject).AsSingle();
        Container.Bind(typeof(StaticData)).FromComponentOn(_staticData.gameObject).AsSingle();
        Container.Bind(typeof(GameStateSwitcher)).FromComponentOn(_gameStateMachine.gameObject).AsSingle();
        Container.Bind<AudioServise>().FromComponentOn(_audioServise.gameObject).AsSingle();
        Container.Bind<CreditPanel>().FromComponentOn(_creditPanel.gameObject).AsSingle();
    }
}