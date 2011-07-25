using System.Windows.Media;

public class TurtleProgram : TurtleLanguage
{
    protected override void Program()
    {
       PenColor(Colors.Red);
       Repeat(10, () => { RT(36); Repeat(5, () => { FD(54); RT(72); }); });
    }
}

