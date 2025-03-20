using System.Windows;

namespace UMLGenerator;


public partial class RuleCreationPopup : Window{

    public String ruleSelected;
    public Ruleset ruleset;

    public RuleCreationPopup(){
        InitializeComponent();

        addRules();
    }

    public void addRules(){
        
    }

    public void SaveButton_Click(object sender, RoutedEventArgs e){

    }

    public void CancelButton_Click(object sender, RoutedEventArgs e){

    }

}