using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;

namespace UMLGenerator;

public partial class RuleWindow : Window
{

    String languageFolder = System.IO.Path.Combine(
        Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.Parent.FullName,
        "languageSettings"
    );


    public RuleWindow(){

        InitializeComponent();

        
        initRuleButtonList();

        LoadCurrentRules();

        initEditor();

    }

    public void initRuleButtonList(){
        ColumnDefinition colDef1 = new ColumnDefinition();
        ColumnDefinition colDef2 = new ColumnDefinition();
        ColumnDefinition colDef3 = new ColumnDefinition();
        MainRuleGrid.ColumnDefinitions.Add(colDef1);
        MainRuleGrid.ColumnDefinitions.Add(colDef2);
        MainRuleGrid.ColumnDefinitions.Add(colDef3);


        RowDefinition rowDef1 = new RowDefinition();
        RowDefinition rowDef2 = new RowDefinition();

        rowDef2.Height = new GridLength(Height-100);

        MainRuleGrid.RowDefinitions.Add(rowDef1);
        MainRuleGrid.RowDefinitions.Add(rowDef2);
    }

    public void initEditor(){
        RuleSetPanel.Margin = new Thickness(10);
        
        TextBlock tmpText = new TextBlock();
        tmpText.Text = "Select A RuleSet";


    }

    public void CreateNewRule_Click(object sender, RoutedEventArgs e){

    }

    public void LoadCurrentRules(){
        

        String[] filePaths = Directory.GetFiles(languageFolder);

        foreach (String filePath in filePaths)
        {
            if (!System.IO.Path.GetExtension(filePath).ToLower().Equals(".json"))
            {
                continue;
            }

            String ruleName = System.IO.Path.GetFileName(filePath).Split(".")[0];

            CreateRuleButton(ruleName, filePath);
        }


    }

    public void CreateRuleButton(String btnTxt, String path){
        Button ruleButton = new Button{
            Content = btnTxt,
            Margin = new Thickness(5),
            Tag = path
        };

        ruleButton.Click += RuleButton_Click;
        ButtonPanel.Children.Add(ruleButton);
    }

    public void RuleButton_Click(object sender, RoutedEventArgs e){
        if (sender is Button button && button.Tag is string filePath)
        {
            Console.WriteLine("Open " + button.Content);
            LoadJsonIntoEditor(filePath);
        }
    }

    public void LoadJsonIntoEditor(String filePath){
        Ruleset ruleset = Ruleset.loadRulesetFromFile(filePath);

        ruleset.DisplayRuleset();

        TextBlock language = new TextBlock{
            Text = $"Language: {ruleset.LanguageName}",
            TextAlignment = TextAlignment.Center
        };

        TextBlock version = new TextBlock{
            Text = $"Version: {ruleset.LanguageVersion}",
            TextAlignment = TextAlignment.Center
        };


        RuleSetPanel.Children.Add(language);
        RuleSetPanel.Children.Add(version);

        foreach (var rule in ruleset.Syntax){

            //Add the Name of the rule
            TextBlock ruleName = new TextBlock{
                Text = rule.Key,
                TextAlignment = TextAlignment.Center
            };

            RuleSetPanel.Children.Add(ruleName);

            //Add the discription of the rule
            RuleSetPanel.Children.Add(generateEditorGridElement("Description:", rule.Value.RuleDescription));

            //Add all the values asociated with a structure
            if (rule.Value.Structure != null)
            {
                RuleSetPanel.Children.Add(generateEditorGridElement("KeyWord:", rule.Value.Structure.Keyword));

                if (rule.Value.Structure.Modifyers != null)
                {
                    RuleSetPanel.Children.Add(generateEditorGridElement("Modifiers:", string.Join(", ", rule.Value.Structure.Modifyers)));
                }

                if (rule.Value.Structure.Extends != null)
                {
                    RuleSetPanel.Children.Add(generateEditorGridElement("Extends:", rule.Value.Structure.Extends.Keyword));
                }

                if (rule.Value.Structure.Arguments != null)
                {
                    RuleSetPanel.Children.Add(generateEditorGridElement("Arguments:", string.Join(", ", [rule.Value.Structure.Arguments.Openingchar, rule.Value.Structure.Arguments.SeperatingChar, rule.Value.Structure.Arguments.Endingchar])));
                }
            }

        }
    }

    public Grid generateEditorGridElement(String label, String text){
            Grid ruleGridElement = new Grid{
                Margin = new Thickness(5)
            };

            TextBlock ruleLabel = new TextBlock{
                Text = label,
                TextAlignment = TextAlignment.Center
            };

            TextBox ruleName = new TextBox{
                Text = text,
                TextAlignment = TextAlignment.Center
            };

            ruleGridElement.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)});
            ruleGridElement.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(2, GridUnitType.Star)});

            Grid.SetColumn(ruleLabel, 0);
            Grid.SetColumn(ruleName, 1);

            ruleGridElement.Children.Add(ruleLabel);
            ruleGridElement.Children.Add(ruleName);

        return ruleGridElement;
    }
}