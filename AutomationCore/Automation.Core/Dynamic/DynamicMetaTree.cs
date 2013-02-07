using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace Automation.Core
{
    public class DynamicMetaTree : DynamicMetaObject
    {
        private static readonly string[] knownPropertyNames =
            typeof(AutomationTree).GetProperties().Select(p => p.Name).ToArray();

        private static readonly BindingRestrictions alwaysTrue =
            BindingRestrictions.GetExpressionRestriction(Expression.Constant(true));

        public DynamicMetaTree(Expression parameter, AutomationTree value)
            : base(parameter, BindingRestrictions.Empty, value)
        {
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            // Case sensitive match
            if (knownPropertyNames.Contains(binder.Name))
            {
                return base.BindGetMember(binder);
            }

            // Case insensitive match
            var matchingProperty = knownPropertyNames.FirstOrDefault(name => 
                StringComparer.OrdinalIgnoreCase.Compare(name, binder.Name) == 0);

            if (matchingProperty != null)
            {
                var result1 = this.Node.GetType().GetProperty(matchingProperty).GetValue(this.Node, new object[0]);
                var expression1 = Expression.Constant(result1);
                return new DynamicMetaObject(expression1, alwaysTrue);
            }

            // Dynamic lookup
            var result = this.Node.Find(binder.Name);
            var expression = Expression.Constant(result);
            return new DynamicMetaObject(expression, alwaysTrue);
        }

        private AutomationTree Node { get { return (AutomationTree)this.Value; } }

    }
}
