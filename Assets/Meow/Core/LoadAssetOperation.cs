using System;
using Meow.AssetLoader;
using Meow.AssetLoader.Core;
using Meow.AssetUpdater;
using UnityEngine;

namespace Meow.Framework
{
    public class LoadAssetOperation<T> : CustomYieldInstruction where T : UnityEngine.Object
    {
        private LoadBundleOperation _op;

        private readonly MainLoader _loader;
        private readonly MainUpdater _updater;
        private readonly string _assetPath;
        
        public bool IsDone { get; private set; }
        public T Asset { get; private set; }

        public LoadAssetOperation(MainLoader loader, MainUpdater updater, string assetPath)
        {
            _loader = loader;
            _updater = updater;
            _assetPath = assetPath;
        }

        public override bool keepWaiting
        {
            get
            {
                if (_op == null)
                {
                    var bundleName = _updater.GetAssetbundlePathByAssetPath(_assetPath);
                    _op = _loader.GetLoadBundleOperation(bundleName);
                    _loader.StartCoroutine(_op);
                    
                }
                if (_op.IsDone)
                {
                    Asset = _op.GetAsset<T>(_assetPath);
                    IsDone = true;
                }
                return !IsDone;
            }
        }
    }
}