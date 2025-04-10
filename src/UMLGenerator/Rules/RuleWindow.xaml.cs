using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UMLGenerator;
//TODO redo createion window
public partial class RuleWindow : Window
{

    String languageFolder = System.IO.Path.Combine(
        Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.Parent.FullName,
        "languageSettings"
    );

    Button currentEditorButton = null;
    Ruleset currentEditorRuleSet = null;

    private TextBox focusedTextBox = null;


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

    public void CreateNewRuleSet_Click(object sender, RoutedEventArgs e){
        RuleSetWindowCreationPopup ruleWindowCreationPopup = new RuleSetWindowCreationPopup();
        bool? result = ruleWindowCreationPopup.ShowDialog();

        if (!ruleWindowCreationPopup.save)
        {
            return;
        }
        
        Ruleset ruleset = new Ruleset{
            //TODO redo rule creation window
            //constructRules = new Dictionary<string, ConstructRule>(),
            language = ruleWindowCreationPopup.LanguageName,
            version = ruleWindowCreationPopup.Version
        };

        ruleset.generateJsonfile(String.Join(System.IO.Path.DirectorySeparatorChar , languageFolder, String.Join("", ruleset.language, ruleset.version, ".json")).ToLower());
        LoadRuleSetIntoEditor(ruleset);
        updateRuleList();
    }

    public void DeleteRuleSet_Click(object sender, RoutedEventArgs e){
        
        ButtonPanel.Children.Remove(currentEditorButton);

        File.Delete(currentEditorButton.Tag as String);

        updateRuleList();
    }

    // Adjust saving feature to only replace necisary data if needed for porformance

    public void SaveRuleSet_Click(object sender, RoutedEventArgs e){

        SaveRules();
        currentEditorRuleSet.generateJsonfile((String)currentEditorButton.Tag);
        LoadRuleSetIntoEditor(currentEditorRuleSet);
    }

    public void SaveRules()
    {
        
    }

    public void CreateNewRule_Click(object sender, RoutedEventArgs e){
        RuleCreationPopup ruleCreationPopup = new RuleCreationPopup(currentEditorRuleSet);
        bool? result = ruleCreationPopup.ShowDialog();

        if (!result.Value)
        {
            return;
        }
        

        Console.WriteLine("Added new rule");
        LoadRuleSetIntoEditor(currentEditorRuleSet);
    }

