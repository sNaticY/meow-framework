using UnityEngine;

namespace Meow.AssetLoader.Core
{
    public class LoadedBundle
    {
        public string AssetBundlePath;
        public AssetBundle AssetBundle;
        public int ReferecedCount;

        public LoadedBundle(string assetBundlePath, AssetBundle assetBundle)
        {
            AssetBundlePath = assetBundlePath;
            AssetBundle = assetBundle;
            ReferecedCount = 1;
        }
    }
}