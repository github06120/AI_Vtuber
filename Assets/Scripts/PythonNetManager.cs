using Python.Runtime;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

namespace UnityPython
{
    public static class PythonNetManager
    {
        private const string pythonFolder = "python-3.12.3-embed-amd64";
        private const string pythonDll = "python312.dll";
        private const string pythonZip = "python312.zip";
        private const string pythonProject = "MyProject";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void PythonInitialize()
        {
            Application.quitting += PythonShutdown;
            Initialize(pythonProject);
        }

        private static void PythonShutdown()
        {
            Application.quitting -= PythonShutdown;
            Shutdown();
        }

        public static void Initialize(string appendPythonPath = "")
        {
            var pythonHome = $"{Application.streamingAssetsPath}/{pythonFolder}";
            var appendPath = string.IsNullOrWhiteSpace(appendPythonPath) ? string.Empty : $"{Application.streamingAssetsPath}/{appendPythonPath}";
            var pythonPath = string.Join(";",
                $"{appendPath}",
                $"{pythonHome}/Lib/site-packages",
                $"{pythonHome}/{pythonZip}",
                $"{pythonHome}"
            );

            var scripts = $"{pythonHome}/Scripts";

            var path = Environment.GetEnvironmentVariable("PATH")?.TrimEnd(';');
            path = string.IsNullOrEmpty(path) ? $"{pythonHome};{scripts}" : $"{pythonHome};{scripts};{path}";
            Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("DYLD_LIBRARY_PATH", $"{pythonHome}/Lib", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", $"{pythonHome}/{pythonDll}", EnvironmentVariableTarget.Process);

#if UNITY_EDITOR
            Environment.SetEnvironmentVariable("PYTHONDONTWRITEBYTECODE", "1", EnvironmentVariableTarget.Process);
#endif

            PythonEngine.PythonHome = pythonHome;
            PythonEngine.PythonPath = pythonPath;

            PythonEngine.Initialize();
        }

        public static void Shutdown()
        {
            PythonEngine.Shutdown();
        }
    }
}

