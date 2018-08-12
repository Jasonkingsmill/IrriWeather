﻿namespace IrriWeather.IO.Control.NativeEnums
{
    /// <summary>
    /// Enumerates the GPIO (electrical) Pads
    /// 0   @ 0-27
    /// 1   @ 28-45
    /// 2   @ 46-53
    /// </summary>
    public enum GpioPadId
    {
        /// <summary>
        /// The pad of GPIO 0 to 27
        /// </summary>
        Pad00To27 = 0,

        /// <summary>
        /// The pad of GPIO 28 to 45
        /// </summary>
        Pad28To45 = 1,

        /// <summary>
        /// The pad of GPIO 46 to53
        /// </summary>
        Pad46To53 = 2,
    }
}
