using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.IO;

namespace HearthScript
{
    class Program
    {
        private static string mManagePath = @"H:\Hearthstone\Hearthstone_Data\Managed\";      //炉石Manage路径
        static void Main(string[] args)
       {
            File.Copy("HearthScript.exe", mManagePath + "HearthScript.exe", true);
            try
            {
                AssemblyDefinition targetAssembly = HearthUtil.GetAssemblyDefinition(mManagePath + "Assembly-CSharp.dll");
                MethodDefinition targetMethod = HearthUtil.GetMethod(targetAssembly, "SceneMgr", "Start");
                AssemblyDefinition addAssembly = HearthUtil.GetAssemblyDefinition(mManagePath + "HearthScript.exe");
                MethodDefinition addMethod = HearthUtil.GetMethod(addAssembly, "HearthUnity", "Invade");
                HearthUtil.AddMethod(targetAssembly, targetMethod, addMethod);
                HearthUtil.SaveAssembly(targetAssembly, mManagePath + "Assembly-CSharp.dll");
                Console.WriteLine("Patch Success!");
            } catch (HearthException e) {
                e.LogError();
            }
            Console.ReadLine();
        }
    }
}
