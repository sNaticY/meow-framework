#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class MeowFrameworkLuaBehaviourWrap
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			Utils.BeginObjectRegister(typeof(Meow.Framework.LuaBehaviour), L, translator, 0, 0, 4, 4);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ProjectName", _g_get_ProjectName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LuaPath", _g_get_LuaPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Injections", _g_get_Injections);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ScriptEnv", _g_get_ScriptEnv);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "ProjectName", _s_set_ProjectName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LuaPath", _s_set_LuaPath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Injections", _s_set_Injections);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ScriptEnv", _s_set_ScriptEnv);
            
			Utils.EndObjectRegister(typeof(Meow.Framework.LuaBehaviour), L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(typeof(Meow.Framework.LuaBehaviour), L, __CreateInstance, 1, 0, 0);
			
			
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UnderlyingSystemType", typeof(Meow.Framework.LuaBehaviour));
			
			
			Utils.EndClassRegister(typeof(Meow.Framework.LuaBehaviour), L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			try {
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Meow.Framework.LuaBehaviour __cl_gen_ret = new Meow.Framework.LuaBehaviour();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Meow.Framework.LuaBehaviour constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ProjectName(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Meow.Framework.LuaBehaviour __cl_gen_to_be_invoked = (Meow.Framework.LuaBehaviour)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.ProjectName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaPath(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Meow.Framework.LuaBehaviour __cl_gen_to_be_invoked = (Meow.Framework.LuaBehaviour)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.LuaPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Injections(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Meow.Framework.LuaBehaviour __cl_gen_to_be_invoked = (Meow.Framework.LuaBehaviour)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.Injections);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ScriptEnv(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Meow.Framework.LuaBehaviour __cl_gen_to_be_invoked = (Meow.Framework.LuaBehaviour)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.ScriptEnv);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ProjectName(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Meow.Framework.LuaBehaviour __cl_gen_to_be_invoked = (Meow.Framework.LuaBehaviour)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ProjectName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaPath(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Meow.Framework.LuaBehaviour __cl_gen_to_be_invoked = (Meow.Framework.LuaBehaviour)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LuaPath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Injections(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Meow.Framework.LuaBehaviour __cl_gen_to_be_invoked = (Meow.Framework.LuaBehaviour)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Injections = (Meow.Framework.Injection[])translator.GetObject(L, 2, typeof(Meow.Framework.Injection[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ScriptEnv(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            try {
			
                Meow.Framework.LuaBehaviour __cl_gen_to_be_invoked = (Meow.Framework.LuaBehaviour)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ScriptEnv = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
