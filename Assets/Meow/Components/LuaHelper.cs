using System;
using System.Collections;
using Meow.AssetLoader;
using Meow.AssetUpdater;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace Meow.Framework
{
    [LuaCallCSharp]
    public class LuaHelper : MonoBehaviour
    {
        private MainLoader _loader;
        private MainUpdater _updater;

        public void Initialize(MainLoader loader, MainUpdater updater)
        {
            _loader = loader;
            _updater = updater;
        }
        
        public void AddClick(GameObject go, LuaFunction callback, LuaTable args)
        {
            Button button = go.GetComponent<Button>();
            Debug.AssertFormat(button != null, "Can't GetComponent [Button] from gameObject [{0}]", go.name);
            button.onClick.AddListener(() =>
            {
                callback.Call(go, args);
            });
        }
        
        public LoadAssetOperation<GameObject> LoadGameObjectAsync(string assetPath, LuaFunction callback, LuaTable args)
        {
            var op = new LoadAssetOperation<GameObject>(_loader, _updater, assetPath);
            StartCoroutine(WaitForYieldDone(op, () =>
            {
                callback.Call(op.Asset, args);
            }));
            return op;
        }
        
        private IEnumerator WaitForYieldDone(CustomYieldInstruction op, Action action)
        {
            yield return op;
            action.Invoke();
        }

    }
}