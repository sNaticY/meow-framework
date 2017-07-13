using UnityEngine;

namespace Meow.AssetLoader.Core
{
    public class LoadManifestOperation : CustomYieldInstruction
    {
        private WWW _www;
        
        public LoadManifestOperation(string manifestPath)
        {
            _www = new WWW(manifestPath);
        }

        public AssetBundleManifest Manifest
        {
            get{ return _www.assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest"); }
        }
        
        public override bool keepWaiting
        {
            get
            {
                return !_www.isDone;
            }
        }
    }
}