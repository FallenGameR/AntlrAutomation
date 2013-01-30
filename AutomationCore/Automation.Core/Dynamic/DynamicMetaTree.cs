using System.Dynamic;
using System.Linq.Expressions;

namespace Automation.Core
{
    public class DynamicMetaTree : DynamicMetaObject
    {
        public DynamicMetaTree(Expression parameter, AutomationTree value)
            : base(parameter, BindingRestrictions.Empty, value)
        {
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            // What if "Text" is searched? It is defined by CommonTree already.
            // Do we care about case sensitivity?

            if (binder.Name == "SomeText")
            {
                return new DynamicMetaObject(
                    Expression.Convert(Expression.Constant("text"), typeof(string)),
                    BindingRestrictions.GetExpressionRestriction(Expression.Constant(true)));
                //BindingRestrictions.GetTypeRestriction(this.Expression, this.LimitType));
            }
            else
            {
                return base.BindGetMember(binder);
            }
        }
    }
}
