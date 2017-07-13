using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Meow.AssetLoader.Core;

namespace Meow.AssetLoader
{
    public class MainLoader : MonoBehaviour
    {
        public string AssetbundleRootPath;

        public AssetBundleManifest Manifest;
        
        public readonly Dictionary<string, LoadedBundle> LoadedBundles = new Dictionary<string, LoadedBundle>();
        
        
#if UNITY_EDITOR
        private static int _isSimulationMode = -1;
        
        public static bool IsSimulationMode
        {
            get
            {
                if (_isSimulationMode == -1)
                    _isSimulationMode = UnityEditor.EditorPrefs.GetBool("AssetLoaderSimulationMode", true) ? 1 : 0;

                return _isSimulationMode != 0;
            }
            set
            {
                int newValue = value ? 1 : 0;
                if (newValue != _isSimulationMode)
                {
                    _isSimulationMode = newValue;
                    UnityEditor.EditorPrefs.SetBool("AssetLoaderSimulationMode", value);
                }
            }
        }
#endif
        
        public IEnumerator Initialize(string assetbundleRootPath, string manifeestName)
        {
#if UNITY_EDITOR
            if (!IsSimulationMode)
#endif
            {
                AssetbundleRootPath = assetbundleRootPath;
                var manifestBundle = new LoadManifestOperation(Path.Combine(assetbundleRootPath, manifeestName));
                yield return manifestBundle;
                Manifest = manifestBundle.Manifest;
            }
        }

        public LoadBundleOperation LoadBundle(string bundlePath)
        {
            return new LoadBundleOperation(this, bundlePath);
        }

        public LoadLevelOperation LoadLevel(string bundlePath, string levelName, bool isAddtive)
        {
            return new LoadLevelOperation(this, bundlePath, levelName, isAddtive);
        }
    }
}