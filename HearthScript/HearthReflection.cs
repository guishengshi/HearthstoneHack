using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HearthScript
{
    public class HearthReflection
    {
        // 获取公有方法  
        public static object InvokePublicMethod(string classFullName, string methodName, params object[] paras)
        {
            Type type = Type.GetType(classFullName);
            object obj = Activator.CreateInstance(type);
            MethodInfo methodInfo = type.GetMethod(methodName);
            return methodInfo.Invoke(obj, paras);
        }

        public static object InvokePublicMethod(object targetObject, string methodName, params object[] paras) {
            MethodInfo methodInfo = targetObject.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return methodInfo.Invoke(targetObject, paras);
        }

        // 获取字段属性  
        public static object GetProperty(string classFullName, string propertyName)
        {
            Type type = Type.GetType(classFullName);
            object obj = Activator.CreateInstance(type);
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            return propertyInfo.GetValue(obj);
        }


        // 获取公有变量或静态变量  
        public static object GetPublicVariable(string classFullName, string variableName)
        {
            Type type = Type.GetType(classFullName);
            object obj = Activator.CreateInstance(type);
            FieldInfo variableInfo = type.GetField(variableName, BindingFlags.Public | BindingFlags.Static);
            return variableInfo.GetValue(obj);
        }
        // 获取私有变量或实例变量  
        public static object GetPrivateVariable(string classFullName, string variableName)
        {
            Type type = Type.GetType(classFullName);
            object obj = Activator.CreateInstance(type);
            FieldInfo variableInfo = type.GetField(variableName, BindingFlags.Instance | BindingFlags.NonPublic);
            return variableInfo.GetValue(obj);
        }
        // 获取类实例  
        public static object GetClassInstance(string classFullName)
        {
            Type type = Type.GetType(classFullName);
            object obj = Activator.CreateInstance(type);
            return obj;
        }
        //获取委托，委托类型在编译后作为类的嵌套类型，所以调用GetNestedType方法  
        public static object GetDelegate(string classFullName, string delegateName, string methodName, params object[] paras)
        {
            Type type = Type.GetType(classFullName);
            object obj = Activator.CreateInstance(type);
            Type delegateInfo = type.GetNestedType(delegateName);
            MethodInfo methodInfo = type.GetMethod(methodName);
            //注意调用CreateDelegate方法时若方法是实例方法则需调用CreateDelegate重载的带实例对象参数的方法！  
            Delegate delegateInstance = Delegate.CreateDelegate(delegateInfo, obj, methodInfo);
            return delegateInstance.DynamicInvoke(paras);
        }
        //获取事件，注意获取事件需要设置绑定参数为实例和非public，此处我也不清楚事件为什么作为非公有参数。。。  
        public static object GetEvent(string classFullName, string eventName, Delegate deles, params object[] paras)
        {
            Type type = Type.GetType(classFullName);
            object obj = Activator.CreateInstance(type);
            EventInfo eventInfo = type.GetEvent(eventName);
            eventInfo.AddEventHandler(obj, deles);
            MulticastDelegate multicasDelegate = (MulticastDelegate)type.GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
            foreach (Delegate dele in multicasDelegate.GetInvocationList())
            {
                Console.WriteLine(dele.Method.Name);
                object c = dele.DynamicInvoke(paras);
                Console.WriteLine("{0}", c);
            }
            Console.WriteLine(eventInfo.RaiseMethod);
            return multicasDelegate;
        }  
    }
}
