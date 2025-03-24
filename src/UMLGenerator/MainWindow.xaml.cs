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
using UMLDiagram;

namespace UMLGenerator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static double GridSize = 10;

    public MainWindow()
    {
        InitializeComponent();
    }

    public void AddClassBox_Click(object sender, RoutedEventArgs e){
        UMLBox box = new UMLBox();
            
        // Set initial position
        Canvas.SetLeft(box, 50);
        Canvas.SetTop(box, 50);

        UmlCanvas.Children.Add(box);

    }

    public void RuleMenu_Click(object sender, RoutedEventArgs e){
        Console.WriteLine("Createing Rule Window");

        RuleWindow ruleWindow = new RuleWindow();

        ruleWindow.Show();
        ruleWindow.Activate();
    }
}