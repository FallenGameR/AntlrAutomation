using System;
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
        public void DynamicTest()
        {
            dynamic obj = new DynamicObject();
            Assert.AreEqual("text", obj.SomeText);
        }
    }

    public class DynamicObject : IDynamicMetaObjectProvider
    {
        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new DynamicMeta(parameter, this);
        }
    }

    public class DynamicMeta : DynamicMetaObject
    {
        public DynamicMeta(Expression parameter, DynamicObject value)
            : base(parameter, BindingRestrictions.Empty, value)
        {
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            var methodName = "GetDictionaryEntry";

            return new DynamicMetaObject(
                Expression.Convert(Expression.Constant("text"), typeof(string)),
                BindingRestrictions.GetExpressionRestriction(Expression.Constant(true)));
                    //BindingRestrictions.GetTypeRestriction(this.Expression, this.LimitType));
        }
    }
}
