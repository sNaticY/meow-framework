using Meow.AssetLoader;
using Meow.AssetLoader.Core;
using Meow.AssetUpdater;
using UnityEngine;

namespace Meow.Framework
{
    public class ResourceLoader
    {
        private readonly MainLoader _loader;
        private readonly MainUpdater _updater;
        
        public ResourceLoader(MainLoader loader, MainUpdater updater)
        {
            _loader = loader;
            _updater = updater;
        }

        public LoadLevelOperation LoadLevelAsync(string levelPath, bool isAdditive = false)
        {
            var bundlePath = _updater.GetAssetbundlePathByAssetPath(levelPath);
            var op = _loader.GetLoadLevelOperation(bundlePath, levelPath, isAdditive);
            _loader.StartCoroutine(op);
            return op;
        }

        public LoadAssetOperation<GameObject> LoadGameObject(string assetPath)
        {
            var op = new LoadAssetOperation<GameObject>(_loader, _updater, assetPath);
            _loader.StartCoroutine(op);
            return op;
        }
    }
}