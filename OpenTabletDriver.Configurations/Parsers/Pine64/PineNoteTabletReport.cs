using System.Numerics;
using System.Runtime.CompilerServices;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.Configurations.Parsers.Pine64
{
    public struct PineNoteTabletReport : ITabletReport, IProximityReport, ITiltReport
    {
        public PineNoteTabletReport(byte[] report, ref Vector2 lastPosition, ref uint lastPressure, ref bool[] lastPenButtons)
        {
            Raw = report;

            Position = new Vector2
            {
                X = Unsafe.ReadUnaligned<ushort>(ref report[2]),
                Y = Unsafe.ReadUnaligned<ushort>(ref report[4])
            };

            // Unit: [-9000..9000]x10^-3 degrees
            Tilt = new Vector2
            {
                X = Unsafe.ReadUnaligned<short>(ref report[9]) * 0.01f,
                Y = Unsafe.ReadUnaligned<short>(ref report[11]) * 0.01f
            };

            NearProximity = report[1].IsBitSet(5);

            Pressure = Unsafe.ReadUnaligned<ushort>(ref report[6]);
            HoverDistance = report[1].IsBitSet(0) ? 0 : (uint)report[8];

            PenButtons = lastPenButtons; // Use the last known button state (button state for passive pens not implemented)

            lastPosition = Position;
            lastPressure = Pressure;
        }

        public byte[] Raw { set; get; }
        public Vector2 Position { set; get; }
        public Vector2 Tilt { set; get; }
        public uint Pressure { set; get; }
        public bool[] PenButtons { set; get; }
        public bool NearProximity { set; get; }
        public uint HoverDistance { set; get; }
    }
}
