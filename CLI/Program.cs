// CLI/Program.cs (top-level statements)
using Shared;

// Read all input from stdin
string input = Console.In.ReadToEnd();

try
{
    // Parse input using Shared JSON parser
    var parsed = JSON.Parse(input);

    // Output pretty-printed result
    Console.WriteLine(parsed.ToString());
}
catch (Exception ex)
{
    Console.Error.WriteLine("Error parsing JSON: " + ex.Message);
    Environment.Exit(1);
}
