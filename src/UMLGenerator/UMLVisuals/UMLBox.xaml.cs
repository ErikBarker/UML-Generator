using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using UMLGenerator;

namespace UMLDiagram{

    public partial class UMLBox : UserControl
    {   
        private static int count = 0;
        private bool isDragging = false;

        public UMLData data = new UMLData();

        private Point offset;

        public UMLBox(){
            InitializeComponent();

            data.id = count;
            count++;

            this.MouseLeftButtonDown += OnMouseDown;
            this.MouseMove += OnMouseMove;
            this.MouseLeftButtonUp += OnMouseUp;
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
        }

        private void OnMouseEnter(object sender, MouseEventArgs e){
            if (!MainWindow.drawingConnection)
            {
                hoverButtons.Visibility = Visibility.Visible;
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e){
            if (!MainWindow.drawingConnection)
            {
                hoverButtons.Visibility = Visibility.Collapsed;
            }

            
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e){
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isDragging = true;
                offset = e.GetPosition(this);
                this.CaptureMouse();
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e){
            if (isDragging)
            {
                dragUML(e);
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e){
            isDragging = false;
            this.ReleaseMouseCapture();
        }

        public void addConnection_Click(object sender, RoutedEventArgs e){
            var parent = FindParent<MainWindow>(this);
            if (parent is MainWindow mainWindow)
            {
                mainWindow.StartConnection(this);
            }
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }

        private void dragUML(MouseEventArgs e){
            Canvas parent = this.Parent as Canvas;

                if (parent != null)
                {
                    Point mousePos = e.GetPosition(parent);

                    double x;
                    double y;

                    if (Keyboard.IsKeyDown(Key.LeftShift)||Keyboard.IsKeyDown(Key.RightShift))
                    {
                        x = mousePos.X - offset.X;
                        y = mousePos.Y - offset.Y;
                    } else {
                        x = Math.Round((mousePos.X - offset.X) / MainWindow.GridSize) * MainWindow.GridSize;
                        y = Math.Round((mousePos.Y - offset.Y) / MainWindow.GridSize) * MainWindow.GridSize;
                    }
                    

                Canvas.SetLeft(this, x);
                Canvas.SetTop(this, y);

                foreach (Connection connection in data.connections)
                {
                    if (connection.startItemID == data.id)
                    {
                        connection.setHead(GetCenterPoint());
                    }
                    else if (connection.endItemID == data.id)
                    {
                        connection.setTail(GetCenterPoint());
                    }

                    connection.removeVisual(parent);
                    connection.updateLines();
                    connection.draw(parent);
                }
            }
        }

        public Point GetCenterPoint()
        {
            double x = Canvas.GetLeft(this) + this.ActualWidth / 2;
            double y = Canvas.GetTop(this) + this.ActualHeight / 2;
            return new Point(x, y);
        }
    }

    public class UMLData
    {
        public int id;
        public float width;
        public float height;
        public Point pos;

        public LinkedList<Connection> connections = new LinkedList<Connection>();

    }


}