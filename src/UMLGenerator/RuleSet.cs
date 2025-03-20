using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace UMLGenerator
{
    public class Ruleset
    {
        public string LanguageName { get; set; }
        public string LanguageVersion { get; set; }
        public string FileExtention { get; set; }
        public Dictionary<string, SyntaxRule> Syntax { get; set; }


        public static Ruleset loadRulesetFromFile(String filePath){
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }

                String jsonContent = File.ReadAllText(filePath);
                JsonSerializerOptions options = new JsonSerializerOptions{
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                };

                Ruleset ruleset = JsonSerializer.Deserialize<Ruleset>(jsonContent, options);
                return ruleset;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ruleset: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public String toJsonString(){

            JsonSerializerOptions options = new JsonSerializerOptions{
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true,
                    WriteIndented = true
                };

            return JsonSerializer.Serialize(this, options);
        }

        public void generateJsonfile(String dirPath){
            String filePath = String.Join(Path.DirectorySeparatorChar, dirPath);

            if(File.Exists(filePath)){
                MessageBox.Show($"File already exist: {filePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            File.WriteAllText(filePath, toJsonString());
        }


        public void DisplayRuleset()
        {
            if (this == null) return;

            Console.WriteLine($"Language: {LanguageName} (Version: {LanguageVersion})");

            foreach (var rule in Syntax)
            {
                Console.WriteLine($"Rule: {rule.Key}");
                Console.WriteLine($"Description: {rule.Value.RuleDescription}");

                if (rule.Value.Structure != null)
                {
                    Console.WriteLine($"  Keyword: {rule.Value.Structure.Keyword}");

                    if (rule.Value.Structure.Modifyers != null)
                    {
                        Console.WriteLine($"  Modifiers: {string.Join(", ", rule.Value.Structure.Modifyers)}");
                    }

                    if (rule.Value.Structure.Extends != null)
                    {
                        Console.WriteLine($"  Extends: {rule.Value.Structure.Extends.Keyword}");
                    }

                    if (rule.Value.Structure.Arguments != null)
                    {
                        Console.WriteLine($"  Arguments: {rule.Value.Structure.Arguments.Openingchar}...{rule.Value.Structure.Arguments.Endingchar}");
                    }
                }
            }
        }

    }

    public class SyntaxRule
    {
        public string RuleDescription { get; set; }
        public Structure Structure { get; set; }
    }

    public class Structure
    {
        public List<string> Modifyers { get; set; }
        public string Keyword { get; set; }
        public Extends Extends { get; set; }
        public ReturnTypeLocation ReturnTypeLocation { get; set; }
        public Arguments Arguments { get; set; }
    }

    public class Extends
    {
        public string Keyword { get; set; }
    }

    public class ReturnTypeLocation
    {
        public int? IncrimentAmountByWords { get; set; }
        public int? IncrimentAmountByCharater { get; set; }
        public bool? SingleReturnType { get; set; }
    }

    public class Arguments
    {
        public string Openingchar { get; set; }
        public string SeperatingChar { get; set; }
        public string Endingchar { get; set; }
    }
}