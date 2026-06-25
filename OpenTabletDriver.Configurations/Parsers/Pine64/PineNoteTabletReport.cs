using System.Numerics;
using System.Runtime.CompilerServices;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.Configurations.Parsers.Pine64
{
    public struct PineNoteTabletReport : ITabletReport, IProximityReport, ITiltReport, IEraserReport
    {
        public PineNoteTabletReport(byte[] report, ref Vector2 lastPosition, ref uint lastPressure, ref bool[] passivePenButtons, ref bool[] btPenButtons)
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

            // bool Tip = report[1].IsBitSet(0);
            bool BarrelSwitch = report[1].IsBitSet(1);
            // bool EraserSwitch = report[1].IsBitSet(2);
            Eraser = report[1].IsBitSet(3);
            bool SecondaryBarrelSwitch = report[1].IsBitSet(4);
            NearProximity = report[1].IsBitSet(5);

            Pressure = Unsafe.ReadUnaligned<ushort>(ref report[6]);
            HoverDistance = (uint)report[8];

            passivePenButtons = new bool[]
            {
                BarrelSwitch,
                SecondaryBarrelSwitch,
                false,
                false,
                false,
            };
            PenButtons = new bool[]
            {
                passivePenButtons[0] | btPenButtons[0],
                passivePenButtons[1] | btPenButtons[1],
                btPenButtons[2],
                btPenButtons[3],
                btPenButtons[4],
            };

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
        public bool Eraser { set; get; }
    }
}
