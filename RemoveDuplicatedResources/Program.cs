Console.WriteLine("Enter project full path:");
var projectPath = Console.ReadLine().ToString();

Console.WriteLine("Enter destination file full path:");
var destinationFilePath = Console.ReadLine().ToString();

var directories1 = GetDirectories(projectPath);

var Files = new List<string>();

foreach (var d in directories1)
{
    Files.AddRange(Directory.GetFiles(d).Where(x => x.EndsWith(".cs") || x.EndsWith(".cshtml") || x.EndsWith(".js")));
}

List<string> GetDirectories(string path)
{

    var directories1 = new List<string>();

    directories1.Add(path);

    var newDiectory = new List<string>();
    newDiectory.AddRange(directories1);

    var tempDirectory = new List<string>();

    while (true)
    {
        foreach (var item in newDiectory)
        {
            tempDirectory.AddRange(Directory.GetDirectories(item));
        }

        if (tempDirectory.Count == 0) break;

        newDiectory.Clear();
        newDiectory.AddRange(tempDirectory);
        directories1.AddRange(tempDirectory);
        tempDirectory.Clear();
    }

    return directories1;
}

var allNames = new List<string>();

var destinationFile = File.ReadAllLines(destinationFilePath).ToList();

var namesInDestinationFile = new List<string>();

foreach (var line in destinationFile)
{
    if (line.Contains("<data"))
    {
        var name = line.Split("\"")[1];
        namesInDestinationFile.Add(name);
    }
}

var duplicatedNames = new List<string>();

foreach (var name in namesInDestinationFile.GroupBy(name => name).Where(g => g.Count() > 1))
{
    duplicatedNames.Add(name.Key);
}

// remove unused names from destination file
foreach (var name in duplicatedNames)
{
    Console.WriteLine(name);
}
