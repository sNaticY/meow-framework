using System.Collections;
using System.Collections.Generic;
using Meow.AssetLoader;
using Meow.AssetUpdater;
using UnityEngine;
using UnityEngine.UI;

namespace Meow.Framework
{
    public class MainGame : MonoBehaviour
    {
        private static readonly Dictionary<string, MainGame> _mainGames = new Dictionary<string, MainGame>();
    
        public string ProjectName;
        public string RemoteUrl;
        public string VersionFileName;
        public string LuaBundleName;
        public string LuaScriptRootFolderName;

        public Text SingleSizeText;
        public Slider SingleSlider;
        public Text TotalSizeText;
        public Slider TotalSlider;
        public Text AssetBundleCountText;

        private MainLoader _loader;
        private MainUpdater _updater;
        private LuaLoader _luaLoader;

        private UpdateOperation _updateOperation;
        private int _totalCount;

        private void Awake()
        {
            _mainGames.Add(ProjectName, this);
        }

        private IEnumerator Start()
        {
            _updater = gameObject.GetComponent<MainUpdater>();
            if (_updater == null)
            {
                _updater = gameObject.AddComponent<MainUpdater>();
            }
            
            _loader = gameObject.GetComponent<MainLoader>();
            if (_loader == null)
            {
                _loader = gameObject.AddComponent<MainLoader>();
            }
            
            _luaLoader = new LuaLoader();
            
            yield return _updater.LoadAllVersionFiles();
		
            yield return _updater.UpdateFromStreamingAsset();
            
            _updater.Initialize(RemoteUrl, ProjectName, VersionFileName);
            
            _updateOperation = _updater.UpdateFromRemoteAsset();
            _totalCount = _updateOperation.RemainBundleCount;
            yield return _updateOperation;
            
            yield return _loader.Initialize(_updater.GetAssetbundleRootPath(true), _updater.GetManifestName());
            
            yield return _luaLoader.Initialize(_loader, LuaBundleName, LuaScriptRootFolderName);
        }

        private void Update()
        {
            _luaLoader.UpdateGCTick();
            
            if (_updateOperation != null)
            {
                SingleSizeText.text = _updateOperation.SingleDownloadedSize + " / " + _updateOperation.SingleSize;
                SingleSlider.value = _updateOperation.SingleProgress;
                TotalSizeText.text = _updateOperation.TotalownloadedSize + " / " + _updateOperation.TotalSize;
                TotalSlider.value = _updateOperation.TotalProgress;
                AssetBundleCountText.text = ( _totalCount - _updateOperation.RemainBundleCount ) + " / " + _totalCount;
            }
        }

        public static object[] DoLuaString(string projectName, string luaFileName, string chunkName)
        {
            var mainGame = _mainGames[projectName];
            var luaLoader = mainGame._luaLoader;
            var luaString = luaLoader.GetLuaScriptString(luaFileName);
            var result = luaLoader.LuaEnv.DoString(luaString, chunkName);
            return result;
        }
    }

}
