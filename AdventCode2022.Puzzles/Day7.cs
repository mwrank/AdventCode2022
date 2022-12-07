using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public static class Day7
    {
        private static List<DeviceFolder> ReadInputFile(string filePath)
        {
            DeviceFolder currentDirectory = new DeviceFolder() { Name = "/" };
            DeviceFolder rootDirectory = currentDirectory;
            List<DeviceFolder> allDirectories = new List<DeviceFolder>() { currentDirectory };
            DeviceCommand lastCommand = DeviceCommand.None;

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                if (line.StartsWith("$"))
                {
                    string[] parts = line.Split(" ");
                    string cmd = parts[1];

                    lastCommand = cmd == "cd" ? DeviceCommand.CD : DeviceCommand.LS;

                    if (lastCommand == DeviceCommand.CD)
                    {
                        string param = parts[2];

                        if (param == "/")
                        {
                            currentDirectory = rootDirectory;
                        }
                        else if (param == "..")
                        {
                            currentDirectory = currentDirectory.ParentFolder;
                        }
                        else
                        {
                            DeviceFolder folder = new DeviceFolder()
                            {
                                Name = param,
                                ParentFolder = currentDirectory
                            };

                            currentDirectory.SubFolders.Add(folder);
                            currentDirectory = folder;

                            allDirectories.Add(currentDirectory);
                        }
                    }
                }
                else
                {
                    string[] parts = line.Split(" ");

                    if (parts[0] != "dir")
                    {
                        DeviceFile file = new DeviceFile()
                        {
                            Name = parts[1],
                            Size = int.Parse(parts[0])
                        };

                        currentDirectory.Files.Add(file);
                    }
                }
            }

            return allDirectories;
        }

        public static int Part1(string filePath)
        {
            List<DeviceFolder> directories = ReadInputFile(filePath);
            return directories.Where(x => x.TotalSize() <= 100000).Sum(x => x.TotalSize());
        }

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

        public int TotalSize()
        {
            int total = 0;

            if(Files.Count() > 0)
            {
                total += Files.Sum(x => x.Size);
            }

            if(SubFolders.Count() > 0)
            {
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
