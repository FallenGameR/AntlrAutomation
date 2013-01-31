using System.Dynamic;
using System.Linq.Expressions;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Automation.Core
{
    public class DynamicMetaTree : DynamicMetaObject
    {
        private static readonly string[] knownPropertyNames =
            typeof(AutomationTree).GetProperties().Select(p => p.Name).ToArray();

        private static readonly BindingRestrictions alwaysTrue =
            BindingRestrictions.GetExpressionRestriction(Expression.Constant(true));

        public DynamicMetaTree(Expression parameter, AutomationTree value)
            : base(parameter, alwaysTrue, value)
        {
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            if (knownPropertyNames.Contains(binder.Name))
            {
                return base.BindGetMember(binder);
            }

            var result = this.Node.Find(binder.Name);
            var expression = Expression.Constant(result);
            return new DynamicMetaObject(expression, this.Restrictions);
         
        }

        private AutomationTree Node { get { return (AutomationTree)this.Value; } }
    }
}
