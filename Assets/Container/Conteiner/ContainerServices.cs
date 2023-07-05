using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Yurject
{
    public class ContainerServices
    {
        private static Dictionary<string, System.Object> container;
        public static void Init()
        {
            container = new Dictionary<string, System.Object>();
        }
        public static void Register(System.Object t)
        {
            var instance = Resolve(t.GetType());
            if(instance == null) 
            container.Add(t.GetType().Name, t);
        }
        public static void ApplyAttribute(System.Object thisObject, MethodInfo[] methods)
        {
            foreach (var method in methods)
            {
                var injectAttribute = (InjectAttribute)Attribute.GetCustomAttribute(method, typeof(InjectAttribute));
                if (injectAttribute != null)
                {
                    var parameters = method.GetParameters();
                    if (parameters.Length == 1)
                    {
                        var parameterType = parameters[0].ParameterType;

                        var instance = Resolve(parameterType);
                        method.Invoke(thisObject, new[] { instance });
                    }
                }
            }
        }
        private static System.Object Resolve(Type type)
        {
            if (container.ContainsKey(type.ToString()))
            {
                return container[type.ToString()];
            }
            else
            {
                Debug.Log($"No registr type {type}");
                return null;
            }
        }
    }
}
