using System.Drawing;

namespace Laboratory;

public interface IHasBorder
{
    Point LeftTopCorner  { get; set; }
    Point RightBottomCorner { get; set; } 
}

