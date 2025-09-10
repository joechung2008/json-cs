using Shared;

try
{
    // Read all input from stdin
    string input;
    using (var reader = new System.IO.StreamReader(Console.OpenStandardInput()))
    {
        input = reader.ReadToEnd();
    }

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
