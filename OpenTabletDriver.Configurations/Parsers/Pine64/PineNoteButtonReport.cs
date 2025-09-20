using System.Numerics;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.Configurations.Parsers.Pine64
{
    public struct PineNoteButtonReport : ITabletReport
    {
        public PineNoteButtonReport(byte[] report, ref Vector2 lastPosition, ref uint lastPressure, ref bool[] lastPenButtons)
        {
            Raw = report;

            Position = lastPosition;
            Pressure = lastPressure;

            PenButtons = new bool[]
            {
                report[1].IsBitSet(0),
                report[1].IsBitSet(1),
                report[1].IsBitSet(2),
            };

            lastPenButtons = PenButtons;
        }

        public byte[] Raw { set; get; }
        public Vector2 Position { set; get; }
        public uint Pressure { set; get; }
        public bool[] PenButtons { set; get; }
    }
}
