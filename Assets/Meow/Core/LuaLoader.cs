using System.Collections;
using System.Collections.Generic;
using XLua;
using Meow.AssetLoader;
using UnityEngine;
using UnityEngine.Assertions;

namespace Meow.Framework
{
    public class LuaLoader
    {
        private const float _gcInternal = 1f;
        private float _lastGCTime = 0;
        
        public LuaEnv LuaEnv { get; private set; }
        private Dictionary<string, TextAsset> LuaScripts { get; set; }

        public IEnumerator Initialize(MainLoader _loader, string luaScriptBundlePath, string luaScriptsRootPath)
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
                LuaScripts.Add(luaPath, luaAssets.Value);
            }

            LuaEnv.AddLoader((ref string filepath) => LuaScripts[filepath.ToLower()].bytes);
            
            LuaEnv.DoString(LuaScripts["main"].bytes);
        }
        
        public string GetLuaScriptString(string luaPath)
        {
            Debug.AssertFormat(LuaScripts.ContainsKey(luaPath.ToLower()), "Can't find lua file [{0}]", luaPath);
            return LuaScripts[luaPath.ToLower()].text;
        }

        public object[] DoString(string luaFileName, string chunkName)
        {
            var luaString = GetLuaScriptString(luaFileName);
            var result = LuaEnv.DoString(luaString, chunkName);
            return result;
        }

        public void UpdateGCTick()
        {
            if (Time.time - _lastGCTime > _gcInternal)
            {
                LuaEnv.Tick();
                _lastGCTime = Time.time;
            }
        }
    }
}