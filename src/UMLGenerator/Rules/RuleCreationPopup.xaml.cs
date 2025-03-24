using System.Windows;

namespace UMLGenerator;


public partial class RuleCreationPopup : Window{

    public String ruleSelected;
    public Ruleset ruleset;

    public RuleCreationPopup(Ruleset currentRuleSet){
        InitializeComponent();

        ruleset = currentRuleSet;
        
        addRules();
    }

    public void addRules(){
        foreach (string rulename in Ruleset.ValidSyntaxNames)
        {
            if(!ruleset.Syntax.ContainsKey(rulename)){
                ruleSelection.Items.Add(rulename);
            }
                
        }


    }

    public void SaveButton_Click(object sender, RoutedEventArgs e){
        ruleSelected = ruleSelection.SelectedItem as string;
        DialogResult = true;
        Close();
    }

    public void CancelButton_Click(object sender, RoutedEventArgs e){
        DialogResult = false;
        Close();
    }

}