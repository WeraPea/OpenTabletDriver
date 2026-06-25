using System.Numerics;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.Configurations.Parsers.Pine64
{
    public struct PineNoteButtonReport : ITabletReport
    {
        public PineNoteButtonReport(byte[] report, ref Vector2 lastPosition, ref uint lastPressure, ref bool[] passivePenButtons, ref bool[] btPenButtons)
        {
            Raw = report;

            Position = lastPosition;
            Pressure = lastPressure;

            btPenButtons = new bool[]
            {
                report[1].IsBitSet(0),
                report[1].IsBitSet(1),
                report[1].IsBitSet(2),
                report[1].IsBitSet(3),
                report[1].IsBitSet(4),
            };

            PenButtons = new bool[]
            {
                passivePenButtons[0] | btPenButtons[0],
                passivePenButtons[1] | btPenButtons[1],
                btPenButtons[2],
                btPenButtons[3],
                btPenButtons[4],
            };
        }

        public byte[] Raw { set; get; }
        public Vector2 Position { set; get; }
        public uint Pressure { set; get; }
        public bool[] PenButtons { set; get; }
    }
}
