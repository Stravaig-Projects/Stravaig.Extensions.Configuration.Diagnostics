using System;
using System.Dynamic;
using System.Reflection;
using MemberTypes = System.Reflection.MemberTypes;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.__helpers
{
    public class Jailbreak<T> : DynamicObject
    {
        private const MemberTypes IsPropertyOrField = MemberTypes.Property | MemberTypes.Field;
        private const BindingFlags AllAccessModifiers = BindingFlags.Public | BindingFlags.NonPublic;
        
        private readonly T _object;
        private readonly Type _type;

        public Jailbreak(T obj)
        {
            _type = typeof(T);
            _object = obj;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            MemberInfo[] members = _type.GetMember(binder.Name, IsPropertyOrField, AllAccessModifiers | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.GetProperty);
            if (members.Length == 0)
            {
                result = null;                
                return false;
            }

            if (members.Length > 1)
            {
                throw new AmbiguousMatchException($"Found {members.Length} matches for {binder.Name}.");
            }

            var member = members[0];
            if (member is FieldInfo field)
            {
                result = field.GetValue(_object);
                return true;
            }
            
            if (member is PropertyInfo property)
            {
                var method = property.GetMethod;
                if (method == null)
                {
                    result = null;
                    return false;
                }

                result = method.Invoke(_object, Array.Empty<object>());
                return true;
            }
            
            throw new InvalidOperationException($"{member} is not a property or field.");
        }
    }
}