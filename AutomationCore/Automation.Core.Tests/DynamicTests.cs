﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Core.Tests
{
    [TestClass]
    public class DynamicTests
    {
        [TestMethod]
        public void Test()
        {
            dynamic dict = new DynamicDictionary();

            Console.WriteLine(dict.First.ToString());

            dict.SetDictionaryEntry("test", "me");
            Console.WriteLine(dict.GetDictionaryEntry("test"));
            dict.WriteMethodInfo("method info");
            Console.WriteLine(dict.ToString());
        }
    }

    public class DynamicDictionary : IDynamicMetaObjectProvider
    {
        private Dictionary<string, object> storage = new Dictionary<string, object>();
        private int[] elements = new int[] { 1, 2 };

        public object GetFirst()
        {
            return elements[0];
        }

        public object GetSecond()
        {
            return elements[1];
        }

        public object SetDictionaryEntry(string key, object value)
        {
            if (storage.ContainsKey(key))
                storage[key] = value;
            else
                storage.Add(key, value);
            return value;
        }

        public object GetDictionaryEntry(string key)
        {
            object result = null;
            if (storage.ContainsKey(key))
            {
                result = storage[key];
            }
            return result;
        }

        public object WriteMethodInfo(string methodInfo)
        {
            Console.WriteLine(methodInfo);
            return 42; // because it is the answer to everything
        }

        public override string ToString()
        {
            StringWriter message = new StringWriter();
            foreach (var item in storage)
                message.WriteLine("{0}:\t{1}", item.Key, item.Value);
            return message.ToString();
        }

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new Helper(parameter, this);
        }

        private class Helper : DynamicMetaObject
        {
            public Helper(Expression parameter, DynamicDictionary value)
                : base(parameter, BindingRestrictions.Empty, value)
            {
            }

            public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
            {
                // Method to call in the containing class:
                string methodName = "SetDictionaryEntry";

                // setup the binding restrictions.
                var restrictions = BindingRestrictions.GetTypeRestriction(Expression, LimitType);

                // setup the parameters:
                var args = new Expression[2];
                // First parameter is the name of the property to Set
                args[0] = Expression.Constant(binder.Name);
                // Second parameter is the value
                args[1] = Expression.Convert(value.Expression, typeof(object));

                // Setup the 'this' reference
                var self = Expression.Convert(Expression, LimitType);

                // Setup the method call expression
                var methodCall = Expression.Call(self, typeof(DynamicDictionary).GetMethod(methodName), args);

                // Create a meta object to invoke Set later:
                var setDictionaryEntry = new DynamicMetaObject(methodCall, restrictions);

                Console.WriteLine("Set");

                // return that dynamic object
                return setDictionaryEntry;
            }

            public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
            {
                // Method call in the containing class:
                var methodName = "GetDictionaryEntry";

                // One parameter
                var parameters = new []
                {
                    Expression.Constant(binder.Name)
                };

                var getDictionaryEntry = new DynamicMetaObject(
                    Expression.Call(
                        Expression.Convert(Expression, LimitType),
                        typeof(DynamicDictionary).GetMethod(methodName),
                        parameters),
                    BindingRestrictions.GetTypeRestriction(Expression, LimitType));

                Console.WriteLine("Get");

                return getDictionaryEntry;
            }

            public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
            {
                var paramInfo = new StringBuilder();

                paramInfo.AppendFormat("Calling {0}(", binder.Name);
                foreach (var item in args)
                    paramInfo.AppendFormat("{0}, ", item.Value);
                paramInfo.Append(")");

                var parameters = new []
                {
                    Expression.Constant(paramInfo.ToString())
                };

                var methodInfo = new DynamicMetaObject(
                    Expression.Call(
                    Expression.Convert(Expression, LimitType),
                    typeof(DynamicDictionary).GetMethod("WriteMethodInfo"),
                    parameters),
                    BindingRestrictions.GetTypeRestriction(Expression, LimitType));

                Console.WriteLine("Call");

                return methodInfo;
            }
        }
    }
}
