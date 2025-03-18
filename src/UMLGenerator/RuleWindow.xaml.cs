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
        ColumnDefinition colDef1 = new ColumnDefinition();
        
        RuleSetGrid.ColumnDefinitions.Add(colDef1);
        RuleSetGrid.RowDefinitions.Add(new RowDefinition());

        TextBlock tmpText = new TextBlock();
        tmpText.Text = "Select A RuleSet";

        RuleSetGrid.Children.Add(tmpText);

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
    }
}