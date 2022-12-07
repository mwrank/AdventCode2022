using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public static class Day7
    {
        /// <summary>
        /// Read the input file
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Tree of file structure</returns>
        private static List<DeviceFolder> ReadInputFile(string filePath)
        {
            DeviceFolder currentDirectory = new DeviceFolder() { Name = "/" };
            DeviceFolder rootDirectory = currentDirectory;
            List<DeviceFolder> allDirectories = new List<DeviceFolder>() { currentDirectory };
            DeviceCommand lastCommand = DeviceCommand.None;

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                if (line.StartsWith("$")) // it's a command
                {
                    string[] parts = line.Split(" ");
                    string cmd = parts[1];

                    // is it a change directory or list command
                    lastCommand = cmd == "cd" ? DeviceCommand.CD : DeviceCommand.LS;

                    if (lastCommand == DeviceCommand.CD) // if change dir
                    {
                        string param = parts[2]; // get the directory name

                        if (param == "/") //if root
                        {
                            currentDirectory = rootDirectory; // set root
                        }
                        else if (param == "..")
                        {
                            // move up a folder in the tree
                            currentDirectory = currentDirectory.ParentFolder;
                        }
                        else
                        {
                            // create the new directory
                            DeviceFolder folder = new DeviceFolder()
                            {
                                Name = param,
                                ParentFolder = currentDirectory
                            };

                            // add to subfolders
                            currentDirectory.SubFolders.Add(folder);

                            // set current dir
                            currentDirectory = folder;

                            // add to total list of directories
                            allDirectories.Add(currentDirectory);
                        }
                    }
                }
                else // not a command
                {
                    string[] parts = line.Split(" ");

                    // make sure it's a file
                    if (parts[0] != "dir")
                    {
                        // create the file
                        DeviceFile file = new DeviceFile()
                        {
                            Name = parts[1],
                            Size = int.Parse(parts[0])
                        };

                        // add the file
                        currentDirectory.Files.Add(file);
                    }
                }
            }

            return allDirectories;
        }

        /// <summary>
        /// Advent code day 7 part 1
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Total size of all directories under 100000</returns>
        public static int Part1(string filePath)
        {
            List<DeviceFolder> directories = ReadInputFile(filePath);
            return directories.Where(x => x.TotalSize() <= 100000).Sum(x => x.TotalSize());
        }

        /// <summary>
        /// Advent code day 7 part 2
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Smallest directory that needs to be deleted to make enough free space</returns>
        public static int Part2(string filePath)
        {
            List<DeviceFolder> directories = ReadInputFile(filePath);
            int fileSystemSize = 70000000;
            int unusedNeeded = 30000000;
            int totalUsed = directories[0].TotalSize(); // root directory
            int totalAvailable = fileSystemSize - totalUsed;
            int amountToDelete = unusedNeeded - totalAvailable;

            return directories.OrderByDescending(x => x.TotalSize())
                .Where(x => x.TotalSize() >= amountToDelete).Last().TotalSize();
        }
    }

    internal enum DeviceCommand
    {
        CD,
        LS,
        None
    }

    internal class DeviceFolder
    {
        public string Name { get; set; }
        public DeviceFolder ParentFolder { get; set; }
        public List<DeviceFolder> SubFolders { get; set; }
        public List<DeviceFile> Files { get; set; }

        public DeviceFolder()
        {
            SubFolders = new List<DeviceFolder>();
            Files = new List<DeviceFile>();
        }

        /// <summary>
        /// Calc total size of directory
        /// </summary>
        /// <returns></returns>
        public int TotalSize()
        {
            int total = 0;

            if(Files.Count() > 0)
            {
                total += Files.Sum(x => x.Size);
            }

            if(SubFolders.Count() > 0)
            {
                // call recursive to include subfolders
                foreach (DeviceFolder folder in SubFolders)
                    total += folder.TotalSize();
            }

            return total;
        }

    }

    internal class DeviceFile
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public DeviceFile() { }
    }
}
