using System;
using System.Reflection;
using System.Collections.Generic;
using Abox.Core.Attributes;

namespace Abox.Lambda.Services
{
    public class SerializationService
    {
        private readonly Dictionary<string, Type> types = new Dictionary<string, Type>();

        public void Register(string key, Type type)
        {
            if(key != "*")
                types[key] = type;
        }

        public void Register<TMessage>(string key)
        {
            Register(key, typeof(TMessage));
        }

        public void Register<TMessage>()
        {
            var attributes = typeof(TMessage)
                .GetTypeInfo()
                .GetCustomAttributes<Message>();

            foreach(var attribute in attributes)
            {
                Register<TMessage>(attribute.Name);
            }
        }

        public Type Resolve(string key)
        {
            if(!types.ContainsKey(key))
                throw new Exception($"unknown message key '{key}'");

            return types[key];
        }
    }
}