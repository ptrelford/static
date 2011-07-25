using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

/// <summary>
/// Implementation steps:
/// <list type="number">
///     <item>Implement the Program method in a derived class</item>
///     <item>Instantiate the object</item>
///     <item>Invoke the Run method</item>
/// </list>
/// </summary>
public abstract class TurtleLanguage
{
    #region Fields
    private double x;
    private double y;
    private double a;
    private bool isPenDown = true;
    private Color penColor = Colors.White;
    private Canvas screen;
    #endregion

    #region Run Methods
    public void Run()
    {
        Run(400, 400);
    }

    public void Run(double width, double height)
    {
        Invoke(() =>
            {
                var canvas = new Canvas() { Width = width, Height = height };
                canvas.Background = new SolidColorBrush(Colors.Black);
                Run(canvas);
            });
    }

    private void Run(Canvas canvas)
    {
        screen = canvas;
        x = canvas.Width / 2;
        y = canvas.Height / 2;

        var window = new Window();
        window.Content = canvas;
        window.SizeToContent = SizeToContent.WidthAndHeight;

        Program();
        
        var app = new Application();
        app.Run(window);        
    }

    private static void Invoke(ThreadStart start)
    {
        var thread = new Thread(start);        
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    protected abstract void Program();
    #endregion

    #region Turtle Methods
    protected void Forward(double n)
    {
        var r = a * Math.PI / 180.0;
        var x2 = x + Math.Cos(r) * n;
        var y2 = y + Math.Sin(r) * n;
        if (isPenDown) 
        {
            var line = new Line() { X1 = x, Y1 = y, X2 = x2, Y2 = y2 };
            line.Stroke = new SolidColorBrush(penColor);
            line.StrokeThickness = 1.0;
            screen.Children.Add(line);
        }
        x = x2;
        y = y2;
    }

    protected void Left(double n)
    {
        a -= n;
    }

    protected void Right(double n)
    {
        a += n;
    }

    protected void PenUp()
    {
        isPenDown = false;
    }

    protected void PenDown()
    {
        isPenDown = true;
    }

    protected void PenColor(Color c)
    {
        penColor = c;
    }

    protected void FD(double n) { Forward(n); }
    protected void LT(double n) { Left(n); }
    protected void RT(double n) { Right(n); }

    protected void Repeat(int n, Action action)
    {
        for (int i = 0; i < n; i++)
            action();
    }
    #endregion
}