using System.Dynamic;
using System.Linq.Expressions;

namespace Automation.Core
{
    public class DynamicMeta : DynamicMetaObject
    {
        public DynamicMeta(Expression parameter, object value)
            : base(parameter, BindingRestrictions.Empty, value)
        {
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
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
