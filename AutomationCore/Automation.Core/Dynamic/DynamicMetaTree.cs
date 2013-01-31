using System.Dynamic;
using System.Linq.Expressions;
using System.Linq;

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
            if (knownPropertyNames.Contains(binder.Name))
            {
                return base.BindGetMember(binder);
            }



            // What if "Text" is searched? It is defined by CommonTree already.
            // Do we care about case sensitivity?

            return new DynamicMetaObject(
                Expression.Convert(Expression.Constant("text"), typeof(string)),
                BindingRestrictions.Empty);
         
        }
    }
}
