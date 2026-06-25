using System.Numerics;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.Configurations.Parsers.Pine64
{
    public class PineNoteReportParser : IReportParser<IDeviceReport>
    {
        private Vector2 _lastPosition;
        private uint _lastPressure;
        private bool[] _passivePenButtons = new bool[5];
        private bool[] _btPenButtons = new bool[5];

        public IDeviceReport Parse(byte[] report)
        {
            switch (report[0])
            {
                case 2:
                    return new PineNoteTabletReport(report, ref _lastPosition, ref _lastPressure, ref _passivePenButtons, ref _btPenButtons);
                case 1:
                    return new PineNoteButtonReport(report, ref _lastPosition, ref _lastPressure, ref _passivePenButtons, ref _btPenButtons);
                default:
                    return null;
            }
        }
    }
}
