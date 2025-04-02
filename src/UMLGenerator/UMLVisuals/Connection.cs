using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using UMLGenerator;

public class Connection
{
    private LinkedList<Point> points = new LinkedList<Point>();
    private LinkedList<Ellipse> pointhitBoxes = new LinkedList<Ellipse>();//TODO impliment
    private LinkedList<Line> lines = new LinkedList<Line>();
    private LinkedList<Line> linehitBoxes = new LinkedList<Line>();

    private ArrowType headType = ArrowType.solid;
    private ArrowType tailType = ArrowType.none;
    private LineType lineType = LineType.sharp | LineType.solid;
    public int startItemID;
    public int endItemID;

    private bool hovered = false;
    private bool draggingPoint = false;

    

    public Connection(Point start, Point end){
        points.AddFirst(start);
        points.AddLast(end);
    }

    public Connection(Point start, int startID, Point end, int endID){
        points.AddFirst(start);
        points.AddLast(end);

        startItemID = startID;
        endItemID = endID;

        
    }

    public void addPoint(Point point){
        points.AddBefore(points.Last, point);
    }

    public void removePoint(Point point){
        points.Remove(point);
    }

    public void setHead(Point point){
        points.RemoveFirst();
        points.AddFirst(point);
    }

    public void setTail(Point point){
        points.RemoveLast();
        points.RemoveLast();
        points.AddLast(point);
        points.AddLast(point);
    }

    public void setHeadtype(ArrowType arrow){
        headType = arrow;
    }

    public void setTailtype(ArrowType arrow){
        tailType = arrow;
    }

    // Add a new type using bitwise OR
    public void AddLineType(LineType type)
    {
        lineType |= type;  
    }

    // Remove a type using bitwise AND with negation
    public void RemoveLineType(LineType type)
    {
        lineType &= ~type;  
    }

    public void updateLines(){

        foreach (Line line in lines)
        {
            if (line.Parent is Canvas parentCanvas)
            {
                parentCanvas.Children.Remove(line);
            }
        }

        foreach (Line hitbox in linehitBoxes)
        {
            if (hitbox.Parent is Canvas parentCanvas)
            {
                parentCanvas.Children.Remove(hitbox);
            }
        }
        
        lines.Clear();
        linehitBoxes.Clear();

        if (points.Count < 2)
        return;

        Point previousPoint = points.First.Value;

        foreach (Point currentPoint in points.Skip(1))
        {
            Line line = new Line
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = previousPoint.X,
                Y1 = previousPoint.Y,
                X2 = currentPoint.X,
                Y2 = currentPoint.Y
            };

            // Invisible hitbox line
            Line hitbox = new Line
            {
                Stroke = Brushes.Transparent,      // Invisible line
                StrokeThickness = 10,              // Larger thickness for easier clicks
                X1 = previousPoint.X,
                Y1 = previousPoint.Y,
                X2 = currentPoint.X,
                Y2 = currentPoint.Y
            };

            hitbox.MouseEnter += OnMouseEnter;
            hitbox.MouseLeave += OnMouseLeave;
            hitbox.MouseLeftButtonDown += OnMouseDownLeft;
            hitbox.MouseRightButtonDown += OnMouseDownRight;

            Panel.SetZIndex(line, -3);
            Panel.SetZIndex(hitbox, -2);

            lines.AddLast(line);
            linehitBoxes.AddLast(hitbox);
            previousPoint = currentPoint;  // Update the previous point
        }
    }

    private void OnMouseDownLeft(object sender, MouseButtonEventArgs e){
        if (hovered)
        {
            Canvas parent = ((Line)sender).Parent as Canvas;

            if (parent != null)
            {
                Point mousePos = e.GetPosition(parent);

                // Find the line that was clicked
                Line clickedLine = sender as Line;

                if (clickedLine != null)
                {
                    // Find the segment corresponding to this line
                    int index = GetLineIndex(linehitBoxes, clickedLine);
                    if (index >= 0 && index < points.Count - 1)
                    {
                        Point p1 = points.ElementAt(index);
                        Point p2 = points.ElementAt(index + 1);

                        // Calculate the closest point on the line
                        Point closestPoint = GetClosestPointOnLine(p1, p2, mousePos);

                        // Insert the new point into the list
                        points.AddBefore(points.Find(p2), closestPoint);

                        // Redraw the connection with the new point
                        updateLines();
                        draw(parent);
                    }
                }
            }
        }
    }

    private void OnMouseDownRight (object sender, MouseButtonEventArgs e){
        
    }

    private void OnMouseEnter(object sender, MouseEventArgs e){
        hovered = true;
        foreach (Line line in lines)
        {
            line.Stroke = Brushes.Blue;  // Reset color
        }
    }

    private void OnMouseLeave(object sender, MouseEventArgs e){
        hovered = false;
        foreach (Line line in lines)
        {
            line.Stroke = Brushes.Black;  // Reset color
        }
        
    }

    private Point GetClosestPointOnLine(Point p1, Point p2, Point click)
    {
        double dx = p2.X - p1.X;
        double dy = p2.Y - p1.Y;

        if (dx == 0 && dy == 0)
            return p1;

        double t = ((click.X - p1.X) * dx + (click.Y - p1.Y) * dy) / (dx * dx + dy * dy);

        t = Math.Max(0, Math.Min(1, t));

        return new Point(p1.X + t * dx, p1.Y + t * dy);
    }

    private int GetLineIndex(LinkedList<Line> list, Line clickedLine)
    {
        int index = 0;
        foreach (Line line in list)
        {
            if (line == clickedLine)
            {
                return index;
            }
            index++;
        }
        return -1;  // Return -1 if not found (shouldn't happen)
    }

    public void draw(Canvas parentCanvas){
        
        updateLines();
        

        foreach (Line line in lines)
        {
            if (!parentCanvas.Children.Contains(line))
            {
                parentCanvas.Children.Add(line);
            }
        }

        foreach (Line line in linehitBoxes)
        {
            if (!parentCanvas.Children.Contains(line))
            {
                parentCanvas.Children.Add(line);
            }
        }
    }

    public void removeVisual(Canvas parentCanvas){
        foreach (Line line in lines)
        {
            parentCanvas.Children.Remove(line);
        }

        foreach (Line line in linehitBoxes)
        {
            parentCanvas.Children.Remove(line);
        }
    }

    public enum ArrowType
    {
        solid, hollow, none
    }

    [Flags]

    public enum LineType{
        solid = 1, 
        dashed = 2, 
        curved = 4, 
        sharp = 5
    }
}