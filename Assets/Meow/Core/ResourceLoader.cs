using Meow.AssetLoader;
using Meow.AssetLoader.Core;
using Meow.AssetUpdater;

namespace Meow.Framework
{
    public class ResourceLoader
    {
        private MainLoader _loader;
        private MainUpdater _updater;
        
        public ResourceLoader(MainLoader loader, MainUpdater updater)
        {
            _loader = loader;
            _updater = updater;
        }

        public LoadLevelOperation LoadLevelAsync(string levelPath, bool isAdditive = false)
        {
            var bundleName = _updater.GetAssetbundleNameByAssetPath(levelPath.ToLower());
            return _loader.LoadLevel(bundleName, levelPath, isAdditive);
        }
    }
}