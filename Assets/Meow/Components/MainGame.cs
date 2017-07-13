using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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
        public Button StartGameButton;

        private MainLoader _loader;
        private MainUpdater _updater;
        private LuaLoader _luaLoader;
        private ResourceLoader _resourceLoader;

        private UpdateOperation _updateOperation;
        private int _totalCount;

        private void Awake()
        {
            _mainGames.Add(ProjectName, this);
            StartGameButton.gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject);
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
            
            
            _resourceLoader = new ResourceLoader(_loader, _updater);
            
            StartGameButton.gameObject.SetActive(true);
            StartGameButton.onClick.AddListener(() =>
            {
                StartCoroutine(_resourceLoader.LoadLevelAsync("Assets/Demo/Scenes/01Main.unity"));
            });
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
