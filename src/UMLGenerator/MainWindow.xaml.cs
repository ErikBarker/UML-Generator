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

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void AddClassBox_Click(object sender, RoutedEventArgs e){
        // Create a UML class box (Rectangle)
            Rectangle classBox = new Rectangle
            {
                Width = 150,
                Height = 80,
                Stroke = Brushes.Black,
                Fill = Brushes.LightGray,
                StrokeThickness = 2
            };

            // Create a label for the class name
            TextBlock className = new TextBlock
            {
                Text = "NewClass",
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Create a container (Grid) to hold both
            Grid classContainer = new Grid();
            classContainer.Children.Add(classBox);
            classContainer.Children.Add(className);

            // Position the class box
            Canvas.SetLeft(classContainer, 100);
            Canvas.SetTop(classContainer, 100);

            // Add to the canvas
            DrawingCanvas.Children.Add(classContainer);
    }

    public void RuleMenu_Click(object sender, RoutedEventArgs e){
        Console.WriteLine("Createing Rule Window");

        RuleWindow ruleWindow = new RuleWindow();

        ruleWindow.Show();
        ruleWindow.Activate();
    }
}