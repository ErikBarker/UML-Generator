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
    public RuleWindow(){

        InitializeComponent();

        ColumnDefinition colDef1 = new ColumnDefinition();
        ColumnDefinition colDef2 = new ColumnDefinition();
        ColumnDefinition colDef3 = new ColumnDefinition();
        MainRuleGrid.ColumnDefinitions.Add(colDef1);
        MainRuleGrid.ColumnDefinitions.Add(colDef2);
        MainRuleGrid.ColumnDefinitions.Add(colDef3);


        RowDefinition rowDef1 = new RowDefinition();
        RowDefinition rowDef2 = new RowDefinition();
        RowDefinition rowDef3 = new RowDefinition();

        rowDef3.Height = new GridLength(Height-100);

        MainRuleGrid.RowDefinitions.Add(rowDef1);
        MainRuleGrid.RowDefinitions.Add(rowDef2);
        MainRuleGrid.RowDefinitions.Add(rowDef3);

    }

    public void CreateNewRule_Click(object sender, RoutedEventArgs e){

    }
}