    public void DeleteRule_Click(Object sender, RoutedEventArgs e){
        if (focusedTextBox==null){
            MessageBox.Show("No rule selected to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        Grid parentGrid = focusedTextBox.Parent as Grid;
        if (parentGrid == null){
            return;
        }

        // Find the index of the parent grid in the RuleSetPanel
        int gridIndex = RuleSetPanel.Children.IndexOf(parentGrid);
        if (gridIndex == -1) return;
    
        // Traverse backward to find the TextBlock with the rule name
        TextBlock ruleNameBlock = null;
        for (int i = gridIndex - 1; i >= 0; i--)
        {
            if (RuleSetPanel.Children[i] is TextBlock tb)
            {
                ruleNameBlock = tb;
                break;
            }
        }
    
        if (ruleNameBlock == null)
        {
            MessageBox.Show("No rule name found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
    
        string ruleName = ruleNameBlock.Text;
    
        //TODO Redo remove ruleset
        // Remove the rule from the ruleset
        // if (currentEditorRuleSet.constructRules.ContainsKey(ruleName))
        // {
        //     currentEditorRuleSet.constructRules.Remove(ruleName);
    
        //     // Save the updated ruleset
        //     currentEditorRuleSet.generateJsonfile((string)currentEditorButton.Tag);
    
        //     // Reload the rules into the editor
        //     LoadRuleSetIntoEditor(currentEditorRuleSet);
    
        //     MessageBox.Show($"Rule '{ruleName}' deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
        //     LoadRuleSetIntoEditor(currentEditorRuleSet);
        // }
        // else
        // {
        //     MessageBox.Show($"Rule '{ruleName}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        // }
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

    public void updateRuleList(){
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

        foreach (Button button in ButtonPanel.Children)
        {
            if(File.Exists(button.Tag.ToString())){
                continue;
            }

            ButtonPanel.Children.Remove(button);
        }
    }

    public void CreateRuleButton(String btnTxt, String path){

        foreach (Button button in ButtonPanel.Children)
        {
            if (button.Tag.ToString().Equals(path))
            {
                return;
            }
        }

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
            currentEditorButton = button;
            currentEditorRuleSet = Ruleset.loadRulesetFromFile(filePath);
            LoadJsonIntoEditor(filePath);
        }
    }

    public void LoadJsonIntoEditor(String filePath){

        RuleSetPanel.Children.Clear();

        Ruleset ruleset = Ruleset.loadRulesetFromFile(filePath);

        TextBlock language = new TextBlock{
            Text = $"Language: {ruleset.language}",
            TextAlignment = TextAlignment.Center
        };

        TextBlock version = new TextBlock{
            Text = $"Version: {ruleset.version}",
            TextAlignment = TextAlignment.Center
        };


        // RuleSetPanel.Children.Add(language);
        // RuleSetPanel.Children.Add(version);

        // foreach (var rule in ruleset.constructRules){

        //     //Add the Name of the rule
        //     TextBlock ruleName = new TextBlock{
        //         Text = rule.Key,
        //         TextAlignment = TextAlignment.Center
        //     };

        //     RuleSetPanel.Children.Add(ruleName);

            

        // }
    }

    public void LoadRuleSetIntoEditor(Ruleset ruleSet){
        
        RuleSetPanel.Children.Clear();
        Ruleset ruleset = ruleSet;

        TextBlock language = new TextBlock{
            Text = $"Language: {ruleset.language}",
            TextAlignment = TextAlignment.Center
        };

        TextBlock version = new TextBlock{
            Text = $"Version: {ruleset.version}",
            TextAlignment = TextAlignment.Center
        };


        RuleSetPanel.Children.Add(language);
        RuleSetPanel.Children.Add(version);

        //TODO redo rule construction
        // foreach (var rule in ruleset.constructRules){

        //     //Add the Name of the rule
        //     TextBlock ruleName = new TextBlock{
        //         Text = rule.Key,
        //         TextAlignment = TextAlignment.Center
        //     };

        //     RuleSetPanel.Children.Add(ruleName);

        //     //Add the keyword of the rule
        //     TextBlock keyword = new TextBlock{
        //         Text = rule.Value.keyword,
        //         TextAlignment = TextAlignment.Center
        //     };

        //     RuleSetPanel.Children.Add(keyword);

        //     //Add the modifier of the rule
        //     TextBlock modifier = new TextBlock{
        //         Text = String.Join(", ", rule.Value.modifier),
        //         TextAlignment = TextAlignment.Center
        //     };

        //     RuleSetPanel.Children.Add(modifier);

        //     //Add the pattern of the rule
        //     TextBlock pattern = new TextBlock{
        //         Text = rule.Value.pattern,
        //         TextAlignment = TextAlignment.Center
        //     };

        //     RuleSetPanel.Children.Add(pattern);

        //     //Add the scopeType of the rule
        //     TextBlock scopeType = new TextBlock{
        //         Text = rule.Value.scopeType,
        //         TextAlignment = TextAlignment.Center
        //     };

        //     RuleSetPanel.Children.Add(scopeType);

        // }
    }

    public Grid generateEditorGridElement(String label, String text, String rule){
            Grid ruleGridElement = new Grid{
                Margin = new Thickness(5)
            };

            TextBlock ruleLabel = new TextBlock{
                Text = label,
                TextAlignment = TextAlignment.Center
            };

            TextBox ruleName = new TextBox{
                Text = text,
                TextAlignment = TextAlignment.Center,
                Tag = rule
            };

            ruleName.GotFocus += (sender, e) => focusedTextBox = sender as TextBox;

            ruleGridElement.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)});
            ruleGridElement.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(2, GridUnitType.Star)});

            Grid.SetColumn(ruleLabel, 0);
            Grid.SetColumn(ruleName, 1);

            ruleGridElement.Children.Add(ruleLabel);
            ruleGridElement.Children.Add(ruleName);

        return ruleGridElement;
    }
}