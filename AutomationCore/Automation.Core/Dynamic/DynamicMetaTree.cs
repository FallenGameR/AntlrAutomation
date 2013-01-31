using System.Dynamic;
using System.Linq.Expressions;
using System.Linq;
using System;

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
            if (knownPropertyNames.Contains(binder.Name))
            {
                return base.BindGetMember(binder);
            }

            var expression = Expression.Constant("text");
            return new DynamicMetaObject(expression, alwaysTrue);
         
        }
    }
}
