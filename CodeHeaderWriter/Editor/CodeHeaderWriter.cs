using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[ExecuteInEditMode]
public class CodeHeaderWriter {

    public static string DefaultHeader =
@"
// Copyright (c) [YEAR], [AUTHOR]
// All rights reserved
//
//
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
";
    //Return a list of all cs files in the specified directory
    public static string[] GetScriptNamesInDirectory(string directoryPath, bool includeSubdirectories = true)
    {
        // create new list of file paths
        List<string> filePathList = new List<string>();
        
        if(Directory.Exists(directoryPath))
        {
            // iterate through files and add them to the list
            string[] allFiles = Directory.GetFiles(directoryPath);
            for(int i = 0; i < allFiles.Length; i += 1)
            {
                if(allFiles[i].EndsWith(".cs"))
                {
                    filePathList.Add(allFiles[i]);
                }
            }

            // if include sub directories is true, recursively include all files from subdirectories
            if(includeSubdirectories)
            {
                string[] subDirectories = Directory.GetDirectories(directoryPath);

                for(int i = 0; i < subDirectories.Length; i += 1)
                {
                    string[] subDirectoryFiles = GetScriptNamesInDirectory(subDirectories[i]);
                    if(subDirectoryFiles != null && subDirectoryFiles.Length > 0)
                    {
                        filePathList.AddRange(subDirectoryFiles);
                    }
                }
            }
        }

        return filePathList.ToArray();    
    }

    public static void UpdateAllScriptsInDirectory(string header, string directoryPath, bool includeSubdirectories = true)
    {
        string[] filePaths = GetScriptNamesInDirectory(directoryPath, includeSubdirectories);
        foreach (string filePath in filePaths)
        {
            UpdateFileHeader(filePath, header);
        }
    }

    /// <summary>
    /// Returns true if file was modified, and false if it was not
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="header"></param>
    /// <returns></returns>
    public static bool UpdateFileHeader(string filePath, string header)
    {
        string fileContents = File.ReadAllText(filePath);

        if(fileContents.Contains(header))
        {
            return false;
        }

        File.WriteAllText(filePath, header + "\n" + fileContents);
        return true;
        
    }
    
    public static bool DoesFileHaveHeader(string filePath, string header)
    {
        string fileContents = File.ReadAllText(filePath);

        return fileContents.Contains(header);
        
    }

    
}
