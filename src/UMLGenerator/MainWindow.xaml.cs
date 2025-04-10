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
using UMLGenerator.CodeScanner;

namespace UMLGenerator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private UMLBox startBox = null;
    private Line tempLine = null;
    public static bool drawingConnection = false;
    public static double GridSize = 10;
    
    public MainWindow()
    {
        InitializeComponent();

        UmlCanvas.MouseMove += OnMouseMove;
        UmlCanvas.MouseUp += OnMouseUp;

        Ruleset testRuleset = Ruleset.loadRulesetFromFile("C:\\Users\\erikb\\Documents\\GitHub\\UML-Generator\\languageSettings\\java24.json");
        CodeScanner.CodeScanner.scanLocation = "testFiles";
        CodeScanner.CodeScanner.startScan();
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

    public void StartConnection(UMLBox box){
        startBox = box;

        // Initialize the temporary line for visual feedback
        tempLine = new Line
        {
            Stroke = System.Windows.Media.Brushes.Gray,
            StrokeThickness = 2
        };
        UmlCanvas.Children.Add(tempLine);
    }

    private void OnMouseMove(object sender, MouseEventArgs e){
        if (startBox == null || tempLine == null) return;

        Point startPos = startBox.GetCenterPoint();
        Point mousePos = e.GetPosition(UmlCanvas);

        tempLine.X1 = startPos.X;
        tempLine.Y1 = startPos.Y;
        tempLine.X2 = mousePos.X;
        tempLine.Y2 = mousePos.Y;
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (startBox == null || tempLine == null)
        {
            ClearTempLine();
            return;
        }

        // Find the target UMLBox under the mouse
        var targetBox = FindUMLBoxUnderMouse(e);

        if (targetBox != null && targetBox != startBox)
        {
            // Create the connection
            CreateConnection(startBox, targetBox);
        }

        ClearTempLine();
    }

    private void ClearTempLine()
        {
            if (tempLine != null)
            {
                UmlCanvas.Children.Remove(tempLine);
                tempLine = null;
            }

            startBox = null;
        }

        private UMLBox FindUMLBoxUnderMouse(MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(UmlCanvas);
            
            foreach (UIElement element in UmlCanvas.Children)
            {
                if (element is UMLBox box)
                {
                    double left = Canvas.GetLeft(box);
                    double top = Canvas.GetTop(box);

                    if (mousePos.X >= left && mousePos.X <= left + box.ActualWidth &&
                        mousePos.Y >= top && mousePos.Y <= top + box.ActualHeight)
                    {
                        return box;
                    }
                }
            }
            

            return null;
        }

        private void CreateConnection(UMLBox start, UMLBox end)
        {
            Point startPos = start.GetCenterPoint();
            Point endPos = end.GetCenterPoint();

            // Create a single connection object
            Connection connection = new Connection(startPos, start.data.id, endPos, end.data.id);

            // Add the same connection reference to both boxes
            start.data.connections.AddFirst(connection);
            end.data.connections.AddLast(connection);

            // Draw the connection line
            connection.draw(UmlCanvas);
        }
}