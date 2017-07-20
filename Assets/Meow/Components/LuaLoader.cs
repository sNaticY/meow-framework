using System.Collections;
using System.Collections.Generic;
using XLua;
using Meow.AssetLoader;
using UnityEngine;

namespace Meow.Framework
{
    public class LuaLoader : MonoBehaviour
    {
        private const float _gcInternal = 1f;
        private float _lastGCTime = 0;
        
        public LuaEnv LuaEnv { get; private set; }
        private Dictionary<string, TextAsset> LuaScripts { get; set; }

        public IEnumerator Initialize(string projectName, MainLoader _loader, string luaScriptBundlePath, string luaScriptsRootPath)
        {
            LuaEnv = new LuaEnv();
            LuaScripts = new Dictionary<string, TextAsset>();
            var operation = _loader.GetLoadBundleOperation(luaScriptBundlePath);
            yield return operation;

            var assets = operation.GetAllAssets<TextAsset>();
            foreach (var luaAssets in assets)
            {
                var firstIndex = luaAssets.Key.LastIndexOf(luaScriptsRootPath.ToLower()) + luaScriptsRootPath.Length;
                var luaPath = luaAssets.Key.Substring(firstIndex + 1, luaAssets.Key.Length - firstIndex - 1 - 8);
                Debug.AssertFormat(!LuaScripts.ContainsKey(luaPath), "lua script file name [{0}] is duplicated", luaPath);
                LuaScripts.Add(luaPath, luaAssets.Value);
            }

            LuaEnv.AddLoader(
                (ref string filepath) =>
                {
                    var filePath = filepath.ToLower();
                    Debug.AssertFormat(LuaScripts.ContainsKey(filePath), "lua script [{0}] not exist", filePath);
                    return LuaScripts[filePath].bytes;
                });
            
            LuaFunction mainLuaFunction = LuaEnv.DoString(LuaScripts["main"].bytes)[0] as LuaFunction;
            mainLuaFunction.Call(projectName);
        }

        public object[] DoString(string luaFileName, string chunkName)
        {
            var luaString = GetLuaScriptString(luaFileName);
            var result = LuaEnv.DoString(luaString, chunkName);
            return result;
        }
        
        private string GetLuaScriptString(string luaPath)
        {
            Debug.AssertFormat(LuaScripts.ContainsKey(luaPath.ToLower()), "Can't find lua file [{0}]", luaPath);
            return LuaScripts[luaPath.ToLower()].text;
        }
        
        private void Update()
        {
            if (LuaEnv != null && Time.time - _lastGCTime > _gcInternal)
            {
                LuaEnv.Tick();
                _lastGCTime = Time.time;
            }
        }
    }
}