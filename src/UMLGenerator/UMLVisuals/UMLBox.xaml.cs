using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UMLGenerator;

namespace UMLDiagram{

    public partial class UMLBox : UserControl
    {
        private bool isDragging = false;
        private Point offset;

        public UMLBox(){
            InitializeComponent();

            this.MouseLeftButtonDown += OnMouseDown;
            this.MouseMove += OnMouseMove;
            this.MouseLeftButtonUp += OnMouseUp;
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
                }
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e){
            isDragging = false;
            this.ReleaseMouseCapture();
        }
    }

    class UMLData
    {
        
    }
}