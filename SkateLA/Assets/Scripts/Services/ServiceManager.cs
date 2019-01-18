using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : IServiceManager
{
  private static IServiceManager instance;
  public static IServiceManager Singleton
  {
    get
    {
      if (instance == null)
      {
        instance = new ServiceManager();
        InitializeCoreServices();
      }
      return instance;
    }
  }

  private static Dictionary<System.Type, object> serviceDict = new Dictionary<System.Type, object>();

  private static void InitializeCoreServices()
  {

#if UNITY_EDITOR
    serviceDict.Add(typeof(IInputSystem), new DebugInputSystem());
    serviceDict.Add(typeof(SceneLoader), new PCSceneLoader());
#elif UNITY_STANDALONE
    serviceDict.Add(typeof(IInputSystem), new PS4InputSystem());
    serviceDict.Add(typeof(SceneLoader), new PCSceneLoader());
#elif UNITY_ANDROID
    serviceDict.Add(typeof(IInputSystem), new MobileInputSystem());
    serviceDict.Add(typeof(SceneLoader), new AndroidSceneLoader());
#endif

  }

  public bool RequestService<T>(out T service) where T : class
  {
    try
    {
      service = (T) serviceDict[typeof(T)];
      return true;
    }
    catch
    {
      service = null;
      return false;
    }
  }
}