using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace UMLGenerator
{
    public class Ruleset
    {
        [JsonIgnore]
        public static Dictionary<string, Ruleset> fileExtentionPairs = new Dictionary<string, Ruleset>();
        public string language { get; set; }
        public string version { get; set; }
        public string[] fileExtentions { get; set; }
        public string[] keywords {get; set;}
        public CommentStyle commentStyle {get; set;}
        public string scopeType {get; set;}
        public Dictionary<string, ConstructRule> constructRules = new Dictionary<string, ConstructRule>();


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

                foreach (string extention in ruleset.fileExtentions)
                {
                    fileExtentionPairs.Add(extention, ruleset);
                }
                
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
                MessageBoxResult saveResult = MessageBox.Show($"File already exist: {filePath}\n Would you like to update the file", "Error", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if(saveResult == MessageBoxResult.No){
                    MessageBox.Show("The RuleSet will not be saved", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                } else{
                    File.Delete(filePath);
                }
            }

            File.WriteAllText(filePath, toJsonString());
        }


        public void DisplayRuleset()
        {
            if (this == null) return;

            Console.WriteLine($"Language: {language} (Version: {version})");
        }

    }

    public class ConstructRule
    {
        public string ruleDescription { get; set; }
        public string? keyword { get; set; }
        public List<string> modifier {get; set;}
        public string? pattern {get; set;}
        public string scopeType {get; set;}
        
    }

    public class CommentStyle{
        public string? singleLine {get; set;}
        public string? multiLineStart {get; set;}
        public string? multiLineEnd {get; set;}
    }

    
}