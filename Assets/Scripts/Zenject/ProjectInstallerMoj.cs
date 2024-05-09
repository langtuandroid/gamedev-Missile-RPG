using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject;

public class ProjectInstallerMoj : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AddressBookController>().FromNewComponentOnNewGameObject().WithGameObjectName("AddressBookController").AsSingle().NonLazy();
        Container.Bind<AndroidCamera>().FromNewComponentOnNewGameObject().WithGameObjectName("AndroidCamera").AsSingle().NonLazy();
        /*        Container.Bind<Wallet>().FromComponentInNewPrefab(_walletPrefab).AsSingle().NonLazy();
                Container.Bind<PlayerData>().FromComponentInNewPrefab(_playerDataPrefab).AsSingle().NonLazy();*/
    }
}
