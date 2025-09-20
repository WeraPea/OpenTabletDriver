using OpenTabletDriver.Plugin.Tablet;
using System.Numerics;

namespace OpenTabletDriver.Configurations.Parsers.Pine64
{
    public class PineNoteReportParser : IReportParser<IDeviceReport>
    {
        private Vector2 _lastPosition;
        private uint _lastPressure;
        private bool[] _lastPenButtons = new bool[3];

        public IDeviceReport Parse(byte[] report)
        {
            switch(report[0]) {
                case 2:
                    return new PineNoteTabletReport(report, ref _lastPosition, ref _lastPressure, ref _lastPenButtons);
                case 1:
                    return new PineNoteButtonReport(report, ref _lastPosition, ref _lastPressure, ref _lastPenButtons);
                default:
                    return null;
            }
        }
    }
}
