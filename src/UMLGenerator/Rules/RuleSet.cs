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
        [JsonInclude]
        public string language { get; set; }
        [JsonInclude]
        public string version { get; set; }
        [JsonInclude]
        public string[] fileExtentions { get; set; }
        [JsonInclude]
        public keywordSet keywords {get; set;}
        [JsonInclude]
        public string[] operations {get; set;}
        [JsonInclude]
        public symbolSet symbolSet {get; set;}
        [JsonInclude]
        public Dictionary<string, patternSet> patterns = new Dictionary<string, patternSet>();


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
                    PropertyNameCaseInsensitive = false,
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
                MessageBox.Show($"Error loading ruleset: {ex.Message} {ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            System.Diagnostics.Debug.WriteLine($"Language: {language} (Version: {version})");
        }

    }

    

    public class keywordSet{
        [JsonPropertyName("classDefinitions")][JsonInclude]
        public string[]? classDefinitions;
        [JsonPropertyName("methodDefinitions")][JsonInclude]
        public string[]? methodDefinitions;
        [JsonPropertyName("accessModifiers")][JsonInclude]
        public string[]? accessModifiers;
        [JsonPropertyName("nativeDataTypes")][JsonInclude]
        public string[]? nativeDataTypes;
        [JsonPropertyName("conditonalStatements")][JsonInclude]
        public string[]? conditonalStatements;
        [JsonPropertyName("inharitance")][JsonInclude]
        public string[]? inharitance;
        [JsonPropertyName("grouping")][JsonInclude]
        public string[]? grouping;
        [JsonPropertyName("other")][JsonInclude]
        public string[]? other;

        public bool contains(string value){
            bool contains = false;
            if(classDefinitions != null)
            contains = classDefinitions.Contains(value) || contains;

            if(methodDefinitions != null)
            contains = methodDefinitions.Contains(value) || contains;

            if(accessModifiers != null)
            contains = accessModifiers.Contains(value) || contains;

            if(nativeDataTypes != null)
            contains = nativeDataTypes.Contains(value) || contains;

            if(conditonalStatements != null)
            contains = conditonalStatements.Contains(value) || contains;

            if(inharitance != null)
            contains = inharitance.Contains(value) || contains;

            if(grouping != null)
            contains = grouping.Contains(value) || contains;

            if(other != null)
            contains = other.Contains(value) || contains;

            return contains;
        }
    }

    public class symbolSet{
        [JsonInclude]
        public string[]? endLine {get; set;}
        [JsonInclude]
        public string[]? openBlock {get; set;}
        [JsonInclude]
        public string[]? closeBlock {get; set;}
        [JsonInclude]
        public CommentStyle? commentSet {get; set;}
        [JsonInclude]
        public string[]? anotations {get; set;}
        [JsonInclude]
        public string[]? generics {get; set;}

        public bool Contains(string value){
            bool contains = false;
            if(endLine != null)
            contains = endLine.Contains(value) || contains;

            if(openBlock != null)
            contains = openBlock.Contains(value) || contains;

            if(closeBlock != null)
            contains = closeBlock.Contains(value) || contains;

            if(anotations != null)
            contains = anotations.Contains(value) || contains;

            if(generics != null)
            contains = generics.Contains(value) || contains;

            if(commentSet != null)
            contains = commentSet.Contains(value) || contains;

            return contains;
        }

    }

    public class CommentStyle{
        [JsonInclude]
        public string? singleLine {get; set;}
        [JsonInclude]
        public string? multiLineStart {get; set;}
        [JsonInclude]
        public string? multiLineEnd {get; set;}

        public bool Contains(string value){
            bool contains = false;
            if(singleLine != null)
            contains = singleLine.Contains(value) || contains;

            if(multiLineStart != null)
            contains = multiLineStart.Contains(value) || contains;

            if(multiLineEnd != null)
            contains = multiLineEnd.Contains(value) || contains;

            return contains;
        }
    }

    public class patternSet{
        [JsonInclude]
        public string type {get; set;}
        [JsonInclude]
        public string pattern {get; set;}
        [JsonInclude]
        public int minTokens {get; set;}
        [JsonInclude]
        public int maxTokens {get; set;}
    }

    
}