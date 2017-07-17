using UnityEngine;

namespace Meow.AssetLoader.Core
{
    public class LoadManifestOperation : CustomYieldInstruction
    {
        private string _manifestPath;
        private WWW _www;
        
        public bool IsDone { get; private set; }
        
        public LoadManifestOperation(string manifestPath)
        {
            _manifestPath = manifestPath;
        }

        public AssetBundleManifest Manifest
        {
            get{ return _www.assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest"); }
        }
        
        public override bool keepWaiting
        {
            get
            {
                if (_www == null)
                {
                    _www = new WWW(_manifestPath);
                }
                IsDone = _www.isDone;
                return !IsDone;
            }
        }
    }
}