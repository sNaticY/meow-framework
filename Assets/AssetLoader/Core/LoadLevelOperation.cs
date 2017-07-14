using UnityEngine;
using UnityEngine.SceneManagement;

namespace Meow.AssetLoader.Core
{
    public class LoadLevelOperation : CustomYieldInstruction
    {
        private LoadBundleOperation _loadBundleOperation;
        private AsyncOperation _loadLevelOperation;

        private readonly string _assetbundlePath;
        private readonly MainLoader _loader;
        private readonly string _levelName;
        private readonly bool _isAddtive;

        public bool IsDone { get; private set; }

        public LoadLevelOperation(MainLoader loader, string assetbundlePath, string levelName, bool isAdditive)
        {
            _loader = loader;
            _levelName = levelName;
            _isAddtive = isAdditive;
            _assetbundlePath = assetbundlePath;
            IsDone = false;
        }

        public override bool keepWaiting
        {
            get
            {
                if (_loadBundleOperation == null)
                {
                    _loadBundleOperation = new LoadBundleOperation(_loader, _assetbundlePath);
                    _loader.StartCoroutine(_loadBundleOperation);
                }
                if (_loadBundleOperation.IsDone)
                {
                    if (_loadLevelOperation == null)
                    {
#if UNITY_EDITOR
                        if (MainLoader.IsSimulationMode)
                        {
                            if (_isAddtive)
                            {
                                _loadLevelOperation = UnityEditor.EditorApplication.LoadLevelAdditiveAsyncInPlayMode(_levelName);
                            }
                            else
                            {
                                _loadLevelOperation = UnityEditor.EditorApplication.LoadLevelAsyncInPlayMode(_levelName);
                            }
                        }
                        else
#endif
                        {
                            _loadLevelOperation =
                                SceneManager.LoadSceneAsync(_levelName, _isAddtive ? LoadSceneMode.Additive : LoadSceneMode.Single);
                        }
                    }
                    IsDone = _loadLevelOperation.isDone;
                }
                return !IsDone;
            }
        }
    }
}