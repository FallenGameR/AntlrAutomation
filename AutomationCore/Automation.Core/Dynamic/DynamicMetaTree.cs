using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

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
            // What about powershell?
            if (knownPropertyNames.Contains(binder.Name))
            {
                return base.BindGetMember(binder);
            }

            var result = this.Node.Find(binder.Name);
            var expression = Expression.Constant(result);
            return new DynamicMetaObject(expression, alwaysTrue);
        }

        private AutomationTree Node { get { return (AutomationTree)this.Value; } }
    }
}
