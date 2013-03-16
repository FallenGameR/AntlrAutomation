using System;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace Automation.Core
{
    public class DynamicMetaTree : DynamicMetaObject
    {
        private static readonly string[] knownPropertyNames =
            typeof(AutomationTree).GetProperties().Select(p => p.Name).ToArray();

        public DynamicMetaTree(Expression parameter, AutomationTree value)
            : base(parameter, BindingRestrictions.Empty, value)
        {
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            // Case insensitive match with unmodified binder
            // For C# that would resolve to property only if the case is correct
            // For Powershell that would would resolve to property on any case
            if (knownPropertyNames.Contains(binder.Name, StringComparer.OrdinalIgnoreCase))
            {
                return base.BindGetMember(binder);
            }

            // Dynamic child lookup
            var self = this.Expression;
            var expression = 
                Expression.Call(
                    Expression.Convert(self, typeof(AutomationTree)),
                    typeof(AutomationTree).GetMethod("Find"),
                    Expression.Constant(binder.Name));
            var restriction =
                BindingRestrictions.GetInstanceRestriction(
                    this.Expression,
                    this.Node);
            return new DynamicMetaObject(expression, restriction);
        }

        private AutomationTree Node { get { return (AutomationTree)this.Value; } }
    }
}
