using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace HearthScript
{
    public class HearthUtil
    {
        public static AssemblyDefinition GetAssemblyDefinition(string fullPath) {
            AssemblyDefinition ad = AssemblyDefinition.ReadAssembly(fullPath);
            if (null == ad) {
                throw new HearthAssemblyException(fullPath.ToString());
            }
            return ad;
        }

        public static void AddMethod(AssemblyDefinition targetAd, MethodDefinition targetMd, MethodDefinition addMd)
        {
            ILProcessor ilProcessor = targetMd.Body.GetILProcessor();
            ilProcessor.InsertBefore(ilProcessor.Body.Instructions[0], ilProcessor.Create(OpCodes.Call, targetAd.MainModule.Import(addMd.Resolve())));
            ilProcessor.InsertBefore(ilProcessor.Body.Instructions[0], ilProcessor.Create(OpCodes.Nop));
        }

        public static MethodDefinition GetMethod(AssemblyDefinition ad, string typeName, string methodName, string namespaceName = null)
        {
            ModuleDefinition md = ad.MainModule;
            if (null == md) {
                throw new HearthModuleException("MainModule");
            }
            TypeDefinition td = null;
            if (string.IsNullOrEmpty(namespaceName))
            {
                foreach (TypeDefinition t in md.Types)
                {
                    if (t.Name.Equals(typeName))
                    {
                        td = t;
                    }
                }
            }
            else
            {
                td = md.GetType(namespaceName, typeName);
            }
            if (null == td) {
                throw new HearthTypeException(typeName.ToString());
            }
            MethodDefinition methodDefinition = null;
            foreach (MethodDefinition m in td.Methods)
            {
                if (m.Name.Equals(methodName))
                {
                    methodDefinition = m;
                    break;
                }
            }
            if (null == methodDefinition) {
                throw new HearthMethodException(methodName.ToString());
            }
            return methodDefinition;
        }

        public static void SaveAssembly(AssemblyDefinition ad, string path) {
            ad.Write(path);
        }
    }
}
