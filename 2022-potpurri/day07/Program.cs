const long DirectoryMarker = -1;


var sampleInput = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";
SolvePart1(sampleInput, 95437);
SolvePart1(File.ReadAllText("input.txt"));

void SolvePart1(string input, int? expectedAnswer = null)
{
    var lines = input.Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
    long smallDirectorySizes = 0;
    var filesystem = Parse(lines);

    foreach (var directory in GetDirectories(filesystem))
    {
        var size = CalculateSize(filesystem, directory);
        if (size <= 100000)
            smallDirectorySizes += size;
    }

    var expectedString = expectedAnswer != null ? $"\t\tExpected: {expectedAnswer.Value}" : "";
    Console.WriteLine($"Actual: {smallDirectorySizes} {expectedString}");
}

List<string> GetDirectories(Dictionary<string, long> filesystem)
{
    return filesystem.Where(x => x.Value == DirectoryMarker).Select(x => x.Key).ToList();
}

long CalculateSize(Dictionary<string, long> filesystem, string directory)
{
    return filesystem.Where(x => x.Key.StartsWith(directory + "/"))
        .Where(x => x.Value != DirectoryMarker)
        .Sum(x => x.Value);
}

Dictionary<string, long> Parse(List<string> lines)
{
    var q = new Queue<string>(lines);
    string pwd = "";
    var filesystem = new Dictionary<string, long>();
    while (q.Any())
    {
        var line = q.Dequeue();
        var words = line.Split(" ");
        if (words[0] == "$" && words[1] == "cd")
        {
            // $ cd ..
            if (words[2] == "..")
            {
                pwd = UpOne(pwd);
            } 
            else
            {
                // $ cd abc
                pwd = Combine(pwd, words[2]);
            }
        }
        else if (words[0] == "$" && words[1] == "ls")
        {
            // parse until queue is empty or we get a $
            while (q.Any() && !q.Peek().StartsWith("$"))
            {
                line = q.Dequeue();
                words = line.Split(" ");
                var filesystemEntry = Combine(pwd, words[1]);
                if (words[0] == "dir")
                {
                    filesystem.Add(filesystemEntry, DirectoryMarker);
                }
                else
                {
                    filesystem.Add(filesystemEntry, long.Parse(words[0]));
                }
            }
        }
        else throw new Exception($"Expected a command; got {line}");
    }
    return filesystem;
}

string Combine(string pwd, string directory)
{
    return Path.Combine(pwd, directory).Replace(@"\", "/");
}

string UpOne(string pwd)
{
    return Path.GetDirectoryName(pwd)!.Replace(@"\", "/");
}