using System.Windows;

namespace UMLGenerator;


public partial class RuleCreationPopup : Window{

    public String ruleSelected;
    public Ruleset ruleset;

    public RuleCreationPopup(Ruleset currentRuleSet){
        InitializeComponent();

        ruleset = currentRuleSet;

    }

    public void SaveButton_Click(object sender, RoutedEventArgs e){
        ruleSelected = ruleSelection.Text as string;
        DialogResult = true;

        ruleset.constructRules.Add(ruleSelected, new ConstructRule());
        Close();
    }

    public void CancelButton_Click(object sender, RoutedEventArgs e){
        DialogResult = false;
        Close();
    }

